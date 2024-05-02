using Amazon.SimpleNotificationService;
using Microsoft.EntityFrameworkCore;
using PushNotificationsService.DbContextConfig;
using PushNotificationsService.DeviceInfo.Repositories;
using PushNotificationsService.DeviceInfo.Services;
using PushNotificationsService.Exceptions;
using PushNotificationsService.PushNotificationEndpoint;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace PushNotificationsService;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    private void AddDatabase(IServiceCollection services)
    {
        services.AddDbContext<PushNotificationContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("PostgresConnectionsDatabase"));
        });
    }
    
    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(new CustomExceptionFilter());
        });
        services.AddTransient<IDeviceInfoRepository, DeviceInfoRepository>();
        services.AddTransient<IDeviceInfoService, DeviceInfoService>();
        services.AddTransient<IPushNotificationEndpointService, PushNotificationEndpointService>();
        services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
        services.AddAWSService<IAmazonSimpleNotificationService>();
        services.AddTransient<IDeviceInfoRepository, DeviceInfoRepository>();
        AddDatabase(services);
        services.AddSwaggerGen();
    }
    
    public static IApplicationBuilder ConfigureSwagger(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("../swagger/v1/swagger.json", "Push notification service");
            c.RoutePrefix = "swagger";
        });

        return app;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        ConfigureSwagger(app);
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/",
                async context =>
                {
                    context.Response.StatusCode = 202;
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
        });
    }
}