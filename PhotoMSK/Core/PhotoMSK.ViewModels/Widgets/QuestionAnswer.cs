using PhotoMSK.Data.Models;

namespace PhotoMSK.ViewModels.Widgets
{
    public class QuestionAnswer
    {
        public class Details : UniqueEntity
        {
            public string Question { get; set; }
            public string Answer { get; set; }
        }
    }
}