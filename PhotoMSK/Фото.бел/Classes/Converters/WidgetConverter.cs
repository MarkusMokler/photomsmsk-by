using System;
using Newtonsoft.Json.Linq;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.Data.Models.Widgets;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Widgets;

namespace Fotobel.Classes.Converters
{
    /// <summary>
    /// The converter to use when deserializing animal objects
    /// </summary>
    public class WidgetConverter : JsonCreationConverter<BaseWidgetViewModel.Details>
    {

        public override bool CanWrite => false;

        /// <summary>
        /// The class that will create Animals when proper json objects are passed in
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        protected override BaseWidgetViewModel.Details Create(Type objectType, JObject jsonObject)
        {
            // examine the $type value
            string typeName = (jsonObject["type"]).ToString();


            switch (typeName)
            {

                case "gallery":
                    {
                        return new GalleryWidgetViewModel.Details();
                    }
                case "description":
                    {
                        return new TextAdnDescriptionWidgetViewModel.Details()
                        {
                            Photo = new PhotoViewModel.Details()
                        };
                    }
                case "splitWidget":
                    {
                        return new SplitWidgetViewModel.Details();
                    }
                case "containerWidget":
                    {
                        return new ContainerWidgetViewModel.Details();
                    }
                case "faqWidget":
                    {
                        return new FaqWidgetViewModel.Details();
                    }
                case "descriptionWidget":
                    {
                        return new DescriptionWidgetViewModel.Details();
                    }

                case "headerWidget":
                    {
                        return new HeaderWidgetViewModel.Details();
                    }
                case "calendarWidget":
                    {
                        return new CalendarsWidgetViewModel.Details();
                    }
                case "menuWidget":
                    {
                        return new RouteMenuWidgetViewModel.Details();
                    }
                case "hallWidget":
                    {
                        return new HallWidgetViewModel.Details();
                    }
                default:
                    throw new NotImplementedException($"type {typeName} not implement yet");
            }

        }
    }
}