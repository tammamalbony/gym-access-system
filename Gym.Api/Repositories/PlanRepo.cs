
// -----------------------------
// File: Repositories/PlanRepo.cs
// -----------------------------
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Repositories;

public class PlanRepo(GymContext db) : IPlanRepo
{
    public async Task<Plan> AddAsync(Plan p)
    {
        db.Plans.Add(p);
        await db.SaveChangesAsync();
        return p;
    }
    public Task<bool> DeleteAsync(int id) => db.Plans.Where(x => x.PlanId == id)
            .ExecuteDeleteAsync().ContinueWith(t => t.Result == 1);

    public Task<Plan?> GetAsync(int id) => db.Plans.AsNoTracking().FirstOrDefaultAsync(x => x.PlanId == id);

    public async Task<IEnumerable<Plan>> GetAllAsync() => await db.Plans.AsNoTracking().ToListAsync();

    public async Task<Plan?> UpdateAsync(Plan p)
    {
        var exists = await db.Plans.FindAsync(p.PlanId);
        if (exists is null) return null;
        db.Entry(exists).CurrentValues.SetValues(p);
        await db.SaveChangesAsync();
        return exists;
    }
}