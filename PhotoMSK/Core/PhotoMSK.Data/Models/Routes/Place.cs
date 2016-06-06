namespace PhotoMSK.Data.Models.Routes
{
    public class Place : RouteEntity
    {
        public string Title { get; set; }
       
        public override string ToString()
        {
            return Title;
        }

        public override string GetName()
        {
            return Title;
        }
    }
}