using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration.UserSecrets;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//Uncomment this after we got a MVC to work with.
builder.Services.AddDbContext<KoalaDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("KoalaDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString();
    var userCookie = context.Request.Cookies["userCookie"];
    string userRole = null;
    string userEmail = null;

    if (!string.IsNullOrEmpty(userCookie))
    {
        try
        {
            var users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(userCookie);

            if (users != null && users.Count > 0)
            {
                userRole = users[0].Role;
                userEmail = users[0].Email;
            }
        }
        catch (System.Text.Json.JsonException ex)
        {
            Console.WriteLine($"JSON Exception: {ex.Message}");
        }
    }
    bool isAdmin = userRole == "Admin";
    bool isStaff = userRole == "Staff";
    if (path.StartsWith("/Manage", StringComparison.OrdinalIgnoreCase))
    {
        if (!isAdmin && !isStaff)
        {
            var userIp = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
            Console.WriteLine($"Unauthorized access attempt to {path} by IP: {userIp}");
            context.Response.Redirect("/Index");
            return;
        }
    }
    if (path.StartsWith("/Manage/UsersManager", StringComparison.OrdinalIgnoreCase) && isStaff)
    {
        Console.WriteLine($"Unauthorized access attempt to {path} by user: {userEmail}");
        context.Response.Redirect("/Index");
        return;
    }
    await next();
});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
