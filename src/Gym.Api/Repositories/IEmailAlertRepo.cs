// -----------------------------
// File: Repositories/IEmailAlertRepo.cs
// -----------------------------
using Gym.Api.Models;

namespace Gym.Api.Repositories;

public interface IEmailAlertRepo
{
    Task<IEnumerable<EmailAlert>> GetAllAsync();
    Task<EmailAlert?> GetAsync(long id);
    Task<EmailAlert> AddAsync(EmailAlert alert);
    Task<EmailAlert?> UpdateAsync(EmailAlert alert);
    Task<bool> DeleteAsync(long id);
}
