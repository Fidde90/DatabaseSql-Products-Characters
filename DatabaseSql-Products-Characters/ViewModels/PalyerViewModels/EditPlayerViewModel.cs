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
    public partial class EditPlayerViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CreateCharacterViewModel _createCharacterViewModel;
        private readonly KindService _kindService;
        private readonly RaceService _raceService;
        private readonly WeaponService _weaponService;
        private readonly PlayerService _playerService;
        private readonly PlayerWeaponService _playerWeaponService;
        public EditPlayerViewModel(IServiceProvider serviceProvider, CreateCharacterViewModel createCharacterViewModel, RaceService raceService, KindService kindService, WeaponService weaponService, PlayerService playerService, PlayerWeaponService playerWeaponService)
        {
            _serviceProvider = serviceProvider;
            _createCharacterViewModel = createCharacterViewModel;
            _raceService = raceService;
            _kindService = kindService;
            _weaponService = weaponService;
            _playerService = playerService;
            _playerWeaponService = playerWeaponService;
            _playerDetails = _playerService.PlayerDetails;
            _defaultRace = _raceService.GetRace(_playerDetails.Race).RaceName;
            _defaultKind = _kindService.GetKind(_playerDetails.Kind).KindName;
            _genderCheckValue = _playerDetails.Gender;

            if(_genderCheckValue == "Male")
                maleChecked = true;
            else
                femaleChecked = true;

            Fill_Races_Dropdown();
            Fill_Kinds_List();
        }

        [ObservableProperty]
        bool maleChecked = false;

        [ObservableProperty]
        bool femaleChecked = false;

        [ObservableProperty]
        public string _genderCheckValue;

        [ObservableProperty]
        public string _defaultRace;

        [ObservableProperty]
        public string _defaultKind;

        [ObservableProperty]
        PlayerForm playerForm = new();

        [ObservableProperty]
        ObservableCollection<RaceEntity> listOfRaces = [];

        [ObservableProperty]
        ObservableCollection<string> kindsList = [];

        [ObservableProperty]
        public PlayerEntity _playerDetails = new();

        [RelayCommand]
        public void SaveChanges_BtnClick(PlayerEntity update)
        {
            if (!string.IsNullOrWhiteSpace(update.CharacterName) && update.Age != 0 && !string.IsNullOrWhiteSpace(GenderCheckValue))
            {
                var race = _raceService.GetRace(DefaultRace);
                var kind = _kindService.GetKind(DefaultKind);

                var updatedPlayer = new PlayerEntity
                {
                    Id = PlayerDetails.Id,
                    CharacterName = update.CharacterName,
                    Age = update.Age,
                    Gender = GenderCheckValue!,
                    Kind = kind.Id,
                    Race = race.Id,
                    RaceNavigation = race,
                    KindNavigation = kind
                };

                var updated = _playerService.UpdatePlayer(updatedPlayer);

                if (updated)
                    MessageBox.Show("Player updated"!);
            }

            PlayerForm = new();
        }
        [RelayCommand]

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

        [RelayCommand]
        public void Female_Checked()
        {
            FemaleChecked = true;
            MaleChecked = false;
            GenderCheckValue = "Female";
        }

        [RelayCommand]
        public void Male_Checked()
        {
            MaleChecked = true;
            FemaleChecked = false;

            GenderCheckValue = "Male";
        }

        [RelayCommand]
        public void BackTo_CreatePlayer_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CreateCharacterViewModel>();
        }

        [RelayCommand]
        public void BackTo_Details_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<PlayerDetailsViewModel>();
        }

        [RelayCommand]
        public void BackTo_All_BtnClick()
        {
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindowViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AllPlayersViewModel>();
        }
    }
}
