using AutoMapper;
using PostService.DTOs;
using PostService.Models;

namespace PostService.Profiles
{
    public class LikeOfReplyProfile : Profile
    {
        public LikeOfReplyProfile()
        {
            CreateMap<LikeOfReply, LikeOfReplyDTO>();
            CreateMap<CreateLikeOfReplyDTO, LikeOfReply>();
            CreateMap<UpdateLikeOfReplyDTO, LikeOfReply>();
        }
    }
}
