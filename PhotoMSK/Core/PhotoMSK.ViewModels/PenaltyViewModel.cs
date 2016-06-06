using System;

namespace PhotoMSK.ViewModels
{
    public static class PenaltyViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public string Route { get; set; }
            public DateTime Date { get; set; }
            public string Description { get; set; }
            public Double Price { get; set; }        
            public Guid UserInformationID { get; set; }
            public decimal Paragraph { get; set; }
        }

        public class Details : Summary
        {
            public virtual UserInformationViewModel.Summary User { get; set; }
        }
        
    }
}