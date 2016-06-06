using System;

namespace PhotoMSK.Data.Models.ActivityStream
{
    [Flags]
    public enum CallType
    {
        Incoming, Outcoming, Missed,
    }
}