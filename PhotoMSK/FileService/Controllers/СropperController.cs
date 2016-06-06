using System;
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
    public class CropperController : ApiController
    {
        [HttpPost]
        public async Task<Object> Post()
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

            var imageInformationList = files.Select(storage.SaveImages).First();

            return new
            {
                status = "success",
                largeImage = new
                {
                    url = imageInformationList.LargeImage.Url,
                    width = imageInformationList.LargeImage.Width,
                    height = imageInformationList.LargeImage.Height
                },
                mediumImage = new
                {
                    url = imageInformationList.MediumImage.Url,
                    width = imageInformationList.MediumImage.Width,
                    height = imageInformationList.MediumImage.Height
                },
                smallImage = new
                {
                    url = imageInformationList.SmallImage.Url,
                    width = imageInformationList.SmallImage.Width,
                    height = imageInformationList.SmallImage.Height
                }
            };
        }
    }
}
