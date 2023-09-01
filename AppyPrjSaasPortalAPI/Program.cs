

using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using AppyPrjSaasPortalAPI.Utils;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console().CreateBootstrapLogger();

Log.Information("Starting up - pre init");
try
{
var builder = WebApplication.CreateBuilder(args);


    builder.Host.UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(ctx.Configuration));

    Log.Information("App Starting Up");


    AuthProvider.Configure(builder.Services, builder.Configuration);
    SwaggerProvider.Configure(builder.Services);
    CorsProvider.Configure(builder.Services);
    ComponentRegistry.Register(builder.Services, builder.Configuration);
    var app = builder.Build();

    // Configure the HTTP request pipeline.

    app.UseSerilogRequestLogging();

    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<Repository>();
        dbContext.Database.Migrate();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
