using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using FileService.Classes;
using FileService.Classes.Files;

namespace FileService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FilesController : ApiController
    {

        [HttpPost]
        public async Task<List<FileInformation>> Post()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Request.Content.LoadIntoBufferAsync().Wait();
            var provider = await Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());

            //access form data
            var formData = provider.FormData;

            //access files
            //        IList<HttpContent> fileContentList = provider.Files;

            var fileDataList = provider.GetFiles();

            // get the files 
            var files = await fileDataList;

            var storage = new LocalStorage();

            return new List<FileInformation>(files.Select(storage.SaveFile));
        }
    }
}