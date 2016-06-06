using Fotobel.Classes;
using Fotobel.Models;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Routes.Photographer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Fotobel.Api.Version2
{
    public class ProjectController : ApiController
    {
        private readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get()
        {
            var projects = _context.Projects.ToList().As<IList<ProjectViewModel.Details>>();
            return Ok(projects);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            var primaryQuery = _context.Projects.SingleOrDefault(x => x.ID == id).As<ProjectViewModel.Details>();
            return Ok(primaryQuery);
        }

        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {
            // Получаем ID фотографа с указанным shortcut
            var photographer = _context.Photographers.SingleOrDefault(x => x.Shortcut == shortcut);
            // Получаем все проекты фотографа с указанным ID
            var primaryQuery =
                _context.Projects.Where(x => x.PhotographerID == photographer.ID).As<IList<ProjectViewModel.Summary>>();

            return Ok(primaryQuery);
        }

        [HttpPut]
        public IHttpActionResult Put(Guid id, Proxy<Project> model)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var route = _context.Projects.Find(id);
            model.Patch(route);
            _context.SaveChanges();
            return Ok(model);
        }

        public class ProjectModel
        {
            public string Name;
            public int Price;
            public string Description;
            public string Shortcut;
        }

        [HttpPost]
        public IHttpActionResult Post(ProjectModel model)
        {
            var photographer = _context.Photographers.SingleOrDefault(x => x.Shortcut == model.Shortcut);

            _context.Projects.Add(new Project
            {
                ID = Guid.NewGuid(),
                Name = model.Name,
                Photographer = photographer,
                Price = model.Price,
                Description = model.Description
            });
           
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(string shortcut, Guid id)
        {
            var userId = User.GetUserInformationID();
            var route = _context.Photographers.Single(x => x.Shortcut == shortcut);
            if (
                !route.Participants.Any(
                    z =>
                        z.AccessStatus == AccessStatus.Owner ||
                        z.AccessStatus == AccessStatus.Administrator && z.UserInformation.ID == userId))
                throw new ValidationException("Не достаточно прав для выполнения данной операции", MessageCodes.OkAction);

            var project = _context.Projects.Find(id);
            _context.Entry(project).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ok();
        }
    }
}