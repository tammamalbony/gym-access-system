// -----------------------------
// File: Services/AlertService.cs
// -----------------------------
using AutoMapper;
using Gym.Core.Dtos;
using Gym.Api.Repositories;

namespace Gym.Api.Services;

public interface IAlertService
{
    Task<IEnumerable<EmailAlertDto>> All();
}

public class AlertService(IMapper map, IEmailAlertRepo repo) : IAlertService
{
    public async Task<IEnumerable<EmailAlertDto>> All() =>
        (await repo.GetAllAsync()).Select(map.Map<EmailAlertDto>);
}
