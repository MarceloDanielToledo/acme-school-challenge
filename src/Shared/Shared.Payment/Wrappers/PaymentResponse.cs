namespace Shared.Payment.Wrappers
{
    public class PaymentResponse<T> where T : class
    {
        public bool IsSuccess { get; set; } 
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
