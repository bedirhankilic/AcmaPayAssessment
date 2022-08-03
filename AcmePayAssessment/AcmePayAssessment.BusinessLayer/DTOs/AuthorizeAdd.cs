namespace AcmePayAssessment.BusinessLayer.DTOs
{
    public class AuthorizeAdd
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardHolder { get; set; }
        public string HolderName { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public int CVV { get; set; }
        public string OrderReference { get; set; }
    }
}
