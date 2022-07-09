using XM.Assignment.Domain.Extensions.DI;
using XM.Assignment.Infrastructure.Extensions.Configuration;
using XM.Assignment.Infrastructure.Extensions.DI;
using XM.Assignment.Infrastructure.Extensions.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.AllowEmptyInputInBodyModelBinding = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerDocumentation();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddConfiguration();
builder.Services.AddInfrastructureServices();
builder.Services.AddDomainServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
