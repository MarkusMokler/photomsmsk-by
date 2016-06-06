using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models.ShoppingCart;

namespace PhotoMSK.Data.Models
{
    public class Contract
    {
        public Guid ID { get; set; }
        public Guid OrderID { get; set; }
        public virtual Order Order { get; set; }
        public virtual ContractTemplate Template { get; set; }
        public int BarCode { get; set; }
        public bool Closed { get; set; }
    }

    public class ContractTemplate
    {
        public Guid ID { get; set; }
        public Guid RouteID { get; set; }
        public string Template { get; set; }
        public string Url { get; set; }
        public virtual RouteEntity Route{ get; set; }
    }

    public class EventOrderLineMapping : EntityTypeConfiguration<Contract>
    {
        public EventOrderLineMapping()
        {
            HasRequired(x => x.Order).WithOptional(x=>x.Contract);
            HasRequired(x => x.Template).WithMany();
        }

    }
}
