
// -----------------------------
// File: Repositories/IPlanRepo.cs
// -----------------------------
using Gym.Api.Models;

namespace Gym.Api.Repositories;

public interface IPlanRepo
{
    Task<IEnumerable<Plan>> GetAllAsync();
    Task<Plan?> GetAsync(int id);
    Task<Plan> AddAsync(Plan p);
    Task<Plan?> UpdateAsync(Plan p);
    Task<bool> DeleteAsync(int id);
}
