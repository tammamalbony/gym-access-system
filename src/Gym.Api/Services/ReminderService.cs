// -----------------------------
// File: Services/ReminderService.cs
// -----------------------------
using Gym.Api.Dtos;
using Gym.Api.Models;
using Gym.Api.Repositories;
using System.Linq;

namespace Gym.Api.Services;

public interface IReminderService
{
    Task<IEnumerable<ExpiringSubDto>> ExpiringOn(DateOnly date);
    Task<bool> SendReminder(long subscriptionId);
}

public class ReminderService(
    ISubscriptionRepo subs,
    IMemberRepo members,
    IPlanRepo plans,
    IEmailAlertRepo alerts) : IReminderService
{
    public async Task<IEnumerable<ExpiringSubDto>> ExpiringOn(DateOnly date)
    {
        var all = (await subs.GetAllAsync())
            .Where(s => s.EndDate == date && s.Status == SubscriptionStatus.ACTIVE)
            .ToList();
        var memDict = (await members.GetAllAsync()).ToDictionary(m => m.MemberId);
        var planDict = (await plans.GetAllAsync()).ToDictionary(p => p.PlanId);
        var list = new List<ExpiringSubDto>();
        foreach (var s in all)
        {
            if (!memDict.TryGetValue(s.MemberId, out var m)) continue;
            if (!planDict.TryGetValue(s.PlanId, out var p)) continue;
            list.Add(new ExpiringSubDto(s.SubscriptionId, s.MemberId,
                $"{m.FirstName} {m.LastName}", s.EndDate, p.GraceDays, m.Email));
        }
        return list;
    }

    public async Task<bool> SendReminder(long subscriptionId)
    {
        var sub = await subs.GetAsync(subscriptionId);
        if (sub is null) return false;
        var member = await members.GetAsync(sub.MemberId);
        if (member is null) return false;
        var alert = new EmailAlert
        {
            AlertType = AlertType.OVERDUE,
            RelatedMember = member.MemberId,
            Details = $"Reminder for subscription {subscriptionId}",
            SentAt = DateTime.UtcNow
        };
        await alerts.AddAsync(alert);
        return true;
    }
}
