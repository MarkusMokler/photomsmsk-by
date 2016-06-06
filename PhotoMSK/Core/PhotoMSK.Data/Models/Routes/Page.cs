namespace PhotoMSK.Data.Models.Routes
{
    public class Page : RouteEntity
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override string GetName()
        {
            return Name;
        }
    }
}