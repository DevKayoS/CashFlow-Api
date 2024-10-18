using Microsoft.EntityFrameworkCore;
using CashFlow.Domain.Entities;

namespace CashFlow.Infrastructure.DataAccess;

internal class CashFlowDbContext : DbContext
{
    public CashFlowDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Domain.Entities.User> Users { get; set; }
}
