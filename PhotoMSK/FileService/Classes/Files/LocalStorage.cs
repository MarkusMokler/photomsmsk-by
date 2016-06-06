using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web.Hosting;
using Extension;

namespace FileService.Classes.Files
{
    public class LocalStorage : IStorage
    {
        private enum  Sizes
        {
            Small, Medium, Large
        }

        private static readonly Dictionary<Sizes, Size> SizeDictionary = new Dictionary<Sizes, Size>()
        {
            { Sizes.Small, new Size(480, 360)},
            { Sizes.Medium, new Size(960, 720)},
            { Sizes.Large, new Size(1920, 1440)}
        };

        public FileInformation SaveFile(FileData fileData)
        {
            string extension = Path.GetExtension(fileData.FileName);

            string mount = HostingEnvironment.MapPath("~");

            var idetity = getHashSha256(fileData.Data);

            var dir = idetity.ToFilePath();


            var fulldir = Path.Combine(mount, dir);

            if (!Directory.Exists(fulldir))
                Directory.CreateDirectory(fulldir);

            string path = Path.Combine(fulldir, idetity + extension);

            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                file.Write(fileData.Data, 0, fileData.Data.Length);
                file.Close();
            }
            return new FileInformation { ServerName = ConfigurationManager.AppSettings["ServerName"], FileName = idetity, Extension = extension, Url = ConfigurationManager.AppSettings["ServerName"] + dir + "/" + idetity + extension };
        }

        public ImageFileInformation SaveImages(FileData fileData)
        {
            return new ImageFileInformation
            {
                SmallImage = SaveImage(fileData, SizeDictionary[Sizes.Small]),
                MediumImage = SaveImage(fileData, SizeDictionary[Sizes.Medium]),
                LargeImage = SaveImage(fileData, SizeDictionary[Sizes.Large])
            };
        }

        private FileInformation SaveImage(FileData fileData, Size size)
        {
            string mount = HostingEnvironment.MapPath("~");

            Image img = new Bitmap(new MemoryStream(fileData.Data));

            var asp = getAspect(img, size.Width, size.Height);

            var newImage = ResizeImage(img, asp.Width, asp.Height);

            var stream = new MemoryStream();

            // EncoderParameter object in the array.
            var myEncoderParameters = new EncoderParameters(1);
            var encoder = GetEncoder(ImageFormat.Jpeg);
            var myEncoderParameter = new EncoderParameter(Encoder.Quality, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            newImage.Save(stream, encoder, myEncoderParameters);

            var bytes = stream.ToArray();

            var idetity = getHashSha256(bytes);

            var dir = idetity.ToFilePath();

            var fulldir = Path.Combine(mount, dir);

            if (!Directory.Exists(fulldir))
                Directory.CreateDirectory(fulldir);

            string path = Path.Combine(fulldir, idetity + ".jpg");

            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                file.Write(bytes, 0, bytes.Length);
                file.Close();
            }
            return new FileInformation { ServerName = ConfigurationManager.AppSettings["ServerName"], FileName = idetity, Extension = ".jpg", Url = ConfigurationManager.AppSettings["ServerName"] + dir + "/" + idetity + ".jpg", Width = newImage.Width, Height = newImage.Height };

        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private static bool IsImage(FileData file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" }; // add more if u like...

            // linq from Henrik Stenbæk
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        private string getHashSha256(byte[] bytes)
        {
            var hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            return hash.Aggregate(string.Empty, (current, x) => current + String.Format("{0:x2}", x));
        }

        public string GetUrl(Guid id)
        {
            return string.Format(ConfigurationManager.AppSettings["GetUrl"], id);
        }

        public byte[] GetContent(FileInformation file)
        {
            var root = ConfigurationManager.AppSettings["StorageRoot"];

            string mount = HostingEnvironment.MapPath("~");


            var path = Path.Combine(mount, root, file.FileName);

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return fs.ReadToArray();
            }

        }

        static Size getAspect(Image image, int resizeMaxWidth, int resizeMaxHeight)
        {
            double maxAspect = (double)resizeMaxWidth / (double)resizeMaxHeight;
            double aspect = (double)image.Width / (double)image.Height;

            if (maxAspect > aspect && image.Width > resizeMaxWidth)
            {
                //Width is the bigger dimension relative to max bounds
                int resizeWidth = resizeMaxWidth;
                var resizeHeight = (int)(resizeMaxWidth / aspect);
                return new Size(resizeWidth, resizeHeight);
            }
            if (maxAspect <= aspect && image.Height > resizeMaxHeight)
            {
                //Height is the bigger dimension
                int resizeHeight = resizeMaxHeight;
                int resizeWidth = (int)(resizeMaxHeight * aspect);
                return new Size(resizeWidth, resizeHeight);
            }

            return new Size(image.Width, image.Height);
        }

        static Image ResizeImage(Image image, int desWidth, int desHeight)
        {

            var bmp = new Bitmap(desWidth, desHeight);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(image, new Rectangle(0, 0, desWidth, desHeight), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            }

            return bmp;
        }

        private static Image CropImage(Image image, Size size, Rectangle rectangle)
        {
            var bmp = new Bitmap(size.Width, size.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(image, new Rectangle(0, 0, size.Width, size.Height), rectangle, GraphicsUnit.Pixel);

            }

            return bmp;
        }

        public FileInformation CropImage(Url url, Size size, Rectangle rectangle)
        {
            string mount = HostingEnvironment.MapPath("~");
            var path = Path.Combine(mount, url.Value.Replace(ConfigurationManager.AppSettings["ServerName"], ""));

            Image b = Image.FromFile(path);

            var bb = CropImage(b, size, rectangle);
            b.Dispose();
            // EncoderParameter object in the array.
            var myEncoderParameters = new EncoderParameters(1);
            var encoder = GetEncoder(ImageFormat.Jpeg);
            var myEncoderParameter = new EncoderParameter(Encoder.Quality, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            var stream = new MemoryStream();
            bb.Save(stream, encoder, myEncoderParameters);

            var bytes = stream.ToArray();

            var idetity = getHashSha256(bytes);

            var dir = idetity.ToFilePath();

            var fulldir = Path.Combine(mount, dir);

            if (!Directory.Exists(fulldir))
                Directory.CreateDirectory(fulldir);

            string path1 = Path.Combine(fulldir, idetity + ".jpg");

            using (var file = new FileStream(path1, FileMode.Create, FileAccess.Write))
            {
                file.Write(bytes, 0, bytes.Length);
                file.Close();
            }
            bb.Dispose();
            stream.Dispose();
    
            return new FileInformation { ServerName = ConfigurationManager.AppSettings["ServerName"], FileName = idetity, Extension = ".jpg", Url = ConfigurationManager.AppSettings["ServerName"] + dir + "/" + idetity + ".jpg" };
        }
    }
}