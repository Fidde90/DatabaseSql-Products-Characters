using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class TracklistService
    {
        private readonly TracklistRepository _tracklistRepository;
        private readonly ProductRepository _productRepository;
        public TracklistService(TracklistRepository tr, ProductRepository pr)
        {
            _tracklistRepository = tr;
            _productRepository = pr;
        }

        public void CreateTracks(List<TracklistEntity> tracklist)
        {
            try
            {
                if (_productRepository.Exists(x => x.ArticleNumber == tracklist.First().ArticleNumber))
                {
                    foreach (var tracks in tracklist)
                    {
                        if (!_tracklistRepository.Exists(x => x.Title == tracks.Title))
                            _tracklistRepository.AddToDB(tracks, true);
                    }
                }
            }
            catch (Exception e) { Debug.WriteLine("ERROR :: " + e); }
        }

        public List<TracklistEntity> GetTracks(string articleNumber)
        {
            try
            {
                List<TracklistEntity> listToReturn = new List<TracklistEntity>();

                var list = _tracklistRepository.GetAll_TracksFromDB(x => x.ArticleNumber == articleNumber).ToList();

                for(int i =0; i < list.Count; i++)
                {
                    if (list[i].ArticleNumber == articleNumber)
                        listToReturn.Add(list[i]);
                }

                return listToReturn;
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return null!;
        }       
    }
}
