using AcmePayAssessment.Entity.Enums;

namespace AcmePayAssessment.BusinessLayer.DTOs
{
    public class Response
    {
        public string Id { get; set; }
        public Statuses Status { get; set; }
    }
}
