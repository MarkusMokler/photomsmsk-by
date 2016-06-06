using System;
using System.Web.Mvc;
using AutoMapper;
using PhotoMSK.Areas.Default.ViewData.Wall;
using PhotoMSK.Data;
using PhotoMSK.Extensiolns;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Interfaces;

namespace PhotoMSK.Areas.Default.Controllers
{
    public class WallController : Controller
    {
        private readonly IWallService _wallService;

        public WallController(IWallService wallService)
        {
            _wallService = wallService;
        }

        // GET: Default/Wall
        public ActionResult Index(Guid id)
        {
            try
            {
                var model = new IndexViewData { Item = _wallService.GetDetails(id) };
                return View(model);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

    }
}