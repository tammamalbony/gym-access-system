// -----------------------------
// File: Repositories/EmailAlertRepo.cs
// -----------------------------
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Repositories;

public class EmailAlertRepo(GymContext db) : IEmailAlertRepo
{
    public async Task<EmailAlert> AddAsync(EmailAlert alert)
    {
        db.EmailAlerts.Add(alert);
        await db.SaveChangesAsync();
        return alert;
    }
    public Task<bool> DeleteAsync(long id) =>
        db.EmailAlerts.Where(x => x.AlertId == id)
                      .ExecuteDeleteAsync()
                      .ContinueWith(t => t.Result == 1);
    public Task<EmailAlert?> GetAsync(long id) =>
        db.EmailAlerts.AsNoTracking().FirstOrDefaultAsync(x => x.AlertId == id);
    public async Task<IEnumerable<EmailAlert>> GetAllAsync() =>
        await db.EmailAlerts.AsNoTracking().ToListAsync();
    public async Task<EmailAlert?> UpdateAsync(EmailAlert alert)
    {
        var exists = await db.EmailAlerts.FindAsync(alert.AlertId);
        if (exists is null) return null;
        db.Entry(exists).CurrentValues.SetValues(alert);
        await db.SaveChangesAsync();
        return exists;
    }
}
