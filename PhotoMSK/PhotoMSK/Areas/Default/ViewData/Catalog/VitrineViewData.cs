using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.Areas.Default.ViewData.Catalog
{

    public class VitrineViewData : WhiteLabelViewData
    {
        public virtual PageView<PhototechnicsViewModel.Summary> Technics { get; set; }
    }

    public class VitrineViewData<T> : VitrineViewData where T : RouteEntityViewModel.Summary
    {
        public virtual T RouteDetails { get; set; }

        public override RouteEntityViewModel.Summary Route { get { return RouteDetails; } }

    }


}