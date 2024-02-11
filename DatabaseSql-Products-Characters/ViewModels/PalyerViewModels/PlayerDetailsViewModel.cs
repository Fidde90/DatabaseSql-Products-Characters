using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Infrastructure.Entities;
using Infrastructure.Services.PlayerServices;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace DatabaseSql_Products_Characters.ViewModels
{
    public partial class PlayerDetailsViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PlayerService _playerService;
        private readonly PlayerWeaponService _playerWeaponService;
        public PlayerDetailsViewModel(IServiceProvider serviceProvider, PlayerService playerService, PlayerWeaponService playerWeaponService)
        {
            _serviceProvider = serviceProvider;
            _playerService = playerService;
            _playerWeaponService = playerWeaponService;

            _playerDetails = _playerService.PlayerDetails;
            GetPlayerWeaponDetails();
        }

        [ObservableProperty]
        public PlayerEntity _playerDetails = new();

        [ObservableProperty]
        public PlayerWeaponEntity _playerWeaponDetails = new();

        [ObservableProperty]
        ObservableCollection<PlayerWeaponEntity> _weaponList = [];

        private void GetPlayerWeaponDetails()
        {
            WeaponList.Clear();
            var w = _playerWeaponService.GetPlayerWeapons(PlayerDetails);

            foreach (var weapon in w)
                WeaponList.Add(weapon);
        }

        [RelayCommand]
        public void Edit_BtnClick()
        {

            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<EditPlayerViewModel>();
        }

        [RelayCommand]
        public void BackTo_CreatePlayer_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CreateCharacterViewModel>();
        }

        [RelayCommand]
        public void BackTo_PlayerList_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AllPlayersViewModel>();
        }
    }
}

