using Microsoft.AspNet.SignalR;

namespace Fotobel.Hubs
{
    public class CallerHub : Hub
    {
        public CallerHub()
        {
            int i = 0;
            i++;
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}