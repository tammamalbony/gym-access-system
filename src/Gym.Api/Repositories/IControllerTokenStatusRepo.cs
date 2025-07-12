// -----------------------------
// File: Repositories/IControllerTokenStatusRepo.cs
// -----------------------------
using Gym.Api.Models;

namespace Gym.Api.Repositories;

public interface IControllerTokenStatusRepo
{
    Task<IEnumerable<ControllerTokenStatus>> GetAllAsync();
    Task<ControllerTokenStatus?> GetAsync(int controllerId, long tokenId);
    Task<ControllerTokenStatus> AddAsync(ControllerTokenStatus cts);
    Task<ControllerTokenStatus?> UpdateAsync(ControllerTokenStatus cts);
    Task<bool> DeleteAsync(int controllerId, long tokenId);
}
