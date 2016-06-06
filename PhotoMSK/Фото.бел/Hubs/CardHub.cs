using Microsoft.AspNet.SignalR;

namespace Fotobel.Hubs
{
    public class CardHub : Hub
    {
        public void Send(string id, string info)
        {
            Clients.User(id).showInfo(info);
        }
    }
}