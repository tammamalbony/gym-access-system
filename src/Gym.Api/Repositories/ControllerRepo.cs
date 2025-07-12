// -----------------------------
// File: Repositories/ControllerRepo.cs
// -----------------------------
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Repositories;

public class ControllerRepo(GymContext db) : IControllerRepo
{
    public async Task<Controller> AddAsync(Controller c)
    {
        db.Controllers.Add(c);
        await db.SaveChangesAsync();
        return c;
    }
    public Task<bool> DeleteAsync(int id) =>
        db.Controllers.Where(x => x.ControllerId == id)
                      .ExecuteDeleteAsync()
                      .ContinueWith(t => t.Result == 1);
    public Task<Controller?> GetAsync(int id) =>
        db.Controllers.AsNoTracking().FirstOrDefaultAsync(x => x.ControllerId == id);
    public async Task<IEnumerable<Controller>> GetAllAsync() =>
        await db.Controllers.AsNoTracking().ToListAsync();
    public async Task<Controller?> UpdateAsync(Controller c)
    {
        var exists = await db.Controllers.FindAsync(c.ControllerId);
        if (exists is null) return null;
        db.Entry(exists).CurrentValues.SetValues(c);
        await db.SaveChangesAsync();
        return exists;
    }
}
