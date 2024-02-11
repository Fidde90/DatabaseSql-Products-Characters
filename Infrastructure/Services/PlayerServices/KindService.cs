using Infrastructure.Entities;
using Infrastructure.Repositories.PlayerRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.PlayerServices
{
    public class KindService(KindRepository kindRepository)
    {
        private readonly KindRepository _kindRepository = kindRepository;

        public List<KindEntity> GetAllKinds()
        {
            try
            {
                var kinds = _kindRepository.GetAllFromDB().ToList();
                return kinds;
            }
            catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
            return null!;
        }

        public KindEntity GetKind(string kindName)
        {
            try
            {
                var kindToReturn = _kindRepository.GetOneFromDB(x => x.KindName == kindName);
                if (kindToReturn != null)
                    return kindToReturn;
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return null!;
        }
        public KindEntity GetKind(int kind)
        {
            try
            {
                var kindToReturn = _kindRepository.GetOneFromDB(x => x.Id == kind);
                if (kindToReturn != null)
                    return kindToReturn;
            }
            catch (Exception e) { Debug.WriteLine("Error : " + e.Message); }
            return null!;
        }
    }
}
