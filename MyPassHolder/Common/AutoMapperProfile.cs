using AutoMapper;
using MyPassHolder.RequestResponse;

namespace MyPassHolder.Common
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterRequest, User>().ReverseMap();
            CreateMap<CategoryRequest, Category>().ReverseMap();

        }
    }
}
