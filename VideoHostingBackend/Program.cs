using VideoHostingBackend;
using VideoHostingBackend.Util;

IHost app = CreateDefaultBuilder(args).Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    MigrationApplier applier = scope.ServiceProvider.GetRequiredService<MigrationApplier>();
    DatabaseSeeder seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();

    applier.ApplyPending();
    seeder.SeedDatabase();
}

app.Run();

IHostBuilder CreateDefaultBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webHostBuilder =>
            webHostBuilder.UseStartup<Startup>());