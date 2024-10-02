using System;
using AutoMapper;
using CatThreadService.DTOs;

namespace CatThreadService.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO,Category>();
    }
}
