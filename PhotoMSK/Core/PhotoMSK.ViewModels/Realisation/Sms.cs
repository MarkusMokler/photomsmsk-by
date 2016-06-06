using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace PhotoMSK.ViewModels.Realisation
{
    public class SmsSender
    {
        private string _project;
        private string _apiKey;
        private string _baseDomain = "https://mainsms.ru/api/mainsms/";

        public SmsSender(string projectName, string apiKey, bool isTest)
        {
            _project = projectName;
            _apiKey = apiKey;
        }


        public SmsSender(string projectName, string apiKey)
        {
            _project = projectName;
            _apiKey = apiKey;
        }

        public string MessageSend(string sender, string recipients, string message)
        {

            var xml = "XML=<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                      "<SMS>\n" +
                      "<operations>\n" +
                      "<operation>SEND</operation>\n" +
                      "</operations>\n" +
                      "<authentification>\n" +
                      "<username>radislavi@mail.ru</username>\n" +
                      "<password>mamapapa</password>\n" +
                      "</authentification>\n" +
                      "<message>\n" +
                      "<sender>fotobrif.ru</sender>\n" +
                      "<text>" + message + "</text>\n" +
                      "</message>\n" +
                      "<numbers>\n" +
                      "<number messageID=\"msg11\">" + recipients + "</number>\n" +
                      "</numbers>\n" +
                      "</SMS>\n";
            HttpWebRequest request = WebRequest.Create("http://api.atompark.com/members/sms/xml.php") as HttpWebRequest;
            request.Method = "Post";
            request.ContentType = "application/x-www-form-urlencoded";
            Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(xml);
            request.ContentLength = data.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(data, 0, data.Length);
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"Server error (HTTP {response.StatusCode}: {response.StatusDescription}).");
                StreamReader reader = new StreamReader(response.GetResponseStream());

                return reader.ReadToEnd();

            }




            //Dictionary<string, string> data = new Dictionary<string, string>
            //{
            //    {"project", _project},
            //    {"recipients", recipients},
            //    {"sender", sender},
            //    {"message", message},
            //    {"apikey",_apiKey }
            //};
            //// string sign = GetSign(data);
            //// data.Add("sign", sign);
            //return SendPost(_baseDomain + "message/send/", data);

        }




        private string GetSign(Dictionary<string, string> data)
        {
            string step1 = "";

            step1 = string.Join(";", data.OrderBy(x => x.Key).Select(x => x.Value));
            step1 += ";" + _apiKey;
            var sha1 = _SHA1(step1);
            var md5 = _MD5(sha1);
            return md5;
        }

        private string _MD5(string text)
        {

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        private static string _SHA1(string text)
        {

            return Sha1HashStringForUtf8String(text);
        }

        /// <summary>
        /// Compute hash for string encoded as UTF8
        /// </summary>
        /// <param name="s">String to be hashed</param>
        /// <returns>40-character hex string</returns>
        public static string Sha1HashStringForUtf8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            using (var sha1 = SHA1.Create())
            {
                byte[] hashBytes = sha1.ComputeHash(bytes);

                return HexStringFromBytes(hashBytes);
            }
        }

        /// <summary>
        /// Convert an array of bytes to a string of hex digits
        /// </summary>
        /// <param name="bytes">array of bytes</param>
        /// <returns>String of hex digits</returns>
        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        private string SendPost(String link, Dictionary<string, string> data)
        {

            var pprms = string.Join("&",
                data.Select(kvp =>
                    $"{kvp.Key}={kvp.Value}"));

            WebRequest request = WebRequest.Create(link + $"?{pprms}");

            request.Method = "GET";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            var resp = request.GetResponse();

            return new StreamReader(resp.GetResponseStream()).ReadToEnd();
            // Create a new HttpClient and Post Header
        }


    }
}
