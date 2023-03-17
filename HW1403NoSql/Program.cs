using HW1403NoSql.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//using (var db = new MyContext())
//{
//    builder.Services.AddDbContext<MyContext>(options => options.UseCosmos(
//        "AccountEndpoint=https://lopatin-dbnosql-hw.documents.azure.com:443/;" +
//        "AccountKey=Z6a5AMWOK4U6U87xBsugqTykEyzW4zRbHRdUc533B1Ti8aUdxbceKm0LjAE4DfP9sjnoIY9iHyXeACDbWhrWww==;", "MyDB"));
//}
builder.Services.AddDbContext<MyContext>();

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
