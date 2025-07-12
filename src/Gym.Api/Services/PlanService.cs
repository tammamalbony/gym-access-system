
// -----------------------------
// File: Services/PlanService.cs
// -----------------------------
using AutoMapper;
using Gym.Api.Dtos;
using Gym.Api.Models;
using Gym.Api.Repositories;

namespace Gym.Api.Services;

public interface IPlanService
{
    Task<IEnumerable<PlanDto>> All();
    Task<PlanDto?> One(int id);
    Task<PlanDto> Add(PlanDto dto);
    Task<PlanDto?> Update(PlanDto dto);
    Task<bool> Delete(int id);
}

public class PlanService(IMapper map, IPlanRepo repo) : IPlanService
{
    public async Task<IEnumerable<PlanDto>> All() => (await repo.GetAllAsync()).Select(map.Map<PlanDto>);
    public async Task<PlanDto?> One(int id) => map.Map<PlanDto?>(await repo.GetAsync(id));
    public async Task<PlanDto> Add(PlanDto dto) => map.Map<PlanDto>(await repo.AddAsync(map.Map<Plan>(dto)));
    public async Task<PlanDto?> Update(PlanDto dto) => map.Map<PlanDto?>(await repo.UpdateAsync(map.Map<Plan>(dto)));
    public Task<bool> Delete(int id) => repo.DeleteAsync(id);
}