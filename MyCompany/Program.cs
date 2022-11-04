using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyCompany.Domain;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.EntityFramework;
using MyCompany.Service;


var builder = WebApplication.CreateBuilder(args);

//ïîäêëþ÷àåì êîíôèã èç appsetting.json
builder.Configuration.Bind("Project", new Config());

//ïîäêëþ÷àåì íóæíûé ôóíêöèîíàë ïðèëîæåíèÿ â êà÷åñòâå ñåðâèñîâ
builder.Services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
builder.Services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
builder.Services.AddTransient<DataManager>();

//ïîäêëþ÷àåì êîíòåêñò ÁÄ
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

//íàñòðàèâàåì identity ñèñòåìó
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//íàñòðàèâàåì authentication cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "myCompanyAuth";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
});

//íàñòðàèâàåì ïîëèòèêó àâòîðèçàöèè äëÿ Admin area
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
});

// Add services to the container.
<<<<<<< HEAD
=======
//builder.Services.AddControllersWithViews();
>>>>>>> 3bc9267450d1a336a1bf54656b9ab3e0c6bc7beb
builder.Services.AddControllersWithViews(x =>
{
    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//ïîäêëþ÷àåì àóòåíòèôèêàöèþ è àâòîðèçàöèþ
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
