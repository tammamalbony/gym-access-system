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
}

public class LogService(IMapper map, IAccessLogRepo repo) : ILogService
{
    public async Task<IEnumerable<AccessLogDto>> All() =>
        (await repo.GetAllAsync()).Select(map.Map<AccessLogDto>);
}
