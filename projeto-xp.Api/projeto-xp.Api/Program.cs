using Microsoft.EntityFrameworkCore;
using Serilog;
using projeto_xp.Api.Models;
using projeto_xp.Api.Extensions;
using projeto_xp.Api.Middlewares;
using projeto_xp.Api.Services;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    SerilogExtension.AddSerilogApi(builder.Configuration);
    builder.Host.UseSerilog(Log.Logger);

    builder.Services.AddControllers();

    if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
    {
        var server = builder.Configuration["DbServer"];
        var port = builder.Configuration["DbPort"];
        var user = builder.Configuration["DbUser"];
        var password = builder.Configuration["Password"];
        var database = builder.Configuration["Database"];
        var connectionString = $"Server={server}, {port};Initial Catalog={database};User ID={user};Password={password}";

        builder.Services.AddDbContext<UserContext>(opt =>
        opt.UseSqlServer(connectionString));
    }
    else
    {
        builder.Services.AddDbContext<UserContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    }

    builder.Services.AddScoped<IUserRepository, UserRepository>();

    var app = builder.Build();

    DatabaseManagementService.MigrationInitialization(app);

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestSerilogMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    //app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server shutting down");
    Log.CloseAndFlush();
}
