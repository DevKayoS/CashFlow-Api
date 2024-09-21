using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;

internal class CashFlowDbContext : DbContext
{
    public CashFlowDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Expense> Expenses { get; set; }
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