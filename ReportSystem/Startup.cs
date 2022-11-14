using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReportSystem.DB;
using ReportSystem.Models;
using ReportSystem.Repositories;

namespace ReportSystem;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        //general authentication
        services.AddRazorPages(options => { }).AddMvcOptions(options =>
        {
            options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                _ => "Het veld moet ingevuld worden.");
            options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(_ =>
                "Dit is een verkeerde waarde voor dit veld.");
        });

        //Mysql Version
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

        //Db connection
        services.AddDbContext<AuthDbContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("AuthDbContext"), serverVersion)); //connectionstring from config.json
        //custom rules for identity framework 
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.";
        }).AddEntityFrameworkStores<AuthDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            // Default Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        });
        //if authorized pages gets opened without being logged in we go to /inloggen
        services.ConfigureApplicationCookie(config =>
        {
            config.LoginPath = "/User/Login";
        });

        services.Configure<SecurityStampValidatorOptions>(options =>
        {
            options.ValidationInterval = TimeSpan.FromMinutes(30);
        });

        services.AddHttpContextAccessor();
        
        services.AddScoped<UserRepository>();
        services.AddScoped<CrudRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        //error handling
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }


        app.UseHttpsRedirection();
        app.UseStaticFiles();

        //routing
        app.UseRouting();

        //cookies
        app.UseCookiePolicy();

        //Authentication
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
    }
}