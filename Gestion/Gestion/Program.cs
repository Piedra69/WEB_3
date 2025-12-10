using Gestion.Data;
using Gestion.Models;
using Gestion.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseInMemoryDatabase("UserDb"));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CursoService>();
builder.Services.AddScoped<InscripcionService>();
builder.Services.AddScoped<RecursoService>();
builder.Services.AddScoped<EvaluacionService>();
builder.Services.AddScoped<RolService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowVue");

app.UseAuthorization();

app.MapControllers();

app.Run();
