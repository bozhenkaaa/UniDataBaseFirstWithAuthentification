using WebApplication3;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using Microsoft.AspNetCore.Identity;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Lab1Context>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityContext>(option => option.UseSqlServer(
    builder.Configuration.GetConnectionString("IdentityConnection")
    ));
builder.Services.AddTransient<IUserValidator<User>, CustomUserValidator>();

builder.Services.AddIdentity<User, IdentityRole>(opts => {
    opts.Password.RequiredLength = 5;   // минимальная длина
    opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
    opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
    opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
    opts.Password.RequireDigit = false; // требуются ли цифры
    opts.User.RequireUniqueEmail = true;    // уникальный email
    opts.User.AllowedUserNameCharacters = ".@abcdefghijklmnopqrstuvwxyz";

})
    .AddEntityFrameworkStores<IdentityContext>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleInitializer.InitializeAsync(userManager, rolesManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database." + DateTime.Now.ToString());
    }
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
