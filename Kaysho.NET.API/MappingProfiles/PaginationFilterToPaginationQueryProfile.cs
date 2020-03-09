using AutoMapper;
using Kaysho.NET.Core.Contracts.V1.Requests.Queries;
using Kaysho.NET.Core.Dto;
using Kaysho.NET.Core.V1.Requests.Queries;

namespace Kaysho.NET.API.Profiles
{
    public class PaginationFilterToPaginationQueryProfile : Profile
    {
        public PaginationFilterToPaginationQueryProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<GetAllBlogsQuery, GetAllBlogsFilter>();
        }
    }
}
