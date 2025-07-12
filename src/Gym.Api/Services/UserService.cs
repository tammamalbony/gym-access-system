// -----------------------------
// File: Services/UserService.cs
// -----------------------------
using AutoMapper;
using BCrypt.Net;
using Gym.Api.Dtos;
using Gym.Api.Models;
using Gym.Api.Repositories;

namespace Gym.Api.Services;

public interface IUserService
{
    Task<IEnumerable<AppUserDto>> All();
    Task<AppUserDto?> One(int id);
    Task<AppUserDto> Add(AppUserDto dto, string password);
    Task<AppUserDto?> Update(AppUserDto dto);
    Task<bool> Delete(int id);
    Task<bool> SetEnabled(int id, bool enabled);
    Task<bool> ChangePassword(int id, string newPwd);
}

public class UserService(IMapper map, IAppUserRepo repo) : IUserService
{
    public async Task<IEnumerable<AppUserDto>> All() =>
        (await repo.GetAllAsync()).Select(map.Map<AppUserDto>);

    public async Task<AppUserDto?> One(int id) =>
        map.Map<AppUserDto?>(await repo.GetAsync(id));

    public async Task<AppUserDto> Add(AppUserDto dto, string password)
    {
        var user = map.Map<AppUser>(dto);
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        var added = await repo.AddAsync(user);
        return map.Map<AppUserDto>(added);
    }

    public async Task<AppUserDto?> Update(AppUserDto dto)
    {
        var updated = await repo.UpdateAsync(map.Map<AppUser>(dto));
        return map.Map<AppUserDto?>(updated);
    }

    public Task<bool> Delete(int id) => repo.DeleteAsync(id);

    public async Task<bool> SetEnabled(int id, bool enabled)
    {
        var user = await repo.GetAsync(id);
        if (user is null) return false;
        user.IsEnabled = enabled;
        await repo.UpdateAsync(user);
        return true;
    }

    public async Task<bool> ChangePassword(int id, string newPwd)
    {
        var user = await repo.GetAsync(id);
        if (user is null) return false;
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPwd);
        await repo.UpdateAsync(user);
        return true;
    }
}
