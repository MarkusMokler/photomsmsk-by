using System;
using System.Collections.Generic;
using System.Linq;
using PhotoMSK.Data.Models.Widgets;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.RoutePageLayouts;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Widgets
{
    public class WidgetMapper : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<ContainerWidget, ContainerWidgetViewModel.Details>();

            CreateMap<CalendarWidget, CalendarWidgetViewModel.Details>();

            CreateMap<MenuWidget, RouteMenuWidgetViewModel.Details>();

            CreateMap<HallWidget, HallWidgetViewModel.Details>();

            CreateMap<DescriptionWidget, DescriptionWidgetViewModel.Details>();

            CreateMap<FaqWidget, FaqWidgetViewModel.Details>();

            CreateMap<GalleryWidget, GalleryWidgetViewModel.Details>()
                .ForMember(x => x.Files,
                    opt => opt.MapFrom(y => y.Files.OrderBy(m => m.Position).Select(n => n.FileEntry).ToList()));

            CreateMap<HeaderWidget, HeaderWidgetViewModel.Details>();

            CreateMap<SplitWidget, SplitWidgetViewModel.Details>();

            CreateMap<TextAdnDescriptionWidget, TextAdnDescriptionWidgetViewModel.Details>();

            CreateMap<Zone, LandingPageViewModel.Summary>();

            CreateMap<CalendarsWidget, CalendarsWidgetViewModel.Details>();

            CreateMap<Zone, LandingPageViewModel.Details>()
                .ForMember(x => x.Widgets, opt => opt.MapFrom(src => getList(src.Widgets.OrderBy(x => x.Position))));

            CreateMap<Zone, ZoneViewModel>()
                .ForMember(x => x.Widgets, opt => opt.MapFrom(src => getList(src.Widgets.OrderBy(x => x.Position))));
        }


        public IEnumerable<BaseWidgetViewModel.Details> getList(IEnumerable<BaseWidget> widgets)
        {
            return widgets.Select(getWidget);
        }

        private BaseWidgetViewModel.Details getWidget(BaseWidget editWidget)
        {
            var galleryWidget = editWidget as GalleryWidget;
            if (galleryWidget != null)
                return galleryWidget.As<GalleryWidgetViewModel.Details>();

            var descriptionWidget = editWidget as DescriptionWidget;
            if (descriptionWidget != null)
                return descriptionWidget.As<DescriptionWidgetViewModel.Details>();

            var headerWidget = editWidget as HeaderWidget;
            if (headerWidget != null)
                return headerWidget.As<HeaderWidgetViewModel.Details>();

            var splitWidget = editWidget as SplitWidget;
            if (splitWidget != null)
                return splitWidget.As<SplitWidgetViewModel.Details>();

            var containerWidget = editWidget as ContainerWidget;
            if (containerWidget != null)
                return containerWidget.As<ContainerWidgetViewModel.Details>();

            var widget = editWidget as TextAdnDescriptionWidget;
            if (widget != null)
                return widget.As<TextAdnDescriptionWidgetViewModel.Details>();

            var calendar = editWidget as CalendarsWidget;
            if (calendar != null)
                return calendar.As<CalendarsWidgetViewModel.Details>();

            var menu = editWidget as MenuWidget;
            if (menu != null)
                return menu.As<RouteMenuWidgetViewModel.Details>();

            var hall = editWidget as HallWidget;
            if (hall != null)
                return hall.As<HallWidgetViewModel.Details>();

            throw new NotImplementedException();

        }
    }
}