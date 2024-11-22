using BLL.DAL;
using BLL.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//IoC Container:
string connectionString = "server=(localdb)\\mssqllocaldb;database=MovieAppDB;trusted_connection=true;";
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IRoleService, RolesService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IDirectorService, DirectorsService>();
builder.Services.AddScoped<IGenreService, GenresService>();
builder.Services.AddScoped<IMovieGenresService, MovieGenresService>();
builder.Services.AddScoped<IUserService, UsersService>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
