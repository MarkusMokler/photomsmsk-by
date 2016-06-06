using System;

namespace PhotoMSK.Data.Models.Widgets
{
    public class QuestionAnswersFaq : SortedEntry
    {
        public Guid FaqWidgetID { get; set; }
        public virtual FaqWidget FaqWidget { get; set; }

        public Guid QuestionAnswerID { get; set; }
        public virtual FaqWidget QuestionAnswer { get; set; }
    }
}