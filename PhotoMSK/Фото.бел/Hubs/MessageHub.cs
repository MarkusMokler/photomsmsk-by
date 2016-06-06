using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using PhotoMSK.Data;

namespace Fotobel.Hubs
{
    [Authorize]
    public class MessageHub : Hub
    {
        //public override Task OnConnected()
        //{
        //    var name = Context.User.Identity.GetUserId();
        //    return name;
        //}

        public void Send(string id, string message)
        {
            var senderId = Context.User.Identity.GetUserId();

            using (var context = new AppContext())
            {
                var sender = context.Users.Find(senderId);
                sender.SendMessageTo(id, message);
                context.SaveChanges();
            }

            Clients.User(id).addNewMessageToPage(senderId, message);
        }
    }
}