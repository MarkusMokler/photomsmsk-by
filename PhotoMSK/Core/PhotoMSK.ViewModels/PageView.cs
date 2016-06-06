using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.ViewModels.Route;

namespace PhotoMSK.ViewModels
{
    public class PageView<T>
    {
        public PageView() { }

        internal PageView(IList<T> @where, int count, int size)
        {
            ItemsCount = count;
            PageSize = size;
            Elements = @where;
        }
        public IList<T> Elements { get; set; }
        public int PageSize { get; set; }
        public int ItemsCount { get; set; }
        public int PagesCount
        {
            get { return ItemsCount / PageSize; }
        }
        public PageView<TE> AsPageView<TE>()
        {
            return new PageView<TE>
            {
                ItemsCount = ItemsCount,
                PageSize = PageSize,
                Elements = Mapper.Map<IList<TE>>(Elements)
            };
        }
    }
}