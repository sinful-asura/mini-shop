using Microsoft.EntityFrameworkCore;
using Models;
var builder = WebApplication.CreateBuilder(args);

// Add Database Context
var connectionString = builder.Configuration.GetConnectionString("MiniShopCS");

builder.Services.AddDbContext<StoreContext>(options => {
    options.UseSqlServer(connectionString);
});

// CORS
builder.Services.AddCors(options => {
    options.AddPolicy("CORS", builder => {
        builder.WithOrigins(new string[] {
            "http://localhost:5091",
            "https://localhost:7098",
            "http://127.0.0.1:5091",
            "https://127.0.0.1:7098",
        })
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
