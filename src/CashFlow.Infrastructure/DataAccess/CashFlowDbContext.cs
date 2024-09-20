using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

public class CashFlowDbContext : DbContext
{
    
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Port=3306;Database=cashflow;Uid=root;Pwd=teste123;";
        var serverVersion = new MySqlServerVersion(new Version(8, 4, 2));
        
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
    
    // comando config mysql shell> docker exec -it mysql1 mysql -uroot -p
    /*
     rodar essa query quando entrar no mysql
     
     *CREATE TABLE Expenses (
              `Id` BIGINT NOT NULL AUTO_INCREMENT,
              `Title` VARCHAR(255) NOT NULL,
              `Description` VARCHAR(2000) NULL,
              `Date` DATETIME NOT NULL,
              `Amount` DECIMAL(10, 2) NOT NULL,
              `PaymentType` INT NOT NULL,
              PRIMARY KEY (`Id`)
            );
           *
     * 
     */
}