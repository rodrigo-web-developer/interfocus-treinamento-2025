using InterfocusConsole.Repository;
using InterfocusConsole.Repository.Implementations;
using InterfocusConsole.Services;
using NHibernate.Cfg;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<AlunoService>();
builder.Services.AddTransient<CursoService>();
builder.Services.AddTransient<InscricaoService>();

var isInMemory = builder.Configuration.GetValue("UseInMemory", false);
if (isInMemory)
{
    builder.Services.AddTransient<IRepository, RepositoryInMemory>();
}
else
{
    var connectionString = builder.Configuration
        .GetConnectionString("Default");
    // criar implementacao para ISessionFactory
    builder.Services.AddSingleton(c =>
    {
        var config = new Configuration().Configure();
        config.DataBaseIntegration(
            x => x.ConnectionString = connectionString
        );
        return config.BuildSessionFactory();
    });
    builder.Services.AddTransient<IRepository, RepositoryNHibernate>();
}

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

app.Run();
