using System;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using Castle.DynamicProxy;

namespace Fotobel.Models
{

    public class Proxy<T> : DynamicObject, IInterceptor
    {
        private T _model;

        private Type _componentType;

        public Proxy()
        {
            var _generator = new ProxyGenerator();
            Model = (T)_generator.CreateClassProxy(typeof(T));
        }

        public T Model
        {
            get { return _model; }
            private set
            {
                this._model = value;
                this._componentType = _model.GetType();
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var binderName = binder.Name;
            if (binderName == "Type")
                return true;

            PropertyInfo property = _componentType.GetProperty(binderName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (property != null)
            {
                if (property.PropertyType.IsGenericType && IsNullableType(property.PropertyType) == false)
                    return false;
                if (property.PropertyType.Namespace.Contains("System") == false)
                    return false;
                if (!property.CanWrite)
                    return false;

                if (property.PropertyType.IsEnum)
                {
                    if (value is string)
                    {
                        property.SetValue(_model, ParseEnum(property.PropertyType, value), null);
                    }
                    else
                    {
                        property.SetValue(_model, Enum.ToObject(property.PropertyType, value), null);
                    }
                }
                else
                {
                    SetValue(_model, property, value);
                }


                return true;
            }

            FieldInfo field = _componentType.GetField(binderName, BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(_model, value);
                return true;
            }

            return base.TrySetMember(binder, value);
        }

        public static object ParseEnum(System.Type type, object value)
        {
            int n;
            return int.TryParse(value as string, out n) ? Enum.ToObject(type, n) : Enum.Parse(type, (string) value);
        }
        public static void SetValue(object inputObject, PropertyInfo propertyInfo, object propertyVal)
        {
            if (propertyVal == null)
            {
                propertyInfo.SetValue(inputObject, null, null);
                return;
            }
            Type type = inputObject.GetType();

            Type propertyType = propertyInfo.PropertyType;

            var targetType = IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;

            propertyVal = targetType == typeof(Guid) ? Guid.Parse((string)propertyVal) : Convert.ChangeType(propertyVal, targetType);

            propertyInfo.SetValue(inputObject, propertyVal, null);

        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var binderName = binder.Name;
            PropertyInfo property = _componentType.GetProperty(binderName, BindingFlags.Public | BindingFlags.Instance);
            if (property != null)
            {
                result = property.GetValue(_model, null);
                return true;
            }

            FieldInfo field = _componentType.GetField(binderName, BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
            {
                result = field.GetValue(_model);
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args,
              out object result)
        {
            var binderName = UppercaseFirst(binder.Name);
            MethodInfo method = this._componentType.GetMethod(binderName, BindingFlags.Public | BindingFlags.Instance, null, this.GetArgumentsTypes(args), null);

            if (method != null)
            {
                result = method.Invoke(_model, args);
                return true;
            }

            return base.TryInvokeMember(binder, args, out result);
        }

        public void Intercept(IInvocation invocation)
        {
            Type decoratorType = GetType();
            Type[] argTypes = GetArgumentsTypes(invocation.Arguments);

            MethodInfo method = decoratorType.GetMethod(invocation.Method.Name, BindingFlags.Public | BindingFlags.Instance, null, argTypes, null);

            if (method != null)
            {
                invocation.ReturnValue = method.Invoke(this, invocation.Arguments);
                return;
            }

            method = this._componentType.GetMethod(invocation.Method.Name, BindingFlags.Public | BindingFlags.Instance, null, argTypes, null);

            if (method != null)
            {
                invocation.ReturnValue = method.Invoke(this._model, invocation.Arguments);
                return;
            }

            // No match was found for required method
            var argList = this.GetArgumentsString(invocation.Arguments);
            throw new InvalidOperationException(
                string.Format("No match was found for method {0}({1}).",
                    invocation.Method.Name, argList));
        }

        public T GetComponent()
        {
            var decorator = Model as Proxy<T>;
            if (decorator != null)
            {
                return decorator.GetComponent();
            }

            return Model;
        }

        public void SetComponent(T component)
        {
            var decorator = this.Model as Proxy<T>;
            if (decorator != null)
            {
                decorator.SetComponent(component);
                return;
            }

            this.Model = component;
        }

        private Type[] GetArgumentsTypes(object[] args)
        {
            Type[] argTypes = new Type[args.GetLength(0)];

            int index = 0;
            foreach (var arg in args)
            {
                argTypes[index] = arg.GetType();
                index++;
            }

            return argTypes;
        }

        private string GetArgumentsString(object[] args)
        {
            string argList = string.Empty;
            bool isFirstArgument = true;

            foreach (var arg in args)
            {
                if (isFirstArgument)
                {
                    isFirstArgument = false;
                }
                else
                {
                    argList += ", ";
                }

                argList += arg.GetType().ToString();
            }

            return argList;
        }

        public void Patch(object model)
        {
            var type = model.GetType();

            var pachedObjectProperties = _model.GetType().GetProperties().Select(x => x.Name).ToList();

            foreach (
                var pachedProperty in
                    type.GetProperties().Where(x => pachedObjectProperties.Contains(x.Name)).ToList())
            {
                if (pachedProperty.CanWrite == false)
                    continue;
                if (pachedProperty.Name.Equals("ID", StringComparison.InvariantCultureIgnoreCase))
                    continue;
                var skipField = false;

                //if (model is IAccesable)
                //{
                //    var attrs = pachedProperty.GetCustomAttributes(true);
                //    var access = attrs.OfType<FieldAccessLevel>().FirstOrDefault();

                //    if (access != null)
                //    {
                //        if (!HttpContext.Current.User.Identity.IsAuthenticated)
                //            throw new AuthenticationException();

                //        var service = (IRoleService)DependencyResolver.Current.GetService(typeof(IRoleService));
                //        var role = service.GetRole(HttpContext.Current.User.Identity.GetUserId(),
                //            ((IAccesable) model)?.GetOrganizationID());
                //        if (access.Role < role)
                //        {
                //            skipField = true;
                //        }
                //    }

                //}
                if (skipField)
                    continue;

                var getter = _model.GetType().GetProperty(pachedProperty.Name);
                var toValue = getter.GetValue(_model, null);

                if (toValue is string)
                {
                    var s = toValue as string;
                    if (!string.IsNullOrEmpty(s))
                    {
                        pachedProperty.SetValue(model, toValue, null);
                    }
                }
                else
                {
                    if (toValue != null)
                    {
                        pachedProperty.SetValue(model, toValue, null);
                    }
                }
            }
        }


        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

    }
}