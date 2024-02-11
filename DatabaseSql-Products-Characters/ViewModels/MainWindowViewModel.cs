using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseSql_Products_Characters.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {      
        [ObservableProperty]
        private ObservableObject? _currentViewModel;

        private readonly IServiceProvider _serviceProvider;
        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            CurrentViewModel = _serviceProvider.GetRequiredService<MenuPageViewModel>();
        }
    }
}
