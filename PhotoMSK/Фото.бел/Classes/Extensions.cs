using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Fotobel.Classes
{
    public static class Extensions
    {

        public static Guid GetUserInformationID(this IPrincipal principal)
        {
            ClaimsPrincipal cprincipal = principal as ClaimsPrincipal;
            if (cprincipal != null)
            {
                return Guid.Parse(cprincipal.Claims.Single(c => c.Type == "UserInformationID").Value);
            }
            throw new UnauthorizedAccessException();
        }
    }
}