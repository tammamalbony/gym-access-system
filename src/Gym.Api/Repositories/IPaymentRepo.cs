// -----------------------------
// File: Repositories/IPaymentRepo.cs
// -----------------------------
using Gym.Api.Models;

namespace Gym.Api.Repositories;

public interface IPaymentRepo
{
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<Payment?> GetAsync(long id);
    Task<Payment> AddAsync(Payment p);
    Task<Payment?> UpdateAsync(Payment p);
    Task<bool> DeleteAsync(long id);
}
