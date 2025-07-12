// -----------------------------
// File: Repositories/ControllerTokenStatusRepo.cs
// -----------------------------
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Repositories;

public class ControllerTokenStatusRepo(GymContext db) : IControllerTokenStatusRepo
{
    public async Task<ControllerTokenStatus> AddAsync(ControllerTokenStatus cts)
    {
        db.ControllerTokenStatuses.Add(cts);
        await db.SaveChangesAsync();
        return cts;
    }
    public Task<bool> DeleteAsync(int controllerId, long tokenId) =>
        db.ControllerTokenStatuses.Where(x => x.ControllerId == controllerId && x.TokenId == tokenId)
                                  .ExecuteDeleteAsync()
                                  .ContinueWith(t => t.Result == 1);
    public Task<ControllerTokenStatus?> GetAsync(int controllerId, long tokenId) =>
        db.ControllerTokenStatuses.AsNoTracking()
                                  .FirstOrDefaultAsync(x => x.ControllerId == controllerId && x.TokenId == tokenId);
    public async Task<IEnumerable<ControllerTokenStatus>> GetAllAsync() =>
        await db.ControllerTokenStatuses.AsNoTracking().ToListAsync();
    public async Task<ControllerTokenStatus?> UpdateAsync(ControllerTokenStatus cts)
    {
        var exists = await db.ControllerTokenStatuses.FindAsync(cts.ControllerId, cts.TokenId);
        if (exists is null) return null;
        db.Entry(exists).CurrentValues.SetValues(cts);
        await db.SaveChangesAsync();
        return exists;
    }
}
