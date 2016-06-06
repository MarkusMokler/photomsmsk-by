using System.Collections.Generic;
using PhotoMSK.Data.Models.Widgets;

namespace PhotoMSK.ViewModels.Widgets
{
    public static class FaqWidgetViewModel
    {
        public class Details : BaseWidgetViewModel.Details
        {
            public string Type => "faqWidget";
            public virtual List<QuestionAnswer> QuestionAnswers { get; set; }
        }
    }
}