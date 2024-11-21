using DataAccess.Context;
using DataAccess.Utilities.Auth;
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DataAccess
{
    public static class DataAccessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
               opt.UseSqlServer(configuration.GetConnectionString("AppConnection"))
           );

            services.AddIdentity<AppUser, AppRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();


            services.AddTransient<ILoginUserManager, LoginUserManager>();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }
            //var assembly = Assembly.GetExecutingAssembly();
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
