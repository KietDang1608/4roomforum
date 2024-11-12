
using AutoMapper;
using PostService.DTOs;
using PostService.Models;

namespace PostService.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<CreatePostDTO, Post>();
            CreateMap<UpdatePostDTO, Post>();
        }
    }
}
