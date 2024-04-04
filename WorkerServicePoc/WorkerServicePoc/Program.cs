using WorkerServicePoc;
using WorkerServicePoc.Application;
using WorkerServicePoc.Extensions;
using WorkerServicePoc.Infrastructure;
using WorkerServicePoc.Persistance;
using WorkerServicePoc.WorkerSettings;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<WorkerPeriodSettings>(builder.Configuration.GetSection(WorkerPeriodSettings.Section));

builder.Services
    .AddPersistance(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

builder.Services.AddHostedService<Worker>();

IHost host = builder.Build();

IHostEnvironment env = host.Services.GetRequiredService<IHostEnvironment>();
if (env.IsDevelopment())
{
    host.ApplyMigrations();
}

host.Run();
