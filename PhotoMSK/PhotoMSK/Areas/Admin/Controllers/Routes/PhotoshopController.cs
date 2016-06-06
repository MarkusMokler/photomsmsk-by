using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.Routes;
using PhotoMSK.Extensiolns;
using PhotoMSK.Models;
using PhotoMSK.Models.EditViewModel.Routes;


namespace PhotoMSK.Areas.Admin.Controllers.Routes
{
    [Authorize]
    public class PhotoshopController : Controller
    {
        string CLIENT_ID = "532937511847-200puo7uscb5e0kl3gj17b8fkom0ki35.apps.googleusercontent.com";

        // This is the OAuth 2.0 Client Secret retrieved
        // above.  Be sure to store this value securely.  Leaking this
        // value would enable others to act on behalf of your application!
        string CLIENT_SECRET = "Dv7ReLzzeMSNgocPmY5mpIRs";

        // Space separated list of scopes for which to request access.
        string SCOPE = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds";

        // This is the Redirect URI for installed applications.
        // If you are building a web application, you have to set your
        // Redirect URI at https://code.google.com/apis/console.



        private readonly AppContext _context = new AppContext();
        public ActionResult Index(string shortcut)
        {
            var studio = _context.Photoshops.Single(x => x.Shortcut == shortcut);
            return View(studio);
        }
        public ActionResult Create()
        {
            return View();
        }
        public async Task<ActionResult> Edit(string shortcut)
        {
            if (shortcut == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photoshop photoshop = await _context.Photoshops.SingleOrDefaultAsync(x => x.Shortcut == shortcut);
            if (photoshop == null)
            {
                return HttpNotFound();
            }
            return View(photoshop.As<PhotoshopEditModel>());
        }

        public ActionResult Authorize()
        {
            {
                ////////////////////////////////////////////////////////////////////////////
                // STEP 1: Configure how to perform OAuth 2.0
                ////////////////////////////////////////////////////////////////////////////

                // TODO: Update the following information with that obtained from
                // https://code.google.com/apis/console. After registering
                // your application, these will be provided for you.


                ////////////////////////////////////////////////////////////////////////////
                // STEP 2: Set up the OAuth 2.0 object
                ////////////////////////////////////////////////////////////////////////////
                string REDIRECT_URI = "http://localhost:11551" + Url.Action("SubmitAuthorize", new { controller = "photoshop", area = "admin" });
                // OAuth2Parameters holds all the parameters related to OAuth 2.0.
                OAuth2Parameters parameters = new OAuth2Parameters
                {
                    ClientId = CLIENT_ID,
                    ClientSecret = CLIENT_SECRET,
                    RedirectUri = REDIRECT_URI,
                    Scope = SCOPE,
                };

                // Set your OAuth 2.0 Client Id (which you can register at
                // https://code.google.com/apis/console).

                // Set your OAuth 2.0 Client Secret, which can be obtained at
                // https://code.google.com/apis/console.

                // Set your Redirect URI, which can be registered at
                // https://code.google.com/apis/console.

                ////////////////////////////////////////////////////////////////////////////
                // STEP 3: Get the Authorization URL
                ////////////////////////////////////////////////////////////////////////////

                // Set the scope for this particular service.

                // Get the authorization url.  The user of your application must visit
                // this url in order to authorize with Google.  If you are building a
                // browser-based application, you can redirect the user to the authorization
                // url.
                string authorizationUrl = OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);

                ////////////////////////////////////////////////////////////////////////////
                // STEP 4: Get the Access Token
                ////////////////////////////////////////////////////////////////////////////

                // Once the user authorizes with Google, the request token can be exchanged
                // for a long-lived access token.  If you are building a browser-based
                // application, you should parse the incoming request token from the url and
                // set it in OAuthParameters before calling GetAccessToken().


                ////////////////////////////////////////////////////////////////////////////
                // STEP 5: Make an OAuth authorized request to Google
                ////////////////////////////////////////////////////////////////////////////


                // Make the request to Google
                // See other portions of this guide for code to put here...
                return Redirect(authorizationUrl);
            }
        }

        public ActionResult SubmitAuthorize(string code)
        {
            string REDIRECT_URI = "http://localhost:11551" + Url.Action("SubmitAuthorize", new { controller = "photoshop", area = "admin" });
            OAuth2Parameters parameters = new OAuth2Parameters
            {
                ClientId = CLIENT_ID,
                ClientSecret = CLIENT_SECRET,
                RedirectUri = REDIRECT_URI,
                Scope = SCOPE,
                AccessCode = code
            };
            OAuthUtil.GetAccessToken(parameters);
            string accessToken = parameters.AccessToken;
            var shet = _context.GoogleSheetsSync.Find(Guid.Empty);
            if (shet == null)
            {
                shet = new GoogleSheetsSync()
                {
                    ID = Guid.Empty,
                    Token = accessToken,
                    RefreshToken = parameters.RefreshToken
                };
                _context.GoogleSheetsSync.Add(shet);
            }
            else
            {
                shet.Token = accessToken;
                shet.RefreshToken = parameters.RefreshToken;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}