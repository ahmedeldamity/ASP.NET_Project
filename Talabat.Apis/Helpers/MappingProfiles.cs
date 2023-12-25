using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Talabat.Apis.Dtos;
using Talabat.Core.Entities;

namespace Talabat.Apis.Helpers
{
    public class MappingProfiles: Profile
    {
        //private readonly IConfiguration _configuration;

        public MappingProfiles(/*IConfiguration configuration*/)
        {
            //_configuration = configuration;

            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                // -- We wanted to bring configuration to bring "ApiBaseUrl From appsetting.json 
                // -- but this isn't work because when we register automapper in proram class 
                // -- it create this class with parameter less constractor 
                // -- so it will chain on the parameter less constractor and didn't see this constractor
                // -- so i commented the below line and configuration
                //.ForMember(d => d.PictureUrl, o => o.MapFrom(s => $"{_configuration["ApiBaseUrl"]}/{s.PictureUrl}"))
                // -- the solution of this issue is: instead of using MapFrom I use MapFrom<"class inherit from IValueResolver<sourse, destination, member>">
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
        }   
    }
}
