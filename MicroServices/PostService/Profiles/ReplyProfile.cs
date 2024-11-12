using AutoMapper;
using PostService.DTOs;
using PostService.Models;

namespace PostService.Profiles
{
    public class ReplyProfile : Profile
    {
        public ReplyProfile() {
            CreateMap<Reply, ReplyDTO>();
            CreateMap<CreateReplyDTO, Reply>();
            CreateMap<UpdateReplyDTO, Reply>();
        }
    }
}
