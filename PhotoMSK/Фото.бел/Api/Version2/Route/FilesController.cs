using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fotobel.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Attachments;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Attachments;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2.Route
{
    public class FilesController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string shortcut, Guid? directory = null, int pageSize = 20, int page = 0)
        {
            var direcotoryID = directory ?? Guid.Empty;
            var route = _context.Routes.Single(x => x.Shortcut == shortcut);
            if (directory == null)
            {
                if (route.RootDirectoryID != null)
                    direcotoryID = route.RootDirectoryID.Value;
                else
                {
                    route.RootDirectory = new FileEntry()
                    {
                        ID = Guid.NewGuid(),
                        Type = FileEntryType.Folder
                    };
                    _context.SaveChanges();
                    return Ok(new PageView<FileEntry>()
                    {
                        Elements = new List<FileEntry>(),
                        ItemsCount = 0,
                        PageSize = 20
                    });
                }
            }
            var query = _context.FileEntries.Where(x => x.ParentEntryID == direcotoryID);

            return Ok(new PageView<FileEntryViewModel.Details>()
            {
                ItemsCount = query.Count(),
                PageSize = pageSize,
                Elements =
                    query.OrderBy(x => x.Type)
                        .Skip(page * pageSize)
                        .Take(pageSize)
                        .ToList()
                        .As<IList<FileEntryViewModel.Details>>()
            });
        }

        [HttpGet]
        public IHttpActionResult Get(string shortcut, bool foldersOnly, Guid? directory = null)
        {
            var direcotoryID = directory ?? Guid.Empty;
            var route = _context.Routes.Single(x => x.Shortcut == shortcut);
            if (directory == null)
            {
                if (route.RootDirectoryID != null)
                    direcotoryID = route.RootDirectoryID.Value;
                else
                {
                    route.RootDirectory = new FileEntry()
                    {
                        ID = Guid.NewGuid(),
                        Type = FileEntryType.Folder
                    };
                    _context.SaveChanges();
                    return Ok(new PageView<FileEntry>()
                    {
                        Elements = new List<FileEntry>(),
                        ItemsCount = 0,
                        PageSize = 20
                    });
                }
            }
            var query =
                _context.FileEntries.Where(x => x.ParentEntryID == direcotoryID && x.Type == FileEntryType.Folder);

            return Ok(query.ToList());
        }

        [HttpPost]
        public IHttpActionResult Post(string shortcut, List<FileUploadModel> model)
        {

            var route = _context.Routes.Single(x => x.Shortcut == shortcut);

            var parentFolder = model.First().ParentFolderid ?? route.RootDirectoryID.Value;

            foreach (var fileUploadModel in model)
            {
                switch (fileUploadModel.Type)
                {
                    case "folder":
                        {
                            _context.FileEntries.Add(new FileEntry()
                            {
                                ID = Guid.NewGuid(),
                                Name = fileUploadModel.FileName,
                                ParentEntryID = parentFolder,
                                Type = FileEntryType.Folder,

                            });
                            break;
                        }
                    case "photo":
                        {
                            _context.FileEntries.Add(new FileEntry()
                            {
                                ID = Guid.NewGuid(),
                                Name = fileUploadModel.FileName,
                                ParentEntryID = parentFolder,
                                Type = FileEntryType.File,
                                Attachment = new Photo()
                                {
                                    ID = Guid.NewGuid(),
                                    Description = "",
                                    Title = "",
                                    FileName = fileUploadModel.FileName,
                                    UploadDate = DateTime.Now,
                                    Url = fileUploadModel.Url
                                }

                            });
                            break;
                        }
                    default:
                        throw new ValidationException("Unsuported type", MessageCodes.OkAction);
                }

            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(string shortcut, Guid id, FileUploadModel model)
        {
            var entity = _context.FileEntries.Find(id);
            entity.Name = model.Title;
            if (model.ParentFolderid != null)
                entity.ParentEntryID = model.ParentFolderid.Value;
            _context.SaveChanges();
            return Ok();
        }

        [AcceptVerbs("HTTPDELETE")]
        [HttpDelete]
        public IHttpActionResult Delete(string shortcut, Guid id)
        {
            RemoveAll(new List<Guid> { id });
            _context.SaveChanges();
            return Ok();
        }

        private void RemoveAll(ICollection<Guid> guids)
        {
            var fileEntries = _context.FileEntries.Where(x => guids.Contains(x.ID)).ToList();
            var list = fileEntries.Select(x => x.ID).ToList();
            if (list.Any())
                RemoveAll(list);

            foreach (var fileEntry in fileEntries)
            {
                _context.Entry(fileEntry).State = EntityState.Deleted;
            }
        }


        public class FileUploadModel
        {
            public string FileName;
            public string Url;
            public string Title;
            public string Type;
            public Guid? ParentFolderid;
        }

    }
}

