using CashFlow.Domain.Repositories.User;

namespace CashFlow.Infrastructure.DataAccess.User;

public class UserRepository : IUserReadOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;

    public UserRepository(CashFlowDbContext dbContext) => _dbContext = dbContext;
    
    public Task<bool> ExistActiveUserWithEmail(string email)
    {
        var result = await _dbContext.
    }
}