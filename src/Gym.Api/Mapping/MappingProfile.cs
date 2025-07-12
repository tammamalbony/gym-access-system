
// =============================
// File: Mapping/MappingProfile.cs
// =============================
using AutoMapper;
using Gym.Api.Dtos;
using Gym.Api.Models;

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
    }
}