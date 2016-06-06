using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json.Linq;
using PhotoMSK.Data;

namespace Fotobel.Models.Attributes
{
    public class ShortcutAttribute : ValidationAttribute
    {
        private readonly RequiredAttribute _innerAttribute = new RequiredAttribute();
        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }

        public override bool IsValid(object value)
        {
            using (var context = new AppContext())
            {
                if (
                    string.Equals(System.Web.HttpContext.Current.Request.HttpMethod, "put",
                        StringComparison.InvariantCultureIgnoreCase) && value == null)
                    return true;

                var val = (string)value;
                var result = new System.IO.StreamReader(System.Web.HttpContext.Current.Request.InputStream).ReadToEnd();
                var oo = JObject.Parse(result);
                JToken token;
                var id = Guid.Empty;

                if (oo.TryGetValue("id", out token))
                    id = (Guid)token;

                var route = context.Routes.FirstOrDefault(x => x.Shortcut == val);

                if (route == null)
                    return true;
                return route.ID == id;

            }

        }
    }
}