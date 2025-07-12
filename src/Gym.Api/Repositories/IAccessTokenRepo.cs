// -----------------------------
// File: Repositories/IAccessTokenRepo.cs
// -----------------------------
using Gym.Api.Models;

namespace Gym.Api.Repositories;

public interface IAccessTokenRepo
{
    Task<IEnumerable<AccessToken>> GetAllAsync();
    Task<AccessToken?> GetAsync(long id);
    Task<AccessToken> AddAsync(AccessToken t);
    Task<AccessToken?> UpdateAsync(AccessToken t);
    Task<bool> DeleteAsync(long id);
}
