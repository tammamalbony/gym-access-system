// -----------------------------
// File: Repositories/AccessLogRepo.cs
// -----------------------------
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Repositories;

public class AccessLogRepo(GymContext db) : IAccessLogRepo
{
    public async Task<AccessLog> AddAsync(AccessLog log)
    {
        db.AccessLogs.Add(log);
        await db.SaveChangesAsync();
        return log;
    }
    public Task<bool> DeleteAsync(long id) =>
        db.AccessLogs.Where(x => x.LogId == id)
                     .ExecuteDeleteAsync()
                     .ContinueWith(t => t.Result == 1);
    public Task<AccessLog?> GetAsync(long id) =>
        db.AccessLogs.AsNoTracking().FirstOrDefaultAsync(x => x.LogId == id);
    public async Task<IEnumerable<AccessLog>> GetAllAsync() =>
        await db.AccessLogs.AsNoTracking().ToListAsync();
    public async Task<AccessLog?> UpdateAsync(AccessLog log)
    {
        var exists = await db.AccessLogs.FindAsync(log.LogId);
        if (exists is null) return null;
        db.Entry(exists).CurrentValues.SetValues(log);
        await db.SaveChangesAsync();
        return exists;
    }

    public Task<AccessLog?> GetLatestAsync() =>
        db.AccessLogs.AsNoTracking()
            .OrderByDescending(l => l.EventTime)
            .FirstOrDefaultAsync();
}
