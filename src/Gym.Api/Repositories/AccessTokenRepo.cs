// -----------------------------
// File: Repositories/AccessTokenRepo.cs
// -----------------------------
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Repositories;

public class AccessTokenRepo(GymContext db) : IAccessTokenRepo
{
    public async Task<AccessToken> AddAsync(AccessToken t)
    {
        db.AccessTokens.Add(t);
        await db.SaveChangesAsync();
        return t;
    }
    public Task<bool> DeleteAsync(long id) =>
        db.AccessTokens.Where(x => x.TokenId == id)
                       .ExecuteDeleteAsync()
                       .ContinueWith(t => t.Result == 1);
    public Task<AccessToken?> GetAsync(long id) =>
        db.AccessTokens.AsNoTracking().FirstOrDefaultAsync(x => x.TokenId == id);
    public async Task<IEnumerable<AccessToken>> GetAllAsync() =>
        await db.AccessTokens.AsNoTracking().ToListAsync();
    public async Task<AccessToken?> UpdateAsync(AccessToken t)
    {
        var exists = await db.AccessTokens.FindAsync(t.TokenId);
        if (exists is null) return null;
        db.Entry(exists).CurrentValues.SetValues(t);
        await db.SaveChangesAsync();
        return exists;
    }
}
