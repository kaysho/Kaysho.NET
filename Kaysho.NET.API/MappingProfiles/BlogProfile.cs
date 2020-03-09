using AutoMapper;

namespace Kaysho.NET.API.MappingProfiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<DamilolaShopeyin.Core.Models.Blog, Core.Dto.BlogDto>()
                .ForMember(
                    dest => dest.Article,
                    opt => opt.MapFrom(src => src.Article))
                ;
        }
    }
}
