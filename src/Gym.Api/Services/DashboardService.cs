// -----------------------------
// File: Services/DashboardService.cs
// -----------------------------
using Gym.Api.Dtos;
using Gym.Api.Models;
using Gym.Api.Repositories;

namespace Gym.Api.Services;

public interface IDashboardService
{
    Task<DashboardDto> Summary();
    Task<IEnumerable<LateMemberDto>> LateMembers();
}

public class DashboardService(
    ISubscriptionRepo subs,
    IMemberRepo members,
    IPaymentRepo payments,
    IPlanRepo plans) : IDashboardService
{
    public async Task<DashboardDto> Summary()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var allSubs = (await subs.GetAllAsync()).ToList();
        var plansDict = (await plans.GetAllAsync()).ToDictionary(p => p.PlanId);
        var pays = await payments.GetAllAsync();

        int active = allSubs.Count(s => s.Status == SubscriptionStatus.ACTIVE && s.EndDate >= today);
        var late = allSubs.Where(s => s.EndDate < today && s.Status != SubscriptionStatus.CANCELLED).ToList();
        int lateMembers = late.Select(s => s.MemberId).Distinct().Count();
        int outstanding = 0;
        foreach (var s in late)
        {
            int paid = pays.Where(p => p.SubscriptionId == s.SubscriptionId).Sum(p => p.AmountCents);
            if (plansDict.TryGetValue(s.PlanId, out var plan))
            {
                int due = plan.PriceCents - paid;
                if (due > 0) outstanding += due;
            }
        }
        return new DashboardDto(active, lateMembers, outstanding);
    }

    public async Task<IEnumerable<LateMemberDto>> LateMembers()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var lateSubs = (await subs.GetAllAsync())
            .Where(s => s.EndDate < today && s.Status != SubscriptionStatus.CANCELLED)
            .ToList();
        var memberDict = (await members.GetAllAsync()).ToDictionary(m => m.MemberId);
        var plansDict = (await plans.GetAllAsync()).ToDictionary(p => p.PlanId);
        var pays = await payments.GetAllAsync();
        var list = new List<LateMemberDto>();
        foreach (var s in lateSubs)
        {
            if (!memberDict.TryGetValue(s.MemberId, out var m)) continue;
            int paid = pays.Where(p => p.SubscriptionId == s.SubscriptionId).Sum(p => p.AmountCents);
            int due = plansDict.TryGetValue(s.PlanId, out var plan)
                ? plan.PriceCents - paid
                : 0;
            int daysLate = today.DayNumber - s.EndDate.DayNumber;
            list.Add(new LateMemberDto(s.SubscriptionId, s.MemberId, $"{m.FirstName} {m.LastName}", due > 0 ? due : 0, daysLate));
        }
        return list;
    }
}
