namespace Shared.Payment.Models
{
    public class Card
    {
        public string Number { get; set; }
        public string HolderName { get; set; }
        public string Expiration {  get; set; }
        public string CVV { get; set; }

    }
}
