
using Infrastructure.Entities;
using Infrastructure.Repositories.PlayerRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.PlayerServices
{
    public class WeaponService(WeaponRepository weaponRepository)
    {
        private readonly WeaponRepository _weaponRepository = weaponRepository;

        public List<WeaponEntity> GetAllWeapons()
        {
            try
            {
                var weapons = _weaponRepository.GetAllFromDB().ToList();
                return weapons;
            }
            catch (Exception ex) { Debug.WriteLine("Erorr: " + ex.Message); }
            return null!;
        }

        public WeaponEntity GetWeapon(string weapon)
        {
            try
            {
                var weaponToReturn = _weaponRepository.GetOneFromDB(x => x.WeaponName.ToLower() == weapon);
                if (weaponToReturn != null)
                    return weaponToReturn;
            }
            catch (Exception ex) { Debug.WriteLine("Error: " + ex.Message); }
            return null!;
        }
    }
}
