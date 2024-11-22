using AutoMapper;
using PostService.DTOs;
using PostService.Models;

namespace PostService.Profiles
{
    public class LikeOfPostProfile : Profile
    {
        public LikeOfPostProfile()
        {
            CreateMap<LikeOfPost, LikeOfPostDTO>();
            CreateMap<CreateLikeOfPostDTO, LikeOfPost>();
            CreateMap<UpdateLikeOfPostDTO, LikeOfPost>();
        }
    }
}
