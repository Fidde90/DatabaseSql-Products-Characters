using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseWPFTest.Views;
using Infrastructure.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseWPFTest.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {      
        [ObservableProperty]
        private ObservableObject? _currentViewModel;

        private readonly IServiceProvider _serviceProvider;
        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            CurrentViewModel = _serviceProvider.GetRequiredService<MainPageViewModel>();
        }
    }
}
