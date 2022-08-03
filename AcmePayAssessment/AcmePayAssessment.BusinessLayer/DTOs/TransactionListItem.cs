using AcmePayAssessment.Entity.Enums;
using System;

namespace AcmePayAssessment.BusinessLayer.DTOs
{
    public class TransactionListItem
    {
        public Guid Id { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardHolder { get; set; }
        public string HolderName { get; set; }
        public string OrderReference { get; set; }
        public Statuses Status { get; set; }
    }
}
