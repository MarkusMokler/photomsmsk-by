using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PhotoMSK.Data;
using PhotoMSK.Data.Models;
using PhotoMSK.ViewModels.Interfaces;

namespace PhotoMSK.ViewModels.Realisation
{
    class UserInformationService : IUserInformationService
    {
        readonly Lazy<AppContext> _context = new Lazy<AppContext>(() => new AppContext());
        public UserInformationViewModel.Summary GetSummary(Guid id)
        {
            var info = _context.Value.UserInformations.FirstOrDefault();
            return Mapper.Map<UserInformationViewModel.Summary>(info);

        }

        public UserInformationViewModel.Details GetDetails(Guid id)
        {
            var info = _context.Value.UserInformations.FirstOrDefault();
            return Mapper.Map<UserInformationViewModel.Details>(info);
        }

        public void Dispose()
        {
            if (_context.IsValueCreated)
            {
                _context.Value.Dispose();

            }
        }
    }
}
