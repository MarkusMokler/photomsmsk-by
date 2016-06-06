using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class SnapshotViewModel
    {
        public class Summary
        {
            public DateTime SnapshotYear { get; set; }
        }

        public class Details
        {
            public virtual PhotoViewModel.Summary Photo { get; set; }
            public DateTime SnapshotYear { get; set; }
        }
    }

    public class SnapshotViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Snapshot, SnapshotViewModel.Summary>();
            CreateMap<Snapshot, SnapshotViewModel.Details>();
        }
    }
}
