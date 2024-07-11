using Logic.Dtos;
using AutoMapper;
using DAL.Entities;

namespace Logic.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateTwoWaysMap<Candidate, CandidateDto>();
        }

        private void CreateTwoWaysMap<T1, T2>()
        {
            CreateMap<T1, T2>();
             CreateMap<T2, T1>();
        }
    }
}
