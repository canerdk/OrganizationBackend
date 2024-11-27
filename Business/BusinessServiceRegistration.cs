using Business.Abstract;
using Business.Concrete;
using Business.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Business
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);
            services.AddHttpContextAccessor();

            services.AddScoped<IJwtManager, JwtManager>();      
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IParticipantService, ParticipantService>();
            services.AddScoped<IUserContractService, UserContractService>();

            services.AddMemoryCache();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

            //var serviceType = typeof(IScoped);
            //var types = assembly.GetTypes().Where(t => serviceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

            //foreach (var type in types)
            //{
            //    var interfaceType = type.GetInterfaces().FirstOrDefault(t => t != serviceType);
            //    if (interfaceType != null)
            //    {
            //        services.AddScoped(interfaceType, type);
            //    }
            //}

            return services;
        }
    }
}
