using Microsoft.EntityFrameworkCore;
using WebApiCrud_DotNetCore8.Data;
using WebApiCrud_DotNetCore8.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

// Add services for Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
builder.Services.AddScoped<IMyCacheService, MyCacheService>();

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
