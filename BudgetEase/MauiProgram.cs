
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BudgetEasee.Data;
using BudgetEasee.Services;
using Microsoft.AspNetCore.Components; // Added namespace for NavigationManager
using BudgetEasee;
using Microsoft.EntityFrameworkCore.Sqlite;


namespace BudgetEase
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            // Register DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=BudgetEasee.db"));

            // Register Services
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddScoped<TransactionService>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
