using DatabaseWPFTest.ViewModels;
using DatabaseWPFTest.Views;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
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

                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainWindowViewModel>();

                services.AddSingleton<MainPageViewModel>();
                services.AddTransient<MainPage>();

                services.AddTransient<EditPageViewModel>();
                services.AddTransient<EditPage>();

                services.AddTransient<DetailsViewModel>();
                services.AddTransient<DetailsPage>();

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
