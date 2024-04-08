namespace PaymentApp.Areas.Admin.Models
{
    public class BalanceModel
    {
        public string Username { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class TransactionModel
    {
        public string TrackingId { get; set; }

        public string Username { get; set; }
        public decimal Amount { get; set; }
    }
    public class TransactionGETModel
    {
        public int Id { get; set; }
        public string TrackingId { get; set; }
        public string Username { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }

    public class MyNestedTestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public MyNestedModel A { get; set; }
        public TransactionModel B { get; set; }
    }

    public class MyNestedModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TransactionGETModel A { get; set; }
        public TransactionModel B { get; set; }
    }
}
