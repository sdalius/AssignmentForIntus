using Microsoft.EntityFrameworkCore;
using TestForDotNet.Components;
using TestForDotNet.MapperProfile;
using TestForDotNet.Repository.AutoMapper;
using TestForDotNet.Repository.Models.Context;
using TestForDotNet.Repository.Repositories;
using TestForDotNet.Repository.Repositories.Interfaces;
using TestForDotNet.Services;
using TestForDotNet.Services.Interface;

namespace TestForDotNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddDbContextFactory<OrdersDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDatabaseConnection"));
                options.EnableSensitiveDataLogging(true);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            builder.Services.AddAutoMapper(typeof(OrderProfile), typeof(ViewModelProfile));

            builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
            builder.Services.AddScoped<ISubElementRepository, SubElementRepository>();
            builder.Services.AddScoped<IWindowRepository, WindowRepository>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IWindowService, WindowService>();
            builder.Services.AddScoped<ISubElementService, SubElementService>();
            builder.Services.AddLogging(loggingBuilder => {
                loggingBuilder.AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
                loggingBuilder.AddDebug();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
