using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Fotobel.Models;
using Microsoft.AspNet.Identity;
using PhotoMSK.Data;
using PhotoMSK.Data.Enums;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels;
using PhotoMSK.ViewModels.Event;
using PhotoMSK.ViewModels.Realisation;

namespace Fotobel.Api.Version2.UserInformation
{
    public class CardController : ApiController
    {

        readonly AppContext _context = new AppContext();
        public IHttpActionResult Get(Guid shortcut)
        {
            return Ok(_context.SaleCards.Where(x => x.UserInformationID == shortcut).As<IList<SaleCardViewModel.Summary>>());
        }
        public IHttpActionResult Post(Guid shortcut, Proxy<SaleCard> model)
        {
            SaleCard card = new SaleCard();
            model.Patch(card);
            card.ID = Guid.NewGuid();
            card.UserInformationID = shortcut;
            _context.SaleCards.Add(card);
            _context.SaveChanges();
            return Ok();
        }
    }
}