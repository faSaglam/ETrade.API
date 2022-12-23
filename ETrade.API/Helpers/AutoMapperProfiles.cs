using AutoMapper;
using ETrade.API.Dtos;
using ETrade.API.Models;

namespace ETrade.API.Helpers

{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Category, CategoryForListDto>();
            CreateMap<Product, ProductForListDto>();
            CreateMap<Product, ProductForDetailDto>();
            CreateMap<Cart, CartDto>();

            CreateMap<Customer, CustomerCartDto>().ForMember(dest => dest.Carts, opt =>
            {
                opt.MapFrom(src => src.Carts);
            });
           

            CreateMap<Seller, SellerToSellerForDetailDto>()
                .ForMember(dest => dest.Products, opt =>
                {
                    opt.MapFrom(src => src.Products);
                });
            CreateMap<Category, CategoryForDetailDto>().ForMember(dest => dest.Products, opt =>
            {
                opt.MapFrom(src => src.Products);
            });

        }

      
    }
}
