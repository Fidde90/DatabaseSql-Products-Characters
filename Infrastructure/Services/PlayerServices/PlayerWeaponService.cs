using Infrastructure.Entities;
using Infrastructure.Repositories.PlayerRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.PlayerServices
{
    public class PlayerWeaponService(PlayerWeaponRepository playerWeaponRepository, PlayerService playerService, WeaponService weaponService)
    {
        private readonly PlayerWeaponRepository _playerWeaponRepository = playerWeaponRepository;
        private readonly PlayerService _playerService = playerService;
        private readonly WeaponService _weaponService = weaponService;

        public bool CreatePlayerWeapon(WeaponEntity weapon, PlayerEntity createdPlayer)
        {
            try
            {
                var palyerWeaponEntity = new PlayerWeaponEntity
                {
                    Player = _playerService.GetPlayer(createdPlayer.CharacterName).Id,
                    Weapon = _weaponService.GetWeapon(weapon.WeaponName).Id,
                    PlayerNavigation = createdPlayer,
                    WeaponNavigation = _weaponService.GetWeapon(weapon.WeaponName),
                    Quantity = 1
                };

                var addedPlayerWeapon = _playerWeaponRepository.AddToDB(palyerWeaponEntity);
                createdPlayer.PlayerWeapons.Add(palyerWeaponEntity);
         
                if (addedPlayerWeapon != null)
                    return true;
            }
            catch (Exception ex) { Debug.WriteLine("Error: " + ex.Message); }
            return false;
        }
        public IEnumerable<PlayerWeaponEntity> GetPlayerWeapons(PlayerEntity player)
        {
            try
            {
                List<PlayerWeaponEntity> listToReturn = new List<PlayerWeaponEntity>();

                var weaponList = _playerWeaponRepository.GetAllPlayerWeaponsFromDB(x => x.WeaponNavigation.Id == player.Id).ToList();
                if (weaponList != null)
                {
                    for(int i = 0; i < weaponList.Count; i++)
                    {
                        if (weaponList[i].Player == player.Id)
                            listToReturn.Add(weaponList[i]);
                    }
                    return listToReturn;
                }                   
            }
            catch (Exception ex) { Debug.WriteLine("Error: " + ex.Message); }
            return null!;
        }
    
        public bool DeletePlayerWeapons(int id) 
        {
            try
            {
                if (_playerWeaponRepository.Exists(p => p.Player == id))
                {
                   var deleted = _playerWeaponRepository.DeleteFromDB(x => x.Player == id);
                    if (deleted)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }
    }
}
