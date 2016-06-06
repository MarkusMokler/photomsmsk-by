using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PhotoMSK.Data.Models
{
    public class User : IdentityUser
    {
        public virtual UserInformation UserInformation { get; set; }
        public virtual ICollection<UserMenuItem> UserMenuItems { get; set; }
        public virtual ICollection<Adresse> SendedMessages { get; set; }
        public virtual ICollection<Adresse> RecivedMessages { get; set; }

        public virtual void SendMessageTo(string id, string message)
        {
            var smessage = new Message
            {
                ID = Guid.NewGuid(),
                Text = message,
                Time = DateTime.Now
            };

            SendMessageTo(id, smessage);
        }

        public virtual void SendMessageTo(string id, Message message)
        {
            SendedMessages.Add(new Adresse
            {
                From = this,
                ToID = id,
                Message = message,
                ID = Guid.NewGuid(),
                Readed = false
            });
        }
    }
}