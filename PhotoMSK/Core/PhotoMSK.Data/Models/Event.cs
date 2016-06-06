using System;

namespace PhotoMSK.Data.Models
{
    public class Event : UniqueEntity
    {
        public Event()
        {
            CreaTime = DateTime.Now;
            EventState = EventState.Created;
        }

        public string GoogleID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool AllDay { get; set; }
        public DateTime CreaTime { get; set; }
        public double Price { get; set; }
        public string Tags { get; set; }
        public String Url { get; set; }
        public string ClassName { get; set; }
        public bool Editable { get; set; }
        public bool StartEditable { get; set; }
        public bool DurationEditable { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string TextColor { get; set; }
        public virtual UserInformation User { get; set; }
        public virtual UserInformation CreatedBy { get; set; }
        public Guid UserInformationID { get; set; }
        public Guid? CreatedByID { get; set; }
        public virtual Calendar Calendar { get; set; }
        public EventState EventState { get; set; }
        public double PrePayed { get; set; }
    }
}