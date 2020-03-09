using AutoMapper;
using Kaysho.NET.Core.Contracts.V1.Responses;

namespace Kaysho.NET.API.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<DamilolaShopeyin.Core.Models.Blog, BlogResponse>();


            //CreateMap<Tag, TagResponse>();
        }
    }
}
