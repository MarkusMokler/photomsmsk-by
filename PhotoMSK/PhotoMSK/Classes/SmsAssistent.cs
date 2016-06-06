using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace PhotoMSK.Classes
{
    public class SmsAssistent : IIdentityMessageService
    {


        public Task SendAsync(IdentityMessage message)
        {
            return SendMessageAsync(message.Destination, message.Body);
        }

    }
}