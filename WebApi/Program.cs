using Serilog;
using WebApi.Configuration;
using WebApi.Middlewares;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

    builder.Host.UseSerilog();

    Log.Information("Iniciando a aplicação...");

    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    builder.Services.AddApplication();
    builder.Services.AddValidators();
    builder.Services.AddSQLiteConfiguration(configuration);
    builder.Services.AddCache();
    builder.Services.AddCustomSwagger();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseSwagger();

    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro na inicializacao da aplicacao");
}
finally
{
    Log.CloseAndFlush();
}