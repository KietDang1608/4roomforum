using AutoMapper;
using PostService.DTOs;
using PostService.Models;

namespace PostService.Profiles
{
    public class ReplyProfile : Profile
    {
        public ReplyProfile() {
            CreateMap<Reply, ReplyDTO>();
            CreateMap<CreateReplyDTO1, Reply>();
            CreateMap<CreateReplyDTO2, Reply>();              
            CreateMap<UpdateReplyDTO, Reply>();
        }
    }
}
