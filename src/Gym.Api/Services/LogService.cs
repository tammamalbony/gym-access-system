// -----------------------------
// File: Services/LogService.cs
// -----------------------------
using AutoMapper;
using Gym.Api.Dtos;
using Gym.Api.Repositories;

namespace Gym.Api.Services;

public interface ILogService
{
    Task<IEnumerable<AccessLogDto>> All();
    Task<AccessLogDto?> Latest();
}

public class LogService(IMapper map, IAccessLogRepo repo) : ILogService
{
    public async Task<IEnumerable<AccessLogDto>> All() =>
        (await repo.GetAllAsync()).Select(map.Map<AccessLogDto>);

    public async Task<AccessLogDto?> Latest() =>
        map.Map<AccessLogDto?>(await repo.GetLatestAsync());
}
