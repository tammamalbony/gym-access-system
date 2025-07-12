// -----------------------------
// File: Services/SubscriptionService.cs
// -----------------------------
using AutoMapper;
using Gym.Api.Dtos;
using Gym.Api.Repositories;
using Gym.Api.Models;

namespace Gym.Api.Services;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionDto>> All();
    Task<SubscriptionDto?> One(long id);
    Task<SubscriptionDto?> ActiveForMember(long memberId);
    Task<SubscriptionDto> Add(SubscriptionDto dto);
    Task<SubscriptionDto?> Update(SubscriptionDto dto);
    Task<bool> Delete(long id);
}

public class SubscriptionService(IMapper map, ISubscriptionRepo repo) : ISubscriptionService
{
    public async Task<IEnumerable<SubscriptionDto>> All() =>
        (await repo.GetAllAsync()).Select(map.Map<SubscriptionDto>);

    public async Task<SubscriptionDto?> One(long id) =>
        map.Map<SubscriptionDto?>(await repo.GetAsync(id));

    public async Task<SubscriptionDto?> ActiveForMember(long memberId) =>
        map.Map<SubscriptionDto?>(await repo.GetActiveByMemberAsync(memberId));

    public async Task<SubscriptionDto> Add(SubscriptionDto dto) =>
        map.Map<SubscriptionDto>(await repo.AddAsync(map.Map<Subscription>(dto)));

    public async Task<SubscriptionDto?> Update(SubscriptionDto dto) =>
        map.Map<SubscriptionDto?>(await repo.UpdateAsync(map.Map<Subscription>(dto)));

    public Task<bool> Delete(long id) => repo.DeleteAsync(id);
}
