global using SuperHeroAPI.Data;
global using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<ActorJohnWick>();
builder.Services.AddScoped<ActorBruceLee>();

builder.Services.AddScoped<ActorDel>(sp => (type) =>
{
    switch (type)
    {
        case "John":
           return sp.GetRequiredService<ActorJohnWick>();
        case "Bruce":
            return sp.GetRequiredService<ActorBruceLee>();
        default:
            throw new NotImplementedException();
    }
});
//builder.Services.AddTransient<ActorJohnWick>().AddScoped<IActor, ActorJohnWick>(n => n.GetService<ActorJohnWick>());
//builder.Services.AddTransient<ActorBruceLee>().AddScoped<IActor, ActorBruceLee>(n => n.GetService<ActorBruceLee>());

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

app.UseAuthorization();

app.MapControllers();

app.Run();
