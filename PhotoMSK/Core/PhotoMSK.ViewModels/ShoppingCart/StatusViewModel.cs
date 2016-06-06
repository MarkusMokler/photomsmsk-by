using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models.ShoppingCart;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.ShoppingCart
{
    public class StatusViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public virtual List<OrderViewModel.Summary> Orders { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }
    }

    public class StatusViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Status, StatusViewModel.Summary>();
            CreateMap<Status, StatusViewModel.Details>();
        }
    }
}
