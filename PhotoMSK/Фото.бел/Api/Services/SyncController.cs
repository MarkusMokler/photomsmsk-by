using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using Fotobel.Classes;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using PhotoMSK.Data;

namespace Fotobel.Api.Services
{
    public class SyncController : ApiController
    {
        AppContext _context = new AppContext();
        private readonly ILuceneProvider _provider;

        string CLIENT_ID = "532937511847-200puo7uscb5e0kl3gj17b8fkom0ki35.apps.googleusercontent.com";

        // This is the OAuth 2.0 Client Secret retrieved
        // above.  Be sure to store this value securely.  Leaking this
        // value would enable others to act on behalf of your application!
        string CLIENT_SECRET = "Dv7ReLzzeMSNgocPmY5mpIRs";

        // Space separated list of scopes for which to request access.
        string SCOPE = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds";

        public SyncController(ILuceneProvider provider)
        {
            _provider = provider;
        }

        // This is the Redirect URI for installed applications.
        // If you are building a web application, you have to set your
        // Redirect URI at https://code.google.com/apis/console.


        public IHttpActionResult Get()
        {
            var data = _context.GoogleSheetsSync.First();

            SpreadsheetsService service = new SpreadsheetsService("photomsk");

            // TODO: Authorize the service object for a specific user (see other sections)
            GOAuth2RequestFactory requestFactory =
         new GOAuth2RequestFactory(null, "photomsk", new OAuth2Parameters()
         {
             ClientId = CLIENT_ID,
             ClientSecret = CLIENT_SECRET,
             Scope = SCOPE,
             AccessToken = data.Token,
             RefreshToken = data.RefreshToken
         });
            service.RequestFactory = requestFactory;

            // Instantiate a SpreadsheetQuery object to retrieve spreadsheets.
            SpreadsheetQuery query = new SpreadsheetQuery();
            //query.Uri =new Uri(
            //    "https://docs.google.com/spreadsheets/d/17ed5E9dC2MDYRw5LR5OjWpVBU1gWwkg32JlCg24snpc/edit");

            // Make a request to the API and get all spreadsheets.
            SpreadsheetFeed feed = service.Query(query);
            if (feed.Entries.Count == 0)
            {
                // TODO: There were no spreadsheets, act accordingly.
            }

            // TODO: Choose a spreadsheet more intelligently based on your
            // app's needs.
            SpreadsheetEntry spreadsheet = (SpreadsheetEntry)feed.Entries.FirstOrDefault(x => x.Id == new AtomId("https://spreadsheets.google.com/feeds/spreadsheets/private/full/193lb-hbvfgQT-mJrasaYdDTPkw41VheDtelgPyPiCtI"));

            // Make a request to the API to fetch information about all
            // worksheets in the spreadsheet.
            var wsFeed = spreadsheet.Worksheets;
            foreach (var atomEntry in wsFeed.Entries)
            {
                AtomLink listFeedLink = atomEntry.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

                // Fetch the list feed of the worksheet.
                ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
                ListFeed listFeed = service.Query(listQuery);
                foreach (ListEntry row in listFeed.Entries)
                {
                    // Print the first column's cell value
                    Console.WriteLine(row.Title.Text);
                    // Iterate over the remaining columns, and print each cell value
                    foreach (ListEntry.Custom element in row.Elements)
                    {

                        var val = element.Value;
                    }
                }

            }

            // Iterate through each worksheet in the spreadsheet.
            return Ok();
        }

        public IHttpActionResult Get(bool compare)
        {
            var data = _context.GoogleSheetsSync.First();

            SpreadsheetsService service = new SpreadsheetsService("photomsk");

            // TODO: Authorize the service object for a specific user (see other sections)
            GOAuth2RequestFactory requestFactory =
         new GOAuth2RequestFactory(null, "photomsk", new OAuth2Parameters()
         {
             ClientId = CLIENT_ID,
             ClientSecret = CLIENT_SECRET,
             Scope = SCOPE,
             AccessToken = data.Token,
             RefreshToken = data.RefreshToken
         });
            service.RequestFactory = requestFactory;

            // Instantiate a SpreadsheetQuery object to retrieve spreadsheets.
            SpreadsheetQuery query = new SpreadsheetQuery();
            //query.Uri =new Uri(
            //    "https://docs.google.com/spreadsheets/d/17ed5E9dC2MDYRw5LR5OjWpVBU1gWwkg32JlCg24snpc/edit");

            // Make a request to the API and get all spreadsheets.
            SpreadsheetFeed feed = service.Query(query);
            if (feed.Entries.Count == 0)
            {
                // TODO: There were no spreadsheets, act accordingly.
            }

            // TODO: Choose a spreadsheet more intelligently based on your
            // app's needs.
            SpreadsheetEntry spreadsheet = (SpreadsheetEntry)feed.Entries.FirstOrDefault(x => x.Id == new AtomId("https://spreadsheets.google.com/feeds/spreadsheets/private/full/17ed5E9dC2MDYRw5LR5OjWpVBU1gWwkg32JlCg24snpc"));

            // Make a request to the API to fetch information about all
            // worksheets in the spreadsheet.
            var wsFeed = spreadsheet.Worksheets;
            foreach (var atomEntry in wsFeed.Entries)
            {
                AtomLink listFeedLink = atomEntry.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

                // Fetch the list feed of the worksheet.
                ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
                ListFeed listFeed = service.Query(listQuery);
                foreach (ListEntry row in listFeed.Entries)
                {
                    // Print the first column's cell value
                    Console.WriteLine(row.Title.Text);
                    // Iterate over the remaining columns, and print each cell value

                    ListEntry.Custom elem = null;
                    ListEntry.Custom msk = null;


                    foreach (ListEntry.Custom ee in row.Elements)
                    {
                        if (ee.LocalName == "name")
                            elem = ee;
                        if (ee.LocalName == "msk-id")
                            msk = ee;

                    }
                    if (!string.IsNullOrEmpty(msk.Value))
                        continue;


                    if (elem == null) continue;

                    var q = _provider.ParseQuery(new[] { "Name" },

                        string.Format("Name:*{0}*", Regex.Replace(string.Join(" ", elem.Value.Split(' ').Skip(1)), @"\([^)]*\)", "").Replace(" ", "*").Replace(":", "*").Replace("[", "").Replace("]", "").Replace(",", ".").Replace("\"", "").ToLower()));

                    var results = _provider.Searcher.Search(q, 50);

                    var guids = results.ScoreDocs.Select(x => new Guid(_provider.Searcher.Doc(x.Doc).Get("ID")));

                    if (guids.Any())
                    {
                        foreach (ListEntry.Custom ee in row.Elements)
                        {
                            if (ee.LocalName == "msk-id")
                                ee.Value = guids.First().ToString();
                        }
                        row.Update();
                    }
                }

            }

            // Iterate through each worksheet in the spreadsheet.
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}
