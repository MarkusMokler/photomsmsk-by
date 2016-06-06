using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Attachments;
using Vk.SDK;
using Vk.SDK.Interfaces;

namespace Fotobel.Api.Services
{
    public class CollectController : ApiController
    {
        private readonly IWallService _wallService;

        public CollectController(IWallService wallService)
        {
            this._wallService = wallService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            WebRequest.Create("http://photomsk.by/api/GoogleCalendar/").GetResponse();
            var time = DateTime.Now.AddHours(-1).AddMinutes(-30);
            CopyReference reference;
            using (var context = new AppContext())
            {
                reference = context.CopyReferenses.FirstOrDefault(x => x.LastCollectTime < time);
                if (reference == null)
                    return Ok();
                reference.LastCollectTime = DateTime.Now;
                context.SaveChanges();

            }

            var posts = _wallService.Get(new VKParameters()
            {
                {Vk.SDK.VKApiConst.OWNER_ID, reference.CopyFrom},
                {"filter", "owner"},
                {Vk.SDK.VKApiConst.COUNT, 10}
            }).GetModel();

            var addeds = posts.Items.Where(x => x.id > reference.LastCopiedID && x.post_type != "copy").ToList();

            if (addeds.Count > 0)
            {
                using (var context = new AppContext())
                {
                    reference = context.CopyReferenses.Find(reference.ID);
                    reference.LastCopiedID = addeds.Max(x => x.id);

                    var route = reference.Route;
                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                    foreach (var added in addeds)
                    {
                        if (added.Attachments == null)
                            added.Attachments = new List<Vk.SDK.Model.Attachment>();
                        route.AddWallPost(new Views()
                        {
                            ID = Guid.NewGuid(),
                            Date = start.AddSeconds(added.date).ToLocalTime(),
                            ParticipationType = ParticipationType.Owner,
                            WallPost = new WallPost()
                            {
                                ID = Guid.NewGuid(),
                                Date = start.AddSeconds(added.date).ToLocalTime(),
                                Desctiption = added.text,
                                Attacments = added.Attachments.OfType<Vk.SDK.Model.AttachmentWarpers.PhotoWarper>()
                                .Select(x => new Photo { ID = Guid.NewGuid(), Description = x.Photo.text, Url = this.GetMax(x.Photo), UploadDate = start.AddSeconds(x.Photo.date).ToLocalTime() })
                                .ToList<Attachment>()
                            }
                        });
                    }

                    context.SaveChanges();
                }
            }

            return Ok();
        }

        private string GetMax(Vk.SDK.Model.Photo photo)
        {
            if (!string.IsNullOrEmpty(photo.photo_2560))
                return photo.photo_2560;

            if (!string.IsNullOrEmpty(photo.photo_1280))
                return photo.photo_1280;

            if (!string.IsNullOrEmpty(photo.photo_807))
                return photo.photo_807;

            if (!string.IsNullOrEmpty(photo.photo_604))
                return photo.photo_604;

            if (!string.IsNullOrEmpty(photo.photo_130))
                return photo.photo_130;

            if (!string.IsNullOrEmpty(photo.photo_75))
                if (!string.IsNullOrEmpty(photo.photo_75))
                    return photo.photo_75;
            return "";
        }
    }
}
