using AutoMapper;
using PusulaETicaret.Web.Entities;
using PusulaETicaret.Web.Models;

namespace PusulaETicaret.Web
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, ProductsViewModel>().ReverseMap();
            CreateMap<Item, ItemsViewModel>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, EditUserViewModel>().ReverseMap();
            CreateMap<User, CreateUserViewModel>().ReverseMap();
        }
    }
}
