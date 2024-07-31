using AutoMapper;
using RankingSystem.DAL.Models;
using RankingSystem.PL.ViewModels;

namespace RankingSystem.PL.MapppingProfilles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ItemViewModel, Item>().ReverseMap()
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Ratings.Any() ? src.Ratings.Average(r => r.StarsNum) : 0))
            .ForMember(dest => dest.RatingCount, opt => opt.MapFrom(src => src.Ratings.Count));
            CreateMap<RatingViewModel, Rating>().ReverseMap();
        }
    }
}
