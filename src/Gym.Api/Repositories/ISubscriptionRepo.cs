// -----------------------------
// File: Repositories/ISubscriptionRepo.cs
// -----------------------------
using Gym.Api.Models;

namespace Gym.Api.Repositories;

public interface ISubscriptionRepo
{
    Task<IEnumerable<Subscription>> GetAllAsync();
    Task<Subscription?> GetAsync(long id);
    Task<Subscription> AddAsync(Subscription s);
    Task<Subscription?> UpdateAsync(Subscription s);
    Task<bool> DeleteAsync(long id);
    Task<Subscription?> GetActiveByMemberAsync(long memberId);
}
