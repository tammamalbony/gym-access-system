// -----------------------------
// File: Repositories/IAccessLogRepo.cs
// -----------------------------
using Gym.Api.Models;

namespace Gym.Api.Repositories;

public interface IAccessLogRepo
{
    Task<IEnumerable<AccessLog>> GetAllAsync();
    Task<AccessLog?> GetAsync(long id);
    Task<AccessLog> AddAsync(AccessLog log);
    Task<AccessLog?> UpdateAsync(AccessLog log);
    Task<bool> DeleteAsync(long id);
}
