using tmgcat.App.Middlewares;
using tmgcat.Bll.Extensions;
using tmgcat.Dal.Extensions;
using tmgcat.Dal.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod();
            policy.WithOrigins("http://localhost:5173");
        });
});

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddLogging()
    .AddOptions<DalOptions>().BindConfiguration("DalOptions");
builder.Services
    .AddBllServices()
    .AddDalInfrastructure()
    .AddDalRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<RequestLoggingMiddleware>();
    app.UseCors("MyPolicy");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateUp();

app.Run();
