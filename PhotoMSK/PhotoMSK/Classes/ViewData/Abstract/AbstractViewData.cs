namespace PhotoMSK.Areas.Default.ViewData
{
    public interface IBaseViewData
    {
        /// <summary>
        /// Заголовок страницы, вставляется в метатег
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Описание страницы вставляется в метатег
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Изображение страницы, вставляется в метатег
        /// </summary>
        string Image { get; set; }

        /// <summary>
        /// Ключевые слова, вставляются в метатег
        /// </summary>
        string KeyWords { get; set; }
    }

    /// <summary>
    /// Базовая страница с данными, реализует все необходимые поля на лэйауте
    /// </summary>
    public abstract class BaseViewData : IBaseViewData
    {
        /// <summary>
        /// Заголовок страницы, вставляется в метатег
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// Описание страницы вставляется в метатег
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// Изображение страницы, вставляется в метатег
        /// </summary>
        public virtual string Image { get; set; }
        /// <summary>
        /// Ключевые слова, вставляются в метатег
        /// </summary>
        public virtual string KeyWords { get; set; }
    }
}