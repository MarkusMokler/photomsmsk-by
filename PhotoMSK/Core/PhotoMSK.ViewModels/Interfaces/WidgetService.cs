using System;
using System.Collections.Generic;
using System.Linq;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Widgets;
using PhotoMSK.ViewModels.Widgets;

namespace PhotoMSK.ViewModels.Interfaces
{
    internal class WidgetService : IWidgetService
    {
        readonly Lazy<AppContext> _context = new Lazy<AppContext>(() => new AppContext());
        public List<BaseWidgetViewModel.Details> GetWidgets(Guid LayoutID)
        {
            List<BaseWidgetViewModel.Details> widgetViews = new List<BaseWidgetViewModel.Details>();
            var baseWidgets = _context.Value.Widgets.Where(x => x.Zone.LayoutID == LayoutID).ToList();

            var menu = baseWidgets.OfType<MenuWidget>().ToList();

            LoadMenuWidgets(menu);


            return widgetViews;
        }

        private void LoadMenuWidgets(List<MenuWidget> menu)
        {
            var menues = menu.Select(x => x.MenuID);

//_context.Value.AbstractMenuItem.   
     }
    }
}