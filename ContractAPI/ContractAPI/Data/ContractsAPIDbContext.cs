using ContractAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ContractAPI.Data
{
    public class ContractsAPIDbContext : DbContext
    {
        public ContractsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contract> Contracts { get; set; } 

    }
}
