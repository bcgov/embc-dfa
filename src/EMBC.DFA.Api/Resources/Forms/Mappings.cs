using AutoMapper;
using EMBC.DFA.Dynamics.Microsoft.Dynamics.CRM;

namespace EMBC.DFA.Api.Resources.Forms
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Form, incident>(MemberList.None)
                .IncludeAllDerived()
                .ForMember(d => d.dfa_applicanttype, opts => opts.MapFrom(s => (int?)Enum.Parse<ApplicantTypeOptionSet>(s.ApplicantType.ToString())))
                .ForMember(d => d.dfa_indigenousstatus, opts => opts.MapFrom(s => s.IndigenousStatus))
                .ForMember(d => d.dfa_onfirstnationsreserve, opts => opts.MapFrom(s => s.OnFirstNationReserve))
                .ForMember(d => d.dfa_nameoffirstnationsreserve, opts => opts.MapFrom(s => s.NameOfFirstNationsReserve))
                ;
        }
    }

    public enum ApplicantTypeOptionSet
    {
        SmallBusinessOwner = 222710003,
        FarmOwner = 222710002,
        CharitableOrganization = 222710004,
        HomeOwner = 222710000,
        ResidentialTenant = 222710001
    }
}
