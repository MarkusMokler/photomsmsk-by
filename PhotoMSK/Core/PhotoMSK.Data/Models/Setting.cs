using System;

namespace PhotoMSK.Data.Models
{
    public class Setting : UniqueEntity
    {
        public string SettingName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public static Guid NOTIFY_ABOUT_BOOKING = Guid.Parse("08876b89-e399-4e25-8b2f-baefb37e3f1f");
        public static Guid NOTIFY_NUMBER = Guid.Parse("618aed3a-3b76-45bc-85be-98be7494e813");
    }


}