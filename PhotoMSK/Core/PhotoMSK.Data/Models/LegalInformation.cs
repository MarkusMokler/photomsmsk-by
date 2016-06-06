using System;

namespace PhotoMSK.Data.Models
{
    public class LegalInformation : HistoricalEntity
    {
        public bool AllowOnlineBooking { get; set; }
        public string PublicOffer { get; set; }

        public string Legaladdress { get; set; } //şğèäè÷åñêèé àäğåñ
        public string AccountNumber { get; set; } //ÓÍÏ
        public string RegisterTrade { get; set; } //ÇÀĞÅÃÈÑÒĞÈĞÎÂÀÍ Â ÒÎĞÃÎÂÎÌ ĞÅÅÑÒĞÅ
        public string CertificateNumber { get; set; } //ÍÎÌÅĞ ÑÂÈÄÅÒÅËÜÑÒÂÀ Î ĞÅÃÈÑÒĞÀÖÈÈ
        public DateTime? RegisterDate { get; set; } //ÂÛÄÀÍÎ ÑÂÈÄÅÒÅËÜÑÒÂÎ Î ĞÅÃÈÑÒĞÀÖÈÈ(äàòà)
        public string RegisteringAgency { get; set; } //ĞÅÃÈÑÒĞÈĞÓŞÙÈÉ ÎĞÃÀÍ  
        public string LegalName { get; set; }
        public string CEO { get; set; }

        public virtual RouteEntity RouteEntity { get; set; }

    }
}