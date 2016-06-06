using System;
using PhotoMSK.Data.Models;

namespace PhotoMSK.ViewModels.Event
{
    public interface IEventViewModel
    {
        Guid ID { get; }
        string Title { get; }
        DateTime Start { get; }
        DateTime End { get; }
        string ClassName { get; }
        bool Editable { get; }
        bool StartEditable { get; }
        bool DurationEditable { get; }
        string Color { get; }
        string BackgroundColor { get; }
        string BorderColor { get; }
        string TextColor { get; }
        string EventState { get; }

    }
}