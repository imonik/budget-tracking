using FinanceAndBudgetTracking.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FinanceAndBudgetTracking.UI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinanceAndBudgetTracking.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Load the configuration from the appsettings.json file
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
            // Add services to the container.
            builder.Services.AddAuthentication(options => 
            { 
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            { 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            builder.Services.AddScoped<IAuthService, AuthService>(); // Corrected
            builder.Services.AddScoped<ITransactionService, TransactionService>(); // optional
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<JwtHandler>();

            builder.Services.AddHttpClient<IApiService, ApiService>()
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7025/api/");
                })
                .AddHttpMessageHandler<JwtHandler>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
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
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();
        }
    }
}
