using AutoMapper;

namespace CatThreadService.Profiles
{
    public class ThreadProfile : Profile
    {
        public  ThreadProfile()
        {
            CreateMap<ThreadDTO,Thread>();
            CreateMap<Thread, ThreadDTO>();
        }
    }
}
