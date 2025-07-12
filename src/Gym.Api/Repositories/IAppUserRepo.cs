// -----------------------------
// File: Repositories/IAppUserRepo.cs
// -----------------------------
using Gym.Api.Models;

namespace Gym.Api.Repositories;

public interface IAppUserRepo
{
    Task<IEnumerable<AppUser>> GetAllAsync();
    Task<AppUser?> GetAsync(int id);
    Task<AppUser> AddAsync(AppUser user);
    Task<AppUser?> UpdateAsync(AppUser user);
    Task<bool> DeleteAsync(int id);
}
