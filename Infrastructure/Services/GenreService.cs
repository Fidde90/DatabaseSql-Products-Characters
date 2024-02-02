using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Dtos;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class GenreService
    {
        private readonly GenreRepository _genreRepository;

        public GenreService(GenreRepository gr)
        {
            _genreRepository = gr;
        }

        public List<GenreEntity> GetAllGenres()
        {
            var genres = _genreRepository.GetAllFromDB().ToList();
            return genres;
        }

        public bool UpdateGenre(Product entity)
        {
            try
            {
                var updated = _genreRepository.UpdateEntityInDB(entity, x => x.GenreName == entity.Genre);
                if (updated != null)
                    return true;
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }
    }
}
