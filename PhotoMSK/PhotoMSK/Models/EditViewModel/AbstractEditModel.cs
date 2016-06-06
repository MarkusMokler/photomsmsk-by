using System;
using System.Linq;

namespace PhotoMSK.Models.EditViewModel
{
    public abstract class AbstractEditModel
    {
        public Guid? ID { get; set; }

        public void PatchModel(object ob)
        {
            var type = ob.GetType();

            var pachedObjectProperties = GetType().GetProperties().Select(x => x.Name).ToList();

            foreach (
                var pachedProperty in
                    type.GetProperties().Where(x => pachedObjectProperties.Contains(x.Name)).ToList())
            {
                if (pachedProperty.CanWrite == false)
                    continue;

                var getter = GetType().GetProperty(pachedProperty.Name);
                var toValue = getter.GetValue(this, null);

                if (toValue is string)
                {
                    var s = toValue as string;
                    if (!string.IsNullOrEmpty(s))
                    {
                        pachedProperty.SetValue(ob, toValue, null);
                    }
                }
                else
                {
                    if (toValue != null)
                    {
                        pachedProperty.SetValue(ob, toValue, null);
                    }
                }
            }
        }
    }
}