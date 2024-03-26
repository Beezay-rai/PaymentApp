using System.ComponentModel.DataAnnotations;

namespace PaymentApp.DataModel
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Username { get; set; }
        public string TrackingId { get; set; }
        public string? Status { get; set; }

    }
    public class Balance
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public enum TransactionStatus
    {
        Success = 1,
        Pending = 2,
        Failed = 3

    }
}
