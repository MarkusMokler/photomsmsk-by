using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models.ShoppingCart;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.ShoppingCart
{
    public static class OrderViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public Guid StatusID { get; set; }
            public Guid UserInformationID { get; set; }
            public Guid? ShippingAdressID { get; set; }

            public DateTime OrderDate { get; set; }
            public double Total { get; set; }
            public double ShippingCost { get; set; }
        }

        public class Details : Summary
        {
            public virtual ShippingAddressViewModel.Summary ShippingAddress { get; set; }
            public virtual UserInformationViewModel.Summary UserInformation { get; set; }
            public virtual ICollection<OrderLineViewModel.Summary> OrderLines { get; set; }
            public virtual StatusViewModel.Summary Status { get; set; }
        }
    }

    public class OrderViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Order, OrderViewModel.Summary>();
            CreateMap<Order, OrderViewModel.Details>();
        }
    }
}
