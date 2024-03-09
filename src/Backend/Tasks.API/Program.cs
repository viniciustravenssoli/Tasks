using Tasks.Infra.Migrations;
using Tasks.Domain.Extensions;
using Tasks.Infra;
using Tasks.Infra.RepositoryAccess;
using Tasks.Application;
using Tasks.API.Filters;
using Microsoft.OpenApi.Models;
using Tasks.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionsFilter)));

builder.Services.AddScoped<AuthenticatedUserAttribute>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasks", Version = "v1" });

    // Configuração para autenticação usando Bearer token (JWT)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Token de autorização Bearer no formato JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    // Adicione a política de autorização
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                new string[] {}
            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

UpdateDB();

app.UseMiddleware<CultureMiddleware>();

app.Run();


void UpdateDB()
{
    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    using var context = serviceScope.ServiceProvider.GetService<TaskContext>();

    bool? dbInMemory = context?.Database?.ProviderName?.Equals("Microsoft.EntityFrameworkCore.InMemory");

    if (!dbInMemory.HasValue || !dbInMemory.Value)
    {
        var connection = builder.Configuration.GetConnection();
        var databaseName = builder.Configuration.GetDataBaseName();

        DataBase.CreateDataBase(connection, databaseName);

        app.MigrateDataBase();
    }
}

#pragma warning disable CA1050,  S3903, S1118
public partial class Program { }
#pragma warning restore CA1050, S3903, S1118