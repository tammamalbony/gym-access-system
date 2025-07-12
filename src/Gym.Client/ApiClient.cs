using System.Net.Http;
using System.Net.Http.Json;
using Gym.Client.Models;

namespace Gym.Client;

public class ApiClient
{
    private readonly HttpClient _http;
    public ApiClient(string baseUrl)
    {
        _http = new HttpClient { BaseAddress = new(baseUrl) };
    }

    public Task<List<MemberDto>?> GetMembersAsync() =>
        _http.GetFromJsonAsync<List<MemberDto>>("api/members");

    public async Task<MemberDto?> AddMemberAsync(MemberDto dto)
    {
        var resp = await _http.PostAsJsonAsync("api/members", dto);
        return await resp.Content.ReadFromJsonAsync<MemberDto>();
    }

    public async Task<MemberDto?> UpdateMemberAsync(MemberDto dto)
    {
        var resp = await _http.PutAsJsonAsync($"api/members/{dto.Id}", dto);
        return await resp.Content.ReadFromJsonAsync<MemberDto>();
    }

    public Task DeleteMemberAsync(long id) => _http.DeleteAsync($"api/members/{id}");

    public Task<List<PlanDto>?> GetPlansAsync() =>
        _http.GetFromJsonAsync<List<PlanDto>>("api/plans");

    public async Task<PlanDto?> AddPlanAsync(PlanDto dto)
    {
        var resp = await _http.PostAsJsonAsync("api/plans", dto);
        return await resp.Content.ReadFromJsonAsync<PlanDto>();
    }

    public async Task<PlanDto?> UpdatePlanAsync(PlanDto dto)
    {
        var resp = await _http.PutAsJsonAsync($"api/plans/{dto.Id}", dto);
        return await resp.Content.ReadFromJsonAsync<PlanDto>();
    }

    public Task DeletePlanAsync(int id) => _http.DeleteAsync($"api/plans/{id}");

    public Task<List<SubscriptionDto>?> GetSubscriptionsAsync() =>
        _http.GetFromJsonAsync<List<SubscriptionDto>>("api/subscriptions");

    public async Task<SubscriptionDto?> AddSubscriptionAsync(SubscriptionDto dto)
    {
        var resp = await _http.PostAsJsonAsync("api/subscriptions", dto);
        return await resp.Content.ReadFromJsonAsync<SubscriptionDto>();
    }

    public async Task<SubscriptionDto?> UpdateSubscriptionAsync(SubscriptionDto dto)
    {
        var resp = await _http.PutAsJsonAsync($"api/subscriptions/{dto.Id}", dto);
        return await resp.Content.ReadFromJsonAsync<SubscriptionDto>();
    }

    public Task DeleteSubscriptionAsync(long id) => _http.DeleteAsync($"api/subscriptions/{id}");

    public Task<DashboardDto?> GetDashboardSummaryAsync() =>
        _http.GetFromJsonAsync<DashboardDto>("api/dashboard/summary");

    public Task<List<LateMemberDto>?> GetLateMembersAsync() =>
        _http.GetFromJsonAsync<List<LateMemberDto>>("api/dashboard/late-members");

    public Task<List<AppUserDto>?> GetUsersAsync() =>
        _http.GetFromJsonAsync<List<AppUserDto>>("api/users");

    public async Task<AppUserDto?> AddUserAsync(AppUserDto dto, string password)
    {
        var resp = await _http.PostAsJsonAsync("api/users", new { dto.Id, dto.Username, dto.Role, dto.Email, dto.IsEnabled, dto.CreatedAt, password });
        return await resp.Content.ReadFromJsonAsync<AppUserDto>();
    }

    public async Task<AppUserDto?> UpdateUserAsync(AppUserDto dto)
    {
        var resp = await _http.PutAsJsonAsync($"api/users/{dto.Id}", dto);
        return await resp.Content.ReadFromJsonAsync<AppUserDto>();
    }

    public Task DeleteUserAsync(int id) => _http.DeleteAsync($"api/users/{id}");

    public Task SetUserEnabledAsync(int id, bool enable) =>
        _http.PutAsJsonAsync($"api/users/{id}/enable", enable);

    public Task ChangePasswordAsync(int id, string pwd) =>
        _http.PutAsJsonAsync($"api/users/{id}/password", pwd);

    public Task<List<AccessLogDto>?> GetLogsAsync() =>
        _http.GetFromJsonAsync<List<AccessLogDto>>("api/logs");
}
