
// =============================
// File: Repositories/IMemberRepo.cs
// =============================
using Gym.Api.Models;

namespace Gym.Api.Repositories;
public interface IMemberRepo
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetAsync(long id);
    Task<Member> AddAsync(Member m);
    Task<Member?> UpdateAsync(Member m);
    Task<bool> DeleteAsync(long id);
}