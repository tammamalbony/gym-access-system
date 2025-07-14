
// =============================
// File: Services/MemberService.cs
// =============================
using AutoMapper;
using Gym.Core.Dtos;
using Gym.Api.Repositories;

namespace Gym.Api.Services;
public interface IMemberService
{
    Task<IEnumerable<MemberDto>> All();
    Task<MemberDto?> One(long id);
    Task<MemberDto> Add(MemberDto dto);
    Task<MemberDto?> Update(MemberDto dto);
    Task<bool> Delete(long id);
}

public class MemberService(IMapper map, IMemberRepo repo) : IMemberService
{
    public async Task<IEnumerable<MemberDto>> All()
        => (await repo.GetAllAsync()).Select(map.Map<MemberDto>);
    public async Task<MemberDto?> One(long id)
        => map.Map<MemberDto?>(await repo.GetAsync(id));
    public async Task<MemberDto> Add(MemberDto dto)
        => map.Map<MemberDto>(await repo.AddAsync(map.Map<Models.Member>(dto)));
    public async Task<MemberDto?> Update(MemberDto dto)
        => map.Map<MemberDto?>(await repo.UpdateAsync(map.Map<Models.Member>(dto)));
    public Task<bool> Delete(long id) => repo.DeleteAsync(id);
}
