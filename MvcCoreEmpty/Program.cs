var builder = WebApplication.CreateBuilder(args);

//el constructor indicara como generar la app
//mediante metodos le indicamos l tipado de app
builder.Services.AddControllersWithViews();

var app = builder.Build();

//wwwroot
app.UseStaticFiles();
//mvc
app.MapControllerRoute
    (
        name:"default",
        pattern:"{controller=Home}/{action=Index}"
    );

app.Run();
