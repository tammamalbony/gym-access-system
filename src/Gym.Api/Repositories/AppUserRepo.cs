// -----------------------------
// File: Repositories/AppUserRepo.cs
// -----------------------------
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Repositories;

public class AppUserRepo(GymContext db) : IAppUserRepo
{
    public async Task<AppUser> AddAsync(AppUser u)
    {
        db.AppUsers.Add(u);
        await db.SaveChangesAsync();
        return u;
    }

    public Task<bool> DeleteAsync(int id) =>
        db.AppUsers.Where(x => x.UserId == id)
                   .ExecuteDeleteAsync()
                   .ContinueWith(t => t.Result == 1);

    public Task<AppUser?> GetAsync(int id) =>
        db.AppUsers.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);

    public async Task<IEnumerable<AppUser>> GetAllAsync() =>
        await db.AppUsers.AsNoTracking().ToListAsync();

    public async Task<AppUser?> UpdateAsync(AppUser u)
    {
        var exists = await db.AppUsers.FindAsync(u.UserId);
        if (exists is null) return null;
        db.Entry(exists).CurrentValues.SetValues(u);
        await db.SaveChangesAsync();
        return exists;
    }
}
