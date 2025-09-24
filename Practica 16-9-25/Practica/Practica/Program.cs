using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practica.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar la base de datos
builder.Services.AddDbContext<PracticaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PracticaContext")
        ?? throw new InvalidOperationException("Connection string 'PracticaContext' not found.")));

//// Configurar Identity
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<PracticaContext>()
//    .AddDefaultTokenProviders();

// Agregar MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ✅ SEED: Crear roles y usuario admin automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "admin", "cliente", "empleado" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // ✅ Crear usuario admin por defecto (opcional)
        string adminEmail = "admin@admin.com";
        string adminPassword = "Admin123!"; // Asegúrate de cumplir los requisitos de seguridad

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var newAdmin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newAdmin, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "admin");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al crear roles/usuarios: {ex.Message}");
    }
}

// Middlewares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // ✅ Necesario para Identity
app.UseAuthorization();

// Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Index}/{id?}");

app.Run();
