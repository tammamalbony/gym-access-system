// -----------------------------
// File: Repositories/PaymentRepo.cs
// -----------------------------
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Repositories;

public class PaymentRepo(GymContext db) : IPaymentRepo
{
    public async Task<Payment> AddAsync(Payment p)
    {
        db.Payments.Add(p);
        await db.SaveChangesAsync();
        return p;
    }
    public Task<bool> DeleteAsync(long id) =>
        db.Payments.Where(x => x.PaymentId == id)
                   .ExecuteDeleteAsync()
                   .ContinueWith(t => t.Result == 1);
    public Task<Payment?> GetAsync(long id) =>
        db.Payments.AsNoTracking().FirstOrDefaultAsync(x => x.PaymentId == id);
    public async Task<IEnumerable<Payment>> GetAllAsync() =>
        await db.Payments.AsNoTracking().ToListAsync();
    public async Task<Payment?> UpdateAsync(Payment p)
    {
        var exists = await db.Payments.FindAsync(p.PaymentId);
        if (exists is null) return null;
        db.Entry(exists).CurrentValues.SetValues(p);
        await db.SaveChangesAsync();
        return exists;
    }
}
