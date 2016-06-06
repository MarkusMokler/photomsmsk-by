using PhotoMSK.ViewModels;

namespace PhotoMSK.Areas.Default.ViewData
{
    /// <summary>
    /// Стандартный шаблон данных для страницы с пейджингом
    /// Унаследован от базовой страницы с сеотегами 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BasePageViewData<T> : BaseViewData
    {
        /// <summary>
        /// Страница с данными о Т
        /// </summary>
        public PageView<T> Page { get; set; }
    }
}