namespace PhotoMSK.Data.Models.Attachments
{
    public class Photo : Attachment
    {
        public string FileName { get; set; }
        public string SmallUrl { get; set; }
        public string MediumUrl { get; set; }
        public string LargeUrl { get; set; }
    }
}