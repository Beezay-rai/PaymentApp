using System.ComponentModel.DataAnnotations;

namespace PaymentApp.DataModel
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Username { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string TrackingId { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? Status { get; set; }

    }
    public class Balance
    {
        [Key]
        public int Id { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Username { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public decimal TotalAmount { get; set; }
    }

    public enum TransactionStatus
    {
        Success = 1,
        Pending = 2,
        Failed = 3

    }
}
