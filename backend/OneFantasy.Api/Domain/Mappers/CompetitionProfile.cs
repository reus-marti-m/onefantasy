using AutoMapper;
using OneFantasy.Api.DTOs;
using OneFantasy.Api.Models.Competitions;

namespace OneFantasy.Api.Domain.Mappers
{
    public class CompetitionProfile : Profile
    {

        public CompetitionProfile()
        {
            CreateMap<CompetitionDto, Competition>()
                .ConstructUsing(dto => new Competition(dto.Name, dto.Type, dto.Format));

            CreateMap<Competition, CompetitionDtoResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Format, opt => opt.MapFrom(src => src.Format))
                .ForMember(dest => dest.Seasons, opt => opt.MapFrom(src => src.Seasons));
        }

    }
}
