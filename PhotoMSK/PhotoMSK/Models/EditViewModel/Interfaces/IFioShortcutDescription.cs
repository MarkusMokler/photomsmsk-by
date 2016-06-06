using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoMSK.Models.EditViewModel.Interfaces
{
    public interface IFioShortcutDescription
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Shortcut { get; set; }
        string Description { get; set; }
    }
}
