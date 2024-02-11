using DatabaseSql_Products_Characters.ViewModels;
using DatabaseSql_Products_Characters.Views;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Repositories.PlayerRepositories;
using Infrastructure.Services;
using Infrastructure.Services.PlayerServices;
using Infrastructure.Services.ProductServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace DatabaseSql_Products_Characters
{
    public partial class App : Application
    {
        private IHost builder;
        public App()
        {
            builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=localhost;Initial Catalog=ProductCatalog;Integrated Security=True;Trust Server Certificate=True"));
                services.AddDbContext<PlayerDataContext>(x => x.UseSqlServer(@"Data Source=LocalHost;Initial Catalog=PlayerCatalog;Integrated Security=True;Trust Server Certificate=True"));

                services.AddSingleton<MainWindow>();

                services.AddTransient<MenuPage>();
                services.AddTransient<CreateCharacterPage>();
                services.AddTransient<MainPage>();
                services.AddTransient<DetailsPage>();
                services.AddTransient<EditPage>();
                services.AddTransient<AllPlayersPage>();
                services.AddTransient<PlayerDetailsPage>();
                services.AddTransient<EditPlayerPage>();

                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<MenuPageViewModel>();
                services.AddSingleton<CreateCharacterViewModel>();
                services.AddSingleton<MainPageViewModel>();
                services.AddTransient<EditPageViewModel>();
                services.AddTransient<DetailsViewModel>();
                services.AddTransient<AllPlayersViewModel>();
                services.AddTransient<PlayerDetailsViewModel>();
                services.AddTransient<EditPlayerViewModel>();

                services.AddScoped<WeaponService>();
                services.AddScoped<RaceService>();
                services.AddScoped<KindService>();
                services.AddScoped<PlayerService>();
                services.AddScoped<PlayerWeaponService>();

                services.AddScoped<WeaponRepository>();
                services.AddScoped<RaceRepository>();
                services.AddScoped<KindRepository>();
                services.AddScoped<PlayerRepository>();
                services.AddScoped<PlayerWeaponRepository>();

                services.AddScoped<ProductService>();
                services.AddScoped<GenreService>();
                services.AddScoped<CategoryService>();
                services.AddScoped<TracklistService>();
                services.AddScoped<SpecificationService>();
                services.AddScoped<DescriptionService>();

                services.AddScoped<DescriptionRepository>();
                services.AddScoped<ProductRepository>();
                services.AddScoped<GenreRepository>();
                services.AddScoped<CategoryRepository>();
                services.AddScoped<SpecificationRepository>();
                services.AddScoped<TracklistRepository>();

            }).Build();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            builder.Start();
            var mainWindow = builder.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
