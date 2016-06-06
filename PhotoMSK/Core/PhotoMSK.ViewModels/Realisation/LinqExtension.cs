using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using PhotoMSK.Data.Models;

namespace PhotoMSK.ViewModels.Realisation
{
    public static class LinqExtension
    {
        public static PageView<T> ToPage<T>(this IOrderedQueryable<T> queryable, PageRequest<T> request)
        {
            var count = queryable.Where(request.Where).Count();

            return new PageView<T>(queryable.Where(request.Where).Skip(request.Page * request.Size).Take(request.Size).ToList(), count, request.Size);
        }

        public static IEnumerable<T> GetReferenses<TE, T>(this IEnumerable<TE> ienumerable, Func<TE, Guid> fromproperty, IQueryable<T> query, Expression<Func<T, Guid>> expr) where T : UniqueEntity
        {
            List<Guid> ids = ienumerable.Select(fromproperty).ToList();
            var methodcall = CreateCompatibleDelegate<T>(ids, ids.GetType().GetMethod("Contains"), expr);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(methodcall, expr.Parameters);
            return query.Where(lambda).ToList();
        }

        public static Expression CreateCompatibleDelegate<T>(object instance, MethodInfo method, Expression<Func<T, Guid>> argExpression)
        {
            return Expression.Call(
                instance == null ? null : Expression.Constant(instance),
                method,
                argExpression.Body);
        }

        public static T As<T>(this object ob)
        {
            return Mapper.Map<T>(ob);
        }

        public static IEnumerable<T> AsIEnumerable<T>(this IEnumerable ob)
        {
            return from object onn in ob select (T)Mapper.Map(onn, ObjectContext.GetObjectType(onn.GetType()), typeof(T));
        }

        public static void AttachListTo<T, TE>(this IEnumerable<T> enumerable, IList<TE> attached, Func<T, Guid> attachment, Func<TE, Guid> attach)
        {

            foreach (var elem in enumerable.GroupBy(attachment))
            {
                var elem1 = elem;
                foreach (var e1 in attached.Where(x => attach(x) == elem1.Key))
                {
                    foreach (var propertyInfo in e1.GetType().GetProperties().Where(x => x.PropertyType.IsAssignableFrom(typeof(IList<T>))))
                    {
                        propertyInfo.SetValue(e1, elem.ToList());
                    }
                }
            }
        }
    }
}