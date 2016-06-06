using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Profiling;

namespace PhotoMSK.Extensiolns
{
    public static class ObjectExtension
    {
        public static void UpdateValues(this object self, object other)
        {
            var type = self.GetType();

            var properties = other.GetType().GetProperties().Select(x => x.Name).ToList();

            foreach (var fromProp in type.GetProperties().Where(x => properties.Contains(x.Name)).ToList())
            {
                if (fromProp.CanWrite == false)
                    continue;

                var toProp = type.GetProperty(fromProp.Name);
                var toValue = toProp.GetValue(self, null);

                Guid? tv = toValue as Guid?;
                if (tv.HasValue)
                {
                    if (tv.Value == Guid.Empty)
                        continue;
                }

                if (toValue != null && !toValue.Equals(0))
                {
                    fromProp.SetValue(other, toValue, null);
                }
            }
        }
        public static void SetValues(this object self, object other)
        {
            var otherType = other.GetType();
            var selfType = self.GetType();
            var properties = selfType.GetProperties().Select(x => x.Name).ToList();

            foreach (var property in otherType.GetProperties().Where(x => properties.Contains(x.Name)).ToList())
            {
                if (property.CanWrite == false)
                    continue;

                var value = property.GetValue(other, null);

                var guid = value as Guid?;

                if (guid.HasValue)
                    if (guid.Value == Guid.Empty)
                        continue;

                if (value != null && !value.Equals(0))
                    selfType.GetProperty(property.Name).SetValue(self, value, null);
            }
        }
        public static HtmlString ToJson(this Object value)
        {
            string str = "";

            using (var writer = new StringWriter())
            {
                var serializer = JsonSerializer.Create();
                serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
                serializer.Serialize(writer, value);
                str = writer.ToString();
            }
            var @stirng = new MvcHtmlString(str);

            return @stirng;
        }

        public static T As<T>(this object value)
        {
            return Mapper.Map<T>(value);
        }
    }
}