using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.ViewModels.Widgets;

namespace PhotoMSK.ViewModels.Interfaces
{
    public interface IWidgetService
    {
        List<BaseWidgetViewModel.Details> GetWidgets(Guid LayoutID);
    }
}
