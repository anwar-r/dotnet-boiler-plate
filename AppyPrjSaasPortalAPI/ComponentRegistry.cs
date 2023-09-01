using Data.Repositories;
using Data.util;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;

namespace AppyPrjSaasPortalAPI
{
    public static class ComponentRegistry
    {
        public static void Register(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            //System Dependencies

            serviceCollection.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.Converters.Add(new StringEnumConverter());
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            serviceCollection.AddHttpContextAccessor();

            serviceCollection.AddDbContext<Repository>(opt => opt.UseSqlServer(configuration.GetConnectionString("Repository")));
            
            serviceCollection.AddScoped<IUserContext, UserContext>();

        }
    }
}
