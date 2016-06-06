using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.ViewModels.Interfaces;


namespace PhotoMSK.ViewModels.Realisation
{
    class SmsAssistent : ISmsAssistent
    {
        string _assistentUser = "EnDjiVeb";
        string _assistentPassword = "FnefK1nz";
        public SmsAssistent()
        {
        }

        public async Task<string> SendMessageAsync(string phone, string text)
        {
            if (phone.StartsWith("+375"))
                return await SentAssistentAsync(phone, text);

            var sms = new SmsSender("fotobrif", "57e68e239ff28");
            return sms.MessageSend("fotobrif.ru", phone, text);
        }


        public virtual string SendMessage(string phone, string text)
        {
            if (phone.StartsWith("+375"))
                return SentAssistent(phone, text);
            var sms = new SmsSender("fotobrif", "57e68e239ff28");
            var str = sms.MessageSend("fotobrif.ru", phone, text);
            return str;
        }

        private string SentAssistent(string phone, string text)
        {
            WebRequest request = WebRequest.Create("https://userarea.sms-assistent.by/api/v1/send_sms/plain");
            var pprms = new List<string>
            {
                 "user="+_assistentUser,"password="+_assistentPassword,"recipient="+phone,"message="+text,"sender=photomsk.by"
            };

            string str = string.Join("&", pprms);

            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            request.ContentLength = bytes.Length;

            var os = request.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            var resp = request.GetResponse();

            return new System.IO.StreamReader(resp.GetResponseStream()).ReadToEnd();
        }
        private async Task<string> SentAssistentAsync(string phone, string text)
        {
            WebRequest request = WebRequest.Create("https://userarea.sms-assistent.by/api/v1/send_sms/plain");
            var pprms = new List<string>
            {
                "user="+_assistentUser,"password="+_assistentPassword,"recipient="+phone,"message="+text,"sender=photomsk.by"
            };

            string str = string.Join("&", pprms);

            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            request.ContentLength = bytes.Length;

            var os = request.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            var resp = await request.GetResponseAsync();

            return new System.IO.StreamReader(resp.GetResponseStream()).ReadToEnd();
        }
    }
}
