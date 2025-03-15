using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoushGah.DataAccess;
using NoushGah.DataAccess.IdentityModel;
using NoushGah.Web.Infra;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers And Views
builder.Services.AddControllersWithViews();

// Memory Cache
builder.Services.AddMemoryCache();

// Add DbContext
#region DbContext
string? connection = builder.Configuration.GetConnectionString("cnn");
builder.Services.AddDbContext<NoushGahDbContext>(x => x.UseSqlServer(connection));
#endregion

// Add Identity
#region Identity
builder.Services.AddIdentity<SystemUser, IdentityRole>().AddEntityFrameworkStores<NoushGahDbContext>();
#endregion

// Add Services : I + ClassName
#region Services
var assemblies = AppDomain.CurrentDomain.GetAssemblies();
builder.Services.AddBusinessServices(assemblies); 
#endregion

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.Run();
