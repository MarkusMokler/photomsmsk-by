using System;

namespace PhotoMSK.Data.Models.Permission
{
    [Flags]
    public enum AccountManage
    {
        ViewAdministrators, AddAdministrator, RemoveAdministrator, ChangePermission
    }
}