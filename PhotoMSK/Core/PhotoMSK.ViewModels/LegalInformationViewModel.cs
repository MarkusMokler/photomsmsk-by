using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoMSK.Data.Models;
using PhotoMSK.Data.Models.ActivityStream;
using PhotoMSK.ViewModels.Activity;
using PhotoMSK.ViewModels.Social;

namespace PhotoMSK.ViewModels
{
    public static class LegalInformationViewModel
    {
        public class Summary : UniqueEntity
        {
            public bool AllowOnlineBooking { get; set; }
            public string PublicOffer { get; set; }
            public string LegalName { get; set; }
            public string CEO { get; set; }

            public string Legaladdress { get; set; } //юридический адрес
            public string AccountNumber { get; set; } //УНП
            public string RegisterTrade { get; set; } //ЗАРЕГИСТРИРОВАН В ТОРГОВОМ РЕЕСТРЕ
            public string CertificateNumber { get; set; } // СertificateNumber НОМЕР СВИДЕТЕЛЬСТВА О РЕГИСТРАЦИИ
            public DateTime? RegisterDate { get; set; } //ВЫДАНО СВИДЕТЕЛЬСТВО О РЕГИСТРАЦИИ(дата)
            public string RegisteringAgency { get; set; } //РЕГИСТРИРУЮЩИЙ ОРГАН  
        }
    }

    public class LegalInformationViewModeProfile : MapperProfile
    {
        protected override void Configure()
        {
            CreateMap<LegalInformation, LegalInformationViewModel.Summary>();
        }
    }
}
