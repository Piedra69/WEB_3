using Gestion.Data;
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
