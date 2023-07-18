using DocuWare.Application;
using DocuWare.Application.Middleware;
using DocuWare.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    );
builder.Services.AddApplicationServices();
// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "DocuWare API", Version = "v1"}); });

builder.Services.AddDbContext<QuoteDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")!);
});

builder.Services.AddInfrastructureServices();
var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocuWare API V1"); });

app.MapControllers();
app.UseCustomExceptionHandler();
app.Run();