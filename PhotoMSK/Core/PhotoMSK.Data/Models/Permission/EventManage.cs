using System;

namespace PhotoMSK.Data.Models.Permission
{
    [Flags]
    public enum EventManage
    {
        CreateAdminEvent, ViewAdminEvent, MoveAdminEvent, FullPatch, DeleteAdminEvent
    }
}