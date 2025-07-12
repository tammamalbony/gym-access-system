// -----------------------------
// File: Repositories/SubscriptionRepo.cs
// -----------------------------
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Repositories;

public class SubscriptionRepo(GymContext db) : ISubscriptionRepo
{
    public async Task<Subscription> AddAsync(Subscription s)
    {
        db.Subscriptions.Add(s);
        await db.SaveChangesAsync();
        return s;
    }
    public Task<bool> DeleteAsync(long id) =>
        db.Subscriptions.Where(x => x.SubscriptionId == id)
                        .ExecuteDeleteAsync()
                        .ContinueWith(t => t.Result == 1);
    public Task<Subscription?> GetAsync(long id) =>
        db.Subscriptions.AsNoTracking().FirstOrDefaultAsync(x => x.SubscriptionId == id);
    public async Task<IEnumerable<Subscription>> GetAllAsync() =>
        await db.Subscriptions.AsNoTracking().ToListAsync();
    public async Task<Subscription?> UpdateAsync(Subscription s)
    {
        var exists = await db.Subscriptions.FindAsync(s.SubscriptionId);
        if (exists is null) return null;
        db.Entry(exists).CurrentValues.SetValues(s);
        await db.SaveChangesAsync();
        return exists;
    }
}
