// -----------------------------
// File: Repositories/IFingerprintRepo.cs
// -----------------------------
using Gym.Api.Models;

namespace Gym.Api.Repositories;

public interface IFingerprintRepo
{
    Task<IEnumerable<Fingerprint>> GetAllAsync();
    Task<Fingerprint?> GetAsync(long id);
    Task<Fingerprint> AddAsync(Fingerprint f);
    Task<Fingerprint?> UpdateAsync(Fingerprint f);
    Task<bool> DeleteAsync(long id);
}
