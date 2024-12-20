﻿using CashFlow.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.User;

internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;
    public UserRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
       return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<Domain.Entities.User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

    public async Task Add(Domain.Entities.User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
}