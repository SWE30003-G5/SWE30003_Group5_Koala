using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration.UserSecrets;
using SWE30003_Group5_Koala.Data;
using SWE30003_Group5_Koala.Models;
using SWE30003_Group5_Koala.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//Uncomment this after we got a MVC to work with.
builder.Services.AddDbContext<KoalaDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("KoalaDb")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<KoalaDbContext>();
    DatabaseInitializer.Initialize(context);
}

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
    // Deserialize the user cookie if it exists to retrieve role and email
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
    bool isCustomer = userRole == "Customer";
    // Check if the requested path starts with /Manage and the user is not Admin or Staff
    if (path.StartsWith("/Manage", StringComparison.OrdinalIgnoreCase) && !(isAdmin || isStaff))
    {
        var userIp = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";
        if (isCustomer)
        {
            Console.WriteLine($"Unauthorized access attempt to {path} by customer with email: {userEmail}");
        }
        else
        {
            Console.WriteLine($"Unauthorized access attempt to {path} by IP: {userIp}");
        }
        context.Response.Redirect("/UnauthorizedAccess");
        return;
    }
    // Check if the path is /Manage/UsersManager and if the user is Staff
    if (path.StartsWith("/Manage/UsersManager", StringComparison.OrdinalIgnoreCase) && isStaff)
    {
        Console.WriteLine($"Unauthorized access attempt to {path} by user: {userEmail}");
        context.Response.Redirect("/UnauthorizedAccess");
        return;
    }
    await next();
});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
