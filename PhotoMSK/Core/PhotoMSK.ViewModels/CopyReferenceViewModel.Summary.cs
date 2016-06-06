using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static partial class CopyReferenceViewModel
    {
        public class Summary
        {
            public Guid ID { get; set; }
            public int CopyFrom { get; set; }
            public int LastCopiedID { get; set; }
            public DateTime LastCollectTime { get; set; }
        }
    }
    public class CopyReferenceViewModelSummaryProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<CopyReference, CopyReferenceViewModel.Summary>();
            CreateMap<CopyReference, CopyReferenceViewModel.Details>();
        }
    }
}
