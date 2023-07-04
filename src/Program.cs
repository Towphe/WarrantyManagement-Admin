using Microsoft.EntityFrameworkCore;
using src.Model.Repo;
using src.Services.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WarrantyrepoContext>(opts =>{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("db_key"));
});
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAccountManager, AccountManager>();
builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(opts => {
    opts.ExpirationScanFrequency = TimeSpan.FromSeconds(30);
});
builder.Services.AddSession(opts =>{
    opts.IdleTimeout = TimeSpan.FromMinutes(1);
    opts.Cookie.Name = ".warranty-repo.Session";
    opts.Cookie.HttpOnly = true;
    opts.Cookie.IsEssential = true;
    opts.Cookie.MaxAge = TimeSpan.FromMinutes(1);
});

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

app.UseSession();

app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
