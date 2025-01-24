using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BAL.Shared
{
    public static class AuthenticationService
    {
        public static IServiceCollection AddAutenticationService(this IServiceCollection services)
        {
            services.AddSwaggerGen(o => {
                o.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT token in this field. Add 'Bearer ' before the token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT"
                };

                o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new[] { "Admin", "User" } // Roles to test in Swagger
                    }
                };

                o.AddSecurityRequirement(securityRequirement);
            });

            return services;
        }
    }
}
