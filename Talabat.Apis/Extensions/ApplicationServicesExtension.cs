﻿using Talabat.Apis.Helpers;
using Talabat.Core.IRepositories;
using Talabat.Repository;

namespace Talabat.Apis.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // --- Bad Way To Register Dependancy Injection Of Generic Repositories
            //builder.Services.AddScoped<IGenericRepositories<Product>, GenericRepositories<Product>>();
            //builder.Services.AddScoped<IGenericRepositories<ProductBrand>, GenericRepositories<ProductBrand>>();
            //builder.Services.AddScoped<IGenericRepositories<ProductCategory>, GenericRepositories<ProductCategory>>();
            // --- Right Way To Register Dependancy Injection Of Generic Repositories
            services.AddScoped(typeof(IGenericRepositories<>), typeof(GenericRepositories<>));

            // --- Two Ways To Register AutoMapper
            // - First (harder)
            //builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            // - Second (easier)
            services.AddAutoMapper(typeof(MappingProfiles));

            return services;
        }
    }
}