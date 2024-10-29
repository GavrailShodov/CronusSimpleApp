//using CronusService;
//using Elders.Cronus;
//using Microsoft.Extensions.Hosting;

//var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddCronus(builder.Configuration);
//builder.Services.AddHostedService<Worker>();

//var host = builder.UseDefaultServiceProvider((context, options) =>
//{
//    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
//    options.ValidateScopes = false;
//    options.ValidateOnBuild = false;
//}).Build();


//host.Run();

using Elders.Cronus;

IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<Worker>();
            services.AddCronus(hostContext.Configuration);

        })
        .UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
            options.ValidateScopes = false;
            options.ValidateOnBuild = false;
        })
        .Build();

host.Run();