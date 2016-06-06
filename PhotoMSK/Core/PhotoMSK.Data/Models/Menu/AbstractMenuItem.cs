using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace PhotoMSK.Data.Models.Menu
{
    public abstract class AbstractMenuItem
    {
        public Guid ID { get; set; }
        public Guid RouteID { get; set; }
        public virtual RouteEntity RouteEntity { get; set; }
        public string Name { get; set; }
        public string Order { get; set; }
        public bool Publish { get; set; }
        public virtual Guid? ParentID { get; set; }
        public virtual AbstractMenuItem Parent { get; set; }
        public virtual IList<AbstractMenuItem> Nodes { get; set; }

        public void AddNode(AbstractMenuItem nitem)
        {
            nitem.ParentID = ID;
            Nodes.Add(nitem);
        }
    }

    public class AbstractItemMenuConfigurations : EntityTypeConfiguration<AbstractMenuItem>
    {
        public AbstractItemMenuConfigurations()
        {
            ToTable("AbstractMenuItem");
            HasKey(x => x.ID);
            HasRequired(x => x.RouteEntity).WithMany().HasForeignKey(x => x.RouteID);
            HasOptional(x => x.Parent).WithMany(x => x.Nodes).HasForeignKey(z => z.ParentID);
        }
    }
}