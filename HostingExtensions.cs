using System.Reflection;
using System.Text.Json.Serialization;
using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using IdentityService.Application.Interfaces;
using IdentityService.Infrastructure.Services;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;

namespace IdentityService;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
      .AddJsonOptions(options =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                // ignore omitted parameters on models to enable optional params (e.g. User update)
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));


        builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection("MongoDbConfig"));
        var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

        builder.Services.AddIdentity<User, Role>()
            .AddMongoDbStores<User, Role, int>
            (
                mongoDbSettings.ConnectionString, mongoDbSettings.Name
            );


        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddProfileService<ProfileService>()
            .AddAspNetIdentity<User>()
            ;
        builder.Services.AddTransient<IProfileService, ProfileService>();

        // builder.Services.AddAuthentication();


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpClient();
        builder.Services.AddSwaggerGen();


        builder.Services.AddScoped<IMongoCollectionService, MongoCollectionService>();
        builder.Services.AddScoped<ICollectionService, CollectionService>();
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
              {
                  endpoints.MapControllers();
              });
        return app;
    }
}