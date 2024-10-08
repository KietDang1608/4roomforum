using System;
using AutoMapper;
using UserServices.DTOs;

namespace UserServices.Profiles;

public class RoleProfile : Profile
{
	public RoleProfile()
	{
		CreateMap<Role, RoleDTO>();
		CreateMap<RoleDTO, Role>();
	}
}