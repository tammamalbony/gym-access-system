// -----------------------------
// File: Repositories/IControllerRepo.cs
// -----------------------------
using Gym.Api.Models;

namespace Gym.Api.Repositories;

public interface IControllerRepo
{
    Task<IEnumerable<Controller>> GetAllAsync();
    Task<Controller?> GetAsync(int id);
    Task<Controller> AddAsync(Controller c);
    Task<Controller?> UpdateAsync(Controller c);
    Task<bool> DeleteAsync(int id);
}
