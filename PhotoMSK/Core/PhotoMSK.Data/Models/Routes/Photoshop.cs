using System.Collections.Generic;

namespace PhotoMSK.Data.Models.Routes
{
    public class Photoshop : RouteEntity
    {
        public string Name { get; set; }
        public int LastPositionCode { get; set; }

        public void AddPosition(PricePosition position)
        {
            LastPositionCode++;
            position.BarCode = LastPositionCode;

        }

        public double CurencyValue { get; set; }

        #region NavigatingProperties
        public virtual ICollection<PricePosition> Positions { get; set; }
        public virtual ICollection<ShopCategory> Categories { get; set; }
        #endregion

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