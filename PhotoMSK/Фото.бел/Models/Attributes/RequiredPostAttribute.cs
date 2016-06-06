using System;
using System.ComponentModel.DataAnnotations;

namespace Fotobel.Models.Attributes
{
    public class RequiredPostAttribute : ValidationAttribute
    {
        private readonly RequiredAttribute _innerAttribute = new RequiredAttribute();
        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }

        public override bool IsValid(object value)
        {
            return !string.Equals(System.Web.HttpContext.Current.Request.HttpMethod, "post", StringComparison.InvariantCultureIgnoreCase) || _innerAttribute.IsValid(value);
        }
    }
}