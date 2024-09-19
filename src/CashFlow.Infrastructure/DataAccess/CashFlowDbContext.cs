using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

public class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expnses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Database=cashflow;Uid=kayo;Pwd=teste123;";
        var serverVersion = new MySqlServerVersion(new Version(9, 0, 1));
        
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
    
    // comando config mysql shell> docker exec -it mysql1 mysql -uroot -p
    // 
}