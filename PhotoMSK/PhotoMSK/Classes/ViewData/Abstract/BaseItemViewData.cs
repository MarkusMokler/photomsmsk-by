namespace PhotoMSK.Areas.Default.ViewData
{
    /// <summary>
    /// Базовая страница для одного элемента
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseItemViewData<T> : BaseViewData
    {
        /// <summary>
        /// Элемент на странице
        /// </summary>
        public T Item { get; set; }
    }
}