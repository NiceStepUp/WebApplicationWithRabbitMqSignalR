using WebApplicationRabbitMqSignalR;
using WebApplicationRabbitMqSignalR.Extensions.DependencyInjection;
using WebApplicationRabbitMqSignalR.SignalR;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.ConfigureBackgroundHostedServices();
builder.Services.ConfigureAppSettings(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureRepositories();
builder.Services.ConfigureWebModule();
builder.Services.ConfigureRabbitMq(builder.Configuration);


WebApplication app = builder.Build();

app.UseMiddleware<GlobalRoutePrefixMiddleware>("/api/v1");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UsePathBase(new PathString("/api/v1"));

app.UseRouting();
app.UseAuthorization();

// app.MapControllers();

app.UseEndpoints(endpoints =>
{
    /*
     * MapControllers() - it is important. Без этого не будет работать роутинг на АПИ контроллеры.
     */
    endpoints.MapControllers();
    ConfigureSignalREndpoints(endpoints);
});

app.Run();
return;

void ConfigureSignalREndpoints(IEndpointRouteBuilder endpoints)
{
    // endpoints.MapHub<WeatherHub>("hub/weatherHub").RequireAuthorization(AuthPolicyConstants.Jwt);
    endpoints.MapHub<WeatherHub>("hub/weatherHub");
}




