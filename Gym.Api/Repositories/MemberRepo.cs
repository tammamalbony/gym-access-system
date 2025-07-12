
// =============================
// File: Repositories/MemberRepo.cs
// =============================
using Gym.Api.Data;
using Gym.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Api.Repositories;
public class MemberRepo(GymContext db) : IMemberRepo
{
    public async Task<Member> AddAsync(Member m)
    {
        db.Members.Add(m);
        await db.SaveChangesAsync();
        return m;
    }
    public Task<bool> DeleteAsync(long id)
    {
        return db.Members.Where(x => x.MemberId == id)
                          .ExecuteDeleteAsync()
                          .ContinueWith(t => t.Result == 1);
    }
    public Task<Member?> GetAsync(long id) =>
        db.Members.AsNoTracking().FirstOrDefaultAsync(x => x.MemberId == id);

    public async Task<IEnumerable<Member>> GetAllAsync() =>
        await db.Members.AsNoTracking().ToListAsync();

    public async Task<Member?> UpdateAsync(Member m)
    {
        var exists = await db.Members.FindAsync(m.MemberId);
        if (exists is null) return null;
        db.Entry(exists).CurrentValues.SetValues(m);
        await db.SaveChangesAsync();
        return exists;
    }
}