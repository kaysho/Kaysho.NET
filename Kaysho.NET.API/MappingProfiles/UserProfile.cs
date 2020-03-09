using AutoMapper;
using DamilolaShopeyin.API.Data;

namespace Kaysho.NET.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, DamilolaShopeyin.Core.Models.Profile>()
                .ForMember(
                    dest => dest.IsVerified,
                    opt => opt.MapFrom(src => src.EmailConfirmed));


        }
    }
}
