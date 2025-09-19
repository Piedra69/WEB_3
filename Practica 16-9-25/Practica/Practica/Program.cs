using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Practica.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PracticaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PracticaContext") ?? throw new InvalidOperationException("Connection string 'PracticaContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// SEED: Crear roles automáticamente al iniciar la app
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "admin", "cliente", "empleado" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Middlewares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Rutas por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Index}/{id?}");

app.Run();
