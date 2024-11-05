using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SWE30003_Group5_Koala.Data;

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

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
