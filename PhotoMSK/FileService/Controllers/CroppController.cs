using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using FileService.Classes;
using FileService.Classes.Files;

namespace FileService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CroppController : ApiController
    {
        [HttpPost]
        public Object Post(CroppRequest formData)
        {
            var storage = new LocalStorage();
            var cropfactor = formData.ImgInitW / formData.ImgW;

            Size s;
            s = formData.ZoomFactor < 0.2 ? new Size { Width = formData.CropW, Height = formData.CropH } : new Size { Width = (int)(formData.CropW * formData.ZoomFactor), Height = (int)(formData.CropH * formData.ZoomFactor) };

            var @url = storage.CropImage(new Url(formData.ImgUrl), s,
                new Rectangle((int)(formData.ImgX1 * cropfactor), (int)(formData.ImgY1 * cropfactor), (int)Math.Round(formData.CropW * cropfactor), (int)Math.Round(formData.CropH * cropfactor)));

            return new { status = "success", url = @url.Url };
        }

        public class CroppRequest
        {
            public double ZoomFactor { get; set; }
            public string ImgUrl { get; set; }
            public int ImgInitW { get; set; }
            public int ImgInitH { get; set; }
            public double ImgW { get; set; }
            public double ImgH { get; set; }
            public int ImgY1 { get; set; }
            public int ImgX1 { get; set; }
            public int CropH { get; set; }
            public int CropW { get; set; }
        }
    }
}
