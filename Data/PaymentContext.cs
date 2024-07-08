using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentApp.Data;

namespace PaymentApp.DataModel
{
    public class PaymentContext : IdentityDbContext< AppUserDetail,UserRoles,string>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Balance> Balance { get; set; }
        public DbSet<AppUserDetail> AppUserDetail { get; set; }
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public DbSet<UserRoles> UserRoles { get; set; }
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword

    }

   

}
