using Tasks.Infra.Migrations;
using Tasks.Domain.Extensions;
using Tasks.Infra;
using Tasks.Infra.RepositoryAccess;
using Tasks.Application;
using Tasks.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfra(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

//builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionsFilter)));

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