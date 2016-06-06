using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels.Attachments
{
    public class AttachmentsProfile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<Attachment, AttachmentViewModel.Summary>();
            CreateMap<Photo, PhotoViewModel.Summary>().IncludeBase<Attachment, AttachmentViewModel.Summary>();
            CreateMap<NominationPhoto, NominantPhotoViewModel.Summary>();


            CreateMap<Video, VideoViewModel.Summary>().IncludeBase<Attachment, AttachmentViewModel.Summary>();
            CreateMap<Audio, AudioViewModel.Summary>().IncludeBase<Attachment, AttachmentViewModel.Summary>();
            CreateMap<Poll, PollViewModel.Summary>().IncludeBase<Attachment, AttachmentViewModel.Summary>();


            CreateMap<Attachment, AttachmentViewModel.Details>();
            CreateMap<Photo, PhotoViewModel.Details>().IncludeBase<Attachment, AttachmentViewModel.Details>();
            CreateMap<Video, VideoViewModel.Details>().IncludeBase<Attachment, AttachmentViewModel.Details>();
            CreateMap<Audio, AudioViewModel.Details>().IncludeBase<Attachment, AttachmentViewModel.Details>();
            CreateMap<Poll, PollViewModel.Details>().IncludeBase<Attachment, AttachmentViewModel.Details>();


            CreateMap<FileEntry, FileEntryViewModel.Summary>();
            CreateMap<FileEntry, FileEntryViewModel.Details>()
                .ForMember(x => x.Attachment, opt => opt.MapFrom(z => GetAttachment(z.Attachment)));


            //     base.Configure();
        }

        private static AttachmentViewModel.Details GetAttachment(Attachment attachment)
        {

            if (attachment == null)
                return null;

            if (attachment is Photo)
                return ((Photo)attachment).As<PhotoViewModel.Details>();

            if (attachment is Video)
                return ((Video)attachment).As<VideoViewModel.Details>();

            throw new NotImplementedException();
        }
    }
}
