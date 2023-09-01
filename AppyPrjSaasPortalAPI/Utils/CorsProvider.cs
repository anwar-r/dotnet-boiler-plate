namespace AppyPrjSaasPortalAPI.Utils
{
    public static class CorsProvider
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("LowCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }
            ));
        }
    }
}
