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
}
