using AutoMapper;
using CatThreadService.DTOs;

namespace CatThreadService.Profiles
{
    public class ThreadProfile : Profile
    {
        public  ThreadProfile()
        {
            CreateMap<ThreadDTO, Threads>();
            CreateMap<Threads, ThreadDTO>();
        }
    }
}
