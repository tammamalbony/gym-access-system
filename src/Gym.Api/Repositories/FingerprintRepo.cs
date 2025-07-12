// -----------------------------
// File: Repositories/FingerprintRepo.cs
// -----------------------------
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Repositories;

public class FingerprintRepo(GymContext db) : IFingerprintRepo
{
    public async Task<Fingerprint> AddAsync(Fingerprint f)
    {
        db.Fingerprints.Add(f);
        await db.SaveChangesAsync();
        return f;
    }
    public Task<bool> DeleteAsync(long id) =>
        db.Fingerprints.Where(x => x.FingerprintId == id)
                       .ExecuteDeleteAsync()
                       .ContinueWith(t => t.Result == 1);
    public Task<Fingerprint?> GetAsync(long id) =>
        db.Fingerprints.AsNoTracking().FirstOrDefaultAsync(x => x.FingerprintId == id);
    public async Task<IEnumerable<Fingerprint>> GetAllAsync() =>
        await db.Fingerprints.AsNoTracking().ToListAsync();
    public async Task<Fingerprint?> UpdateAsync(Fingerprint f)
    {
        var exists = await db.Fingerprints.FindAsync(f.FingerprintId);
        if (exists is null) return null;
        db.Entry(exists).CurrentValues.SetValues(f);
        await db.SaveChangesAsync();
        return exists;
    }
}
