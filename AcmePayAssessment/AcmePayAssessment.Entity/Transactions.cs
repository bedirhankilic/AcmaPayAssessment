using AcmePayAssessment.Entity.Abstract;
using AcmePayAssessment.Entity.Enums;
using System;

namespace AcmePayAssessment.Entity
{
    public class Transactions : BaseEntity
    {
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardHolder { get; set; }
        public string HolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int CVV { get; set; }
        public string OrderReference { get; set; }
        public Statuses Status { get; set; }
    }
}
