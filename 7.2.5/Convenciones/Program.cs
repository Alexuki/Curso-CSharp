var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


// 1) Configurar rutas para acceder a las acciones definidas en los controladores

// Products routes
app.MapControllerRoute(
    name: "AllProducts",
    pattern: "products/all",
    defaults: new
    {
        controller = "products",
        action = "index"
    }
);

app.MapControllerRoute(
    name: "ProductsByCategory",
    pattern: "products/category/{category}",
    defaults: new
    {
        controller = "Products",
        action = "bycategory"
    }
);

app.MapControllerRoute(
    name: "ViewProduct",
    pattern: "products/{id}",
    defaults: new
    {
        controller = "products",
        action = "view"
    }
);


// Friends routes
app.MapControllerRoute(
    name: "DeleteFriend",
    pattern: "delete/friends/{id}",
    defaults: new { controller = "friends", action = "delete" },
    constraints: new { id = @"\d{1,5}" }
);

app.MapControllerRoute(
    name: "ViewFriend",
    pattern: "friends/view/{name}",
    defaults: new
    {
        controller = "friends",
        action = "view"
    }
);

app.MapControllerRoute(
    name: "EditFriend",
    pattern: "friends/edit/{id}",
    defaults: new
    {
        controller = "friends",
        action = "edit"
    }
);

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}",
    dataTokens: new { source = "default" }
);

app.Run();
