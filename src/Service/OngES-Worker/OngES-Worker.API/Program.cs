using Microsoft.OpenApi;
using API.Extensions;
using Infra.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes("abc123");



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfraestructure();
builder.Services.AddRabbitMq(builder.Configuration);


var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger(c =>
    {
        c.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            var scheme = httpReq.Headers["X-Forwarded-Proto"].FirstOrDefault() ?? httpReq.Scheme;
            var host = httpReq.Headers["X-Forwarded-Host"].FirstOrDefault() ?? httpReq.Host.Value;
    
            swagger.Servers = new List<OpenApiServer>
            {
                new() { Url = $"{scheme}://{host}/payments" }
            };
        });
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("./v1/swagger.json", "API v1");
    });
}

app.UseHttpsRedirection();

////app.UseAuthentication();
////app.UseAuthorization();


app.Run();

