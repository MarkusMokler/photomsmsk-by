using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CsvHelper.Configuration;

namespace PhotoMSK.Models.EditViewModel
{
    public class OnlinerProductModel
    {
        public string Category { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string Warranty { get; set; }
        public string Delivery { get; set; }
        public string IsCashless { get; set; }
        public string IsCredit { get; set; }
    }

    public class OnlinerProductModelMap : CsvClassMap<OnlinerProductModel>
    {
        public OnlinerProductModelMap()
        {
            base.Map(m => m.Category).Index(0);
            base.Map(x => x.Vendor).Index(1);
            base.Map(x => x.Model).Index(2);
            base.Map(x => x.Price).Index(3);
            base.Map(x => x.Currency).Index(4);
            base.Map(x => x.Status).Index(5);
            base.Map(x => x.Comment).Index(6);
            base.Map(m => m.Warranty).Index(7);
            base.Map(m => m.Delivery).Index(8);
            base.Map(m => m.IsCashless).Index(9);
            base.Map(m => m.IsCredit).Index(10);
        }
    }
}