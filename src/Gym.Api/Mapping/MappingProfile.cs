
// =============================
// File: Mapping/MappingProfile.cs
// =============================
using AutoMapper;
using Gym.Core.Dtos;
using Gym.Api.Models;
using System;

namespace Gym.Api.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Member, MemberDto>()
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.MemberId));
        CreateMap<MemberDto, Member>()
            .ForMember(dst => dst.MemberId, o => o.MapFrom(src => src.Id));

        CreateMap<Plan, PlanDto>()
            .ForCtorParam("Id", o => o.MapFrom(s => s.PlanId));
        CreateMap<PlanDto, Plan>()
            .ForMember(d => d.PlanId, o => o.MapFrom(s => s.Id));

        CreateMap<Subscription, SubscriptionDto>()
            .ForCtorParam("Id", o => o.MapFrom(s => s.SubscriptionId));
        CreateMap<SubscriptionDto, Subscription>()
            .ForMember(d => d.SubscriptionId, o => o.MapFrom(s => s.Id));

        CreateMap<AppUser, AppUserDto>()
            .ForCtorParam("Id", o => o.MapFrom(s => s.UserId))
            .ForCtorParam("Role", o => o.MapFrom(s => s.Role.ToString()));
        CreateMap<AppUserDto, AppUser>()
            .ForMember(d => d.UserId, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Role, o => o.MapFrom(s => Enum.Parse<UserRole>(s.Role)));

        CreateMap<AccessLog, AccessLogDto>()
            .ForCtorParam("Id", o => o.MapFrom(s => s.LogId))
            .ForCtorParam("EventType", o => o.MapFrom(s => s.EventType.ToString()));

        CreateMap<EmailAlert, EmailAlertDto>()
            .ForCtorParam("Id", o => o.MapFrom(s => s.AlertId))
            .ForCtorParam("AlertType", o => o.MapFrom(s => s.AlertType.ToString()))
            .ForCtorParam("MemberId", o => o.MapFrom(s => s.RelatedMember))
            .ForCtorParam("Details", o => o.MapFrom(s => s.Details))
            .ForCtorParam("SentAt", o => o.MapFrom(s => s.SentAt));
    }
}
