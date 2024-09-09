using Shared.Payment.Enums;

namespace Shared.Payment.Responses
{
    public class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public EnumPayment Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}
