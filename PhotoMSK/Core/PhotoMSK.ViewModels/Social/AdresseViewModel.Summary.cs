using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static partial class AdresseViewModel
    {
        public class Summary
        {
            public virtual Guid ID { get; set; }
            public string FromID { get; set; }
            public string ToID { get; set; }
            public bool Readed { get; set; }
        }        
    }

    public class AdresseViewModelSummaryProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Adresse, AdresseViewModel.Summary>();
            CreateMap<Adresse, AdresseViewModel.Details>();
        }
    }
}
