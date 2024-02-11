using Infrastructure.Entities;
using Infrastructure.Repositories.PlayerRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.PlayerServices
{
    public class RaceService(RaceRepository raceRepository)
    {
        private readonly RaceRepository _raceRepository = raceRepository;

        public List<RaceEntity> GetAllRaces()
        {
            try
            {
                var races = _raceRepository.GetAllFromDB().ToList();
                return races;
            }
            catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
            return null!;
        }
        public RaceEntity GetRace(string raceName)
        {
            try
            {
                var raceToReturn = _raceRepository.GetOneFromDB(x => x.RaceName == raceName);
                if (raceToReturn != null)
                    return raceToReturn;
            }
            catch (Exception ex) { Debug.WriteLine("Error: " + ex.Message); }
            return null!;
        }

        public RaceEntity GetRace(int race)
        {
            try
            {
                var raceToReturn = _raceRepository.GetOneFromDB(x => x.Id == race);
                if (raceToReturn != null)
                    return raceToReturn;
            }
            catch (Exception ex) { Debug.WriteLine("Error: " + ex.Message); }
            return null!;
        }
    }
}
