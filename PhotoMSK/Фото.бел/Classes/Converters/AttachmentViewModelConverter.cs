using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Attachments;

namespace Fotobel.Classes.Converters
{
    public class AttachmentViewModelConverter : JsonCreationConverter<AttachmentViewModel.Details>
    {
        public override bool CanWrite => false;

        protected override AttachmentViewModel.Details Create(Type objectType, JObject jsonObject)
        {
            string typeName = "wall";
            if (jsonObject.Properties().Any(x => x.Name == "type"))
                typeName = (jsonObject["type"]).ToString();
            switch (typeName.ToLower())
            {
                case "photo":
                    return new PhotoViewModel.Details();
                case "audio":
                    return new AudioViewModel.Details();
                case "video":
                    return new VideoViewModel.Details();
                case "poll":
                    return new PollViewModel.Details();
                default:
                    return null;
            }
        }
    }
}