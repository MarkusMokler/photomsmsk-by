using System;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static partial class MessageViewModel
    {
        public class Summary
        {
            public virtual Guid ID { get; set; }
            public DateTime Time { get; set; }
            public string Text { get; set; }
        }
    }

    public class MessageViewModelProfile :MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Message, MessageViewModel.Summary>();
            CreateMap<Message, MessageViewModel.Details>();
        }
    }
}
