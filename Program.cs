var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication().AddCookie("MyCookieAuth", opt =>
{
    opt.Cookie.Name = "MyCookieAuth";
    opt.LoginPath = "/Account/Login";
    opt.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("MustBelongToHr", policy => policy.RequireClaim("Department", "HR"));
});
builder.Services.AddRazorPages();


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
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

