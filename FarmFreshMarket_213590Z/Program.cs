using Castle.Core.Smtp;
using FarmFreshMarket_213590Z.Controllers;
using FarmFreshMarket_213590Z.Data;
using FarmFreshMarket_213590Z.Models;
using FarmFreshMarket_213590Z.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Twilio.Clients;
using AspNet.Security.OAuth.Validation;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



//Google ReCaptcha
builder.Services.Configure<GoogleCaptchaConfig>(builder.Configuration.GetSection("GoogleReCaptcha"));
builder.Services.AddTransient(typeof(GoogleCaptchaService));

builder.Services.AddDbContext<AuthDbContext>();
builder.Services.AddIdentity<AuthUser, IdentityRole>(options =>
{
	options.User.RequireUniqueEmail = true;
	options.Lockout.AllowedForNewUsers = true;
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
	options.Lockout.MaxFailedAccessAttempts = 3;

})
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

//Add OAuthValidation for Google Login
builder.Services.AddAuthentication(OAuthValidationDefaults.AuthenticationScheme)
    .AddOAuthValidation("Google");



// Secure CreaditCardNumber
builder.Services.AddDataProtection();

// Session Management
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//save session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(100);
});
builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.Name = "AspNetCore.Identity.Application";
	options.ExpireTimeSpan = TimeSpan.FromSeconds(100);
	options.SlidingExpiration = true;
	options.AccessDeniedPath = "/Errors/Error401";
});
builder.Services.Configure<IdentityOptions>(options =>
{

	// Lockout settings
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(80);
	options.Lockout.MaxFailedAccessAttempts = 3;
	options.Lockout.AllowedForNewUsers = true;


});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Customed Error Message
app.UseStatusCodePages(context =>
{
	context.HttpContext.Response.ContentType = "text/plain";

	switch (context.HttpContext.Response.StatusCode)
	{
		case 404:
			context.HttpContext.Response.Redirect("/Errors/Error404");
			break;
		case 403:
			context.HttpContext.Response.Redirect("/Errors/Error403");
			break;
		default:
			context.HttpContext.Response.Redirect("/Errors/Error");
			break;
	}

	return Task.CompletedTask;

});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();