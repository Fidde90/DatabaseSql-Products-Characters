using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseSql_Products_Characters.ViewModels
{
    public partial class MenuPageViewModel: ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        public MenuPageViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        public void MenuChoicePlayer_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CreateCharacterViewModel>();
        }

        [RelayCommand]
        public void MenuChoiceRecords_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MainPageViewModel>();
        }
    }
}
