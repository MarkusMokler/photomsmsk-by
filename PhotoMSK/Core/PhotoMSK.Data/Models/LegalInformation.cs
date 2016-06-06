using System;

namespace PhotoMSK.Data.Models
{
    public class LegalInformation : HistoricalEntity
    {
        public bool AllowOnlineBooking { get; set; }
        public string PublicOffer { get; set; }

        public string Legaladdress { get; set; } //����������� �����
        public string AccountNumber { get; set; } //���
        public string RegisterTrade { get; set; } //��������������� � �������� �������
        public string CertificateNumber { get; set; } //����� ������������� � �����������
        public DateTime? RegisterDate { get; set; } //������ ������������� � �����������(����)
        public string RegisteringAgency { get; set; } //�������������� �����  
        public string LegalName { get; set; }
        public string CEO { get; set; }

        public virtual RouteEntity RouteEntity { get; set; }

    }
}