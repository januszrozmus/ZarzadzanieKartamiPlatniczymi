using MgmtCardLib.Model;
using Microsoft.EntityFrameworkCore;

namespace MgmtCardLib
{
    public class CustomerCardDbContext : DbContext
    {
        public DbSet<CustomerCard>? CustomerCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionstring = @"Server=.\sqlexpress;Database=CardsManagementSystem;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True";
            try
            {
                if (!optionsBuilder.IsConfigured)
                {
                    var options = optionsBuilder.UseSqlServer(connectionstring);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.SaveLog("CustomerCardDbContext.OnConfiguration: " + ex.Message);
            }
        }
    }
}
