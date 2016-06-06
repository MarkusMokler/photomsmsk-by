using System;
using AutoMapper;
using PhotoMSK.Data.Models.ShoppingCart;
using PhotoMSK.ViewModels.Route;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.ShoppingCart
{
    public class OrderLineViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public Guid PhototechnicsID { get; set; }
            public Guid OrderID { get; set; }

            public double UnitPrice { get; set; }
            public double Price { get; set; }
            public int Count { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public Guid PhototechnicsID { get; set; }
            public Guid OrderID { get; set; }

            public double UnitPrice { get; set; }
            public double Price { get; set; }
            public int Count { get; set; }


            public virtual OrderViewModel.Summary Order { get; set; }
            public virtual PhototechnicsViewModel.Summary Phototechnics { get; set; }
        }
    }

    public class OrderLineViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<OrderLine, OrderLineViewModel.Summary>();
            CreateMap<OrderLine, OrderLineViewModel.Details>();
        }
    }
}
