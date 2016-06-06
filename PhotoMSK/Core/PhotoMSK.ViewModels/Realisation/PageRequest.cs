using System;

namespace PhotoMSK.ViewModels.Realisation
{
    public class PageRequest<T>
    {
        public PageRequest()
        {
            Page = 0;
            Size = 20;
            Where = x => true;
        }

        public Func<T, bool> Where { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}