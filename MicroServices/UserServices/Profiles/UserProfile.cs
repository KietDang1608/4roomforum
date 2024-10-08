using System;
using AutoMapper;
using UserServices.DTOs;

namespace UserServices.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO,User>();
    }
}
