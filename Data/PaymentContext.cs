using Microsoft.EntityFrameworkCore;

namespace PaymentApp.DataModel
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Balance> Balance { get; set; }

    }
}
