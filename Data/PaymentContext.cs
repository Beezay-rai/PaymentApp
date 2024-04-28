using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentApp.Data;

namespace PaymentApp.DataModel
{
    public class PaymentContext : IdentityDbContext< AppUserDetail,UserRoles,string>
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Balance> Balance { get; set; }
        public DbSet<AppUserDetail> AppUserDetail { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

    }

   

}
