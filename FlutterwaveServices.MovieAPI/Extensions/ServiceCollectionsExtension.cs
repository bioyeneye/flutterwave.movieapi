using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FlutterwaveServices.MovieAPI.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionsExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="version"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services, String version, String title, String description)
        {
            services.AddSwaggerGen(c =>
            {
                c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
                c.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = title,
                    Version = version,
                    License = new OpenApiLicense
                    {
                        Name = "Microsoft Licence",
                        Url = new Uri("https://flutterwave.com/ng/"),
                    },
                    Description = description
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });
            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }

    }
}
