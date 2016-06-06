using System;

namespace PhotoMSK.ViewModels
{
    public class CommentViewModel
    {
        public Guid ID { get; set; }
        public DateTime CreationTime { get; set; }
        public string Text { get; set; }
        public Guid UserInformationID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public bool IsAnswer { get; set; }
        public string AnsweredName { get; set; }
    }
}