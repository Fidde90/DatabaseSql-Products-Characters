using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DatabaseSql_Products_Characters.Models;
using Infrastructure.Entities;
using Infrastructure.Services.PlayerServices;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace DatabaseSql_Products_Characters.ViewModels
{
    public partial class CreateCharacterViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly WeaponService _weaponService;
        private readonly RaceService _raceService;
        private readonly KindService _kindService;
        private readonly PlayerService _playerService;
        private readonly PlayerWeaponService _playerWeaponService;

        public CreateCharacterViewModel(IServiceProvider serviceProvider, WeaponService weaponService, RaceService raceService, KindService kindService, PlayerService playerService, PlayerWeaponService playerWeaponService)
        {
            _serviceProvider = serviceProvider;
            _weaponService = weaponService;
            _raceService = raceService;
            _kindService = kindService;
            _playerService = playerService;
            _playerWeaponService = playerWeaponService;

            Fill_Weapon_List();
            Fill_Races_Dropdown();
            Fill_Kinds_List();
        }

        [ObservableProperty]
        bool maleChecked = false;

        [ObservableProperty]
        bool femaleChecked = false;

        private string? _gender;

        [ObservableProperty]
        ObservableCollection<RaceEntity> listOfRaces = [];

        [ObservableProperty]
        ObservableCollection<string> kindsList = [];

        [ObservableProperty]
        ObservableCollection<WeaponEntity> weaponList = [];

        [ObservableProperty]
        ObservableCollection<WeaponEntity> playerWeapons = [];

        [ObservableProperty]
        PlayerForm playerForm = new();

        [ObservableProperty]
        AddWeaponForm weaponForm = new();

        [ObservableProperty]
        string weaponBtnEnabled = "true";

        [ObservableProperty]
        string addPlayerEnabled = "false";

        [RelayCommand]
        public void AddPlayer_BtnClick()
        {
               var race = _raceService.GetRace(PlayerForm.Race);
               var kind = _kindService.GetKind(PlayerForm.Kind);

                var newPlayer = new PlayerEntity
                {
                    CharacterName = PlayerForm.CharacterName,
                    Age = PlayerForm.Age,
                    Gender = _gender!,
                    Kind = kind.Id,
                    Race = race.Id,
                    RaceNavigation = race,
                    KindNavigation = kind
                };

                var createdPlayer = _playerService.CreatePlayer(newPlayer);

                WeaponBtnEnabled = "true";
                AddPlayerEnabled = "false";

                if (createdPlayer != null)
                    foreach (var weapon in PlayerWeapons)
                        _playerWeaponService.CreatePlayerWeapon(weapon, createdPlayer);
                else
                    MessageBox.Show("The player alredy exsists, try again or check your spelling");


            PlayerWeapons.Clear();
            PlayerForm = new();
        }

        [RelayCommand]
        public void AddWeapon_BtnClick()
        {
            if (!string.IsNullOrWhiteSpace(WeaponForm.Weapon))
            {
                var weapon = _weaponService.GetWeapon(WeaponForm.Weapon.ToLower());
                if (weapon != null)
                {
                    PlayerWeapons.Add(weapon);

                    if (PlayerWeapons.Count == 3)
                    {
                        WeaponBtnEnabled = "false";
                        AddPlayerEnabled = "true";
                    }
                }

                else
                    MessageBox.Show("The weapon does not exist, check your spelling");
            }
           
            WeaponForm = new();
        }

        [RelayCommand]
        public void Female_Checked()
        {
            FemaleChecked = true;
            MaleChecked = false;
            _gender = "Female";
        }

        [RelayCommand]
        public void Male_Checked()
        {
            MaleChecked = true;
            FemaleChecked = false;

            _gender = "Male";
        }

        [RelayCommand]
        public void AllPlayers_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AllPlayersViewModel>();
        }

        [RelayCommand]
        public void BackToMenu_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<MenuPageViewModel>();
        }

        private void Fill_Weapon_List()
        {
            WeaponList.Clear();
            var weapons = _weaponService.GetAllWeapons();

            if (weapons != null)
                for (int i = 0; i < weapons.Count; i++)
                    WeaponList.Add(weapons[i]);
        }

        public void Fill_Races_Dropdown()
        {
            ListOfRaces.Clear();
            var races = _raceService.GetAllRaces();

            if (races != null)
                for (int i = 0; i < races.Count; i++)
                    ListOfRaces.Add(races[i]);
        }

        private void Fill_Kinds_List()
        {
            KindsList.Clear();
            var kinds = _kindService.GetAllKinds();

            if (kinds != null)
                for (int i = 0; i < kinds.Count; i++)
                    KindsList.Add(kinds[i].KindName);
        }
    }
}
