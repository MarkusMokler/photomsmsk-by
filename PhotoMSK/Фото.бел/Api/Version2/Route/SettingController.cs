using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoMSK.Data;

namespace Fotobel.Api.Version2.Route
{
    public class SettingController : ApiController
    {
        readonly AppContext _context = new AppContext();

        [HttpGet]
        public IHttpActionResult Get(string shortcut)
        {

            var settings = _context.Routes.Single(x => x.Shortcut == shortcut).RouteSettings;


            var allSettings = _context.Settings.Select(y =>
                new SettingViewModel
                {
                    SettingID = y.ID,
                    SettingName = y.SettingName,
                    SettingDescription = y.Description,

                }
             );


            foreach (var settingViewModel in allSettings)
            {
                var single = settings.SingleOrDefault(x => x.SettingID == settingViewModel.SettingID);

                if (single == null) continue;
                settingViewModel.ID = single.ID;
                settingViewModel.Ennabled = single.Ennabled;
                settingViewModel.SettingValue = single.SettingValue;
            }

            return Ok(allSettings);
        }

        [HttpPost]
        public IHttpActionResult Post(string shortcut)
        {

            return Ok();
        }
    }

    public class SettingViewModel
    {
        public Guid? ID { get; set; }
        public bool? Ennabled { get; set; }
        public Guid SettingID { get; set; }
        public string SettingName { get; set; }
        public string SettingDescription { get; set; }
        public string SettingValue { get; set; }
    }
}
