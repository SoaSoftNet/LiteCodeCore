﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteCode.IService;
using Microsoft.Extensions.DependencyInjection;

namespace LiteCode.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<ISysUserService, SysUserService>();
            services.AddScoped<ISysModulesService, SysModulesService>();
            services.AddScoped<ISysApplicationService, SysApplicationService>();
            services.AddScoped<ISysDepartmentService, SysDepartmentService>();
            services.AddScoped<ISysRolesService, SysRolesService>();
            return services;
        }
    }
}
