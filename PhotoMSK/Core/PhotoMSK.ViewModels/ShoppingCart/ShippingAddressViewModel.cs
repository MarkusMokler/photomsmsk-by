using System;
using System.Collections.Generic;
using AutoMapper;
using PhotoMSK.Data.Models.ShoppingCart;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.ShoppingCart
{
    public class ShippingAddressViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public string Street1 { get; set; }
            public string Street2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public Guid UserInformationID { get; set; }
        }

        public class Details
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public string Street1 { get; set; }
            public string Street2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }

            public Guid UserInformationID { get; set; }

            public virtual UserInformationViewModel.Summary UserInformation { get; set; }
            public virtual ICollection<OrderViewModel.Summary> Orders { get; set; }
        }
    }

    public class ShippingAddressViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<ShippingAddress, ShippingAddressViewModel.Summary>();
            CreateMap<ShippingAddress, ShippingAddressViewModel.Details>();
        }
    }
}
