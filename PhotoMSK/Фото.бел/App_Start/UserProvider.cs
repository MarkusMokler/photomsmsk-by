using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace Fotobel
{
    public class UserProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            return request.User.Identity.GetUserId();
        }
    }
}