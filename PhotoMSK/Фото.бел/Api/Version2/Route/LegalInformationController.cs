using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fotobel.Classes;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2.Route
{
    public class LegalInformationController : ApiController
    {
        readonly AppContext _context = new AppContext();


        public IHttpActionResult Get(string shortcut)
        {
            var li = _context.Routes.Single(x => x.Shortcut == shortcut).LegalInformation;
            return Ok(li == null ? new LegalInformationViewModel.Summary() : li.As<LegalInformationViewModel.Summary>());
        }

        [Authorize]
        public IHttpActionResult Post(string shortcut, LegalInformationViewModel.Summary model)
        {
            var route = _context.Routes.Single(x => x.Shortcut == shortcut);
            if (route.LegalInformation == null)
                route.LegalInformation = new LegalInformation
                {
                    ID = Guid.NewGuid(),
                    CreatedTime = DateTime.Now,
                };

            route.LegalInformation.AllowOnlineBooking = model.AllowOnlineBooking;
            route.LegalInformation.AccountNumber = model.AccountNumber;
            route.LegalInformation.Legaladdress = model.Legaladdress;
            route.LegalInformation.PublicOffer = model.PublicOffer;
            if (model.RegisterDate.HasValue)
                route.LegalInformation.RegisterDate = model.RegisterDate;
            route.LegalInformation.RegisterTrade = model.RegisterTrade;
            route.LegalInformation.RegisteringAgency = model.RegisteringAgency;
            route.LegalInformation.CertificateNumber = model.CertificateNumber;

            route.LegalInformation.LegalName = model.LegalName;
            route.LegalInformation.CEO = model.CEO;


            route.LegalInformation.ApplyModification(User.GetUserInformationID());
            _context.SaveChanges();
            return Ok(route.LegalInformation.As<LegalInformationViewModel.Summary>());
        }
    }
}
