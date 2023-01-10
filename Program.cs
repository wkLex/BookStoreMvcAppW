using BookStoreMvcAppW.Models.Domain;
using BookStoreMvcAppW.Respositories.Abstracts;
using BookStoreMvcAppW.Respositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//var app = builder.Build();

// Add services to the container
//The interface (abstract) and the Implementation files in Repositories:
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreMvcAppWContext")));


// For Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

// builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/UserAuthentication/Login"); //DO LATER COMMENT FOR NOW!!

var app = builder.Build();

// Configure the HTTP request pipeline.


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
