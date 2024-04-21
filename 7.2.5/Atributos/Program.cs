var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(opt =>
{
    opt.ConstraintMap.Add("startsWith", typeof(StartsWithConstraint));
    opt.ConstraintMap.Add("endsWith", typeof(EndsWithConstraint));
});

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
