namespace ProvaPub.Models
{
    public class Payment
    {
        public string Id { get; set; }
        public decimal Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public PaymentStatus Status { get; set; }

        public enum PaymentStatus
        {
            Success,
            Failure,
            Pending
        }


    }
}