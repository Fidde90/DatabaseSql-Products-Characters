using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Entities;
using Infrastructure.Services.PlayerServices;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace DatabaseSql_Products_Characters.ViewModels
{
    public partial class AllPlayersViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PlayerService _playerService;
        public AllPlayersViewModel(IServiceProvider serviceProvider, PlayerService playerService)
        {
            _serviceProvider = serviceProvider;
            _playerService = playerService;
            GetAllPlayers();
        }

        [ObservableProperty]
        ObservableCollection<PlayerEntity> players = [];

        [RelayCommand]
        public void DeletePlayer_BtnClick(PlayerEntity player)
        {
            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete item: {player.CharacterName}?", "Wait!", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                var Isdeleted = _playerService.DeletePlayer(player);
                Players.Clear();
                GetAllPlayers();
            }
        }

        [RelayCommand]
        public void BackTo_CreatePlayer_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CreateCharacterViewModel>();
        }

        [RelayCommand]
        public void PlayerDetails_BtnClick(PlayerEntity player)
        {
            _playerService.PlayerDetails = player;
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<PlayerDetailsViewModel>();
        }

        private void GetAllPlayers()
        {
            foreach (var player in _playerService.GetAllPlayers())
                Players.Add(player);
        }
    }
}
