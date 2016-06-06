using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using PhotoMSK.Data.Models.Attachments;

namespace Fotobel.Classes.Converters
{
    public class AttachmentConverter : JsonCreationConverter<Attachment>
    {
        public override bool CanWrite => false;

        protected override Attachment Create(Type objectType, JObject jsonObject)
        {
            string typeName = "wall";
            if (jsonObject.Properties().Any(x => x.Name == "type"))
                typeName = (jsonObject["type"]).ToString();
            switch (typeName.ToLower())
            {
                case "photo":
                    return new Photo();
                case "audio":
                    return new Audio();
                case "video":
                    return new Video();
                case "poll":
                    return new Poll();
                default:
                    return null;
            }
        }
    }
}