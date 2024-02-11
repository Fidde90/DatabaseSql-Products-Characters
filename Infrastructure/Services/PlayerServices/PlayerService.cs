using Infrastructure.Entities;
using Infrastructure.Repositories.PlayerRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.PlayerServices
{
    public class PlayerService(PlayerRepository playerRepository)
    {
        private readonly PlayerRepository _playerRepository = playerRepository;
        public PlayerEntity PlayerDetails { get; set; } = null!;
        public PlayerEntity CreatePlayer(PlayerEntity newPlayer)
        {
            try
            {
                if (newPlayer != null)
                {
                    if (!_playerRepository.Exists(x => x.CharacterName == newPlayer.CharacterName) && newPlayer.CharacterName != string.Empty)
                    {
                        var player = _playerRepository.AddToDB(newPlayer);
                        if (player != null)
                            return player;
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine("Error: " + ex.Message); }
            return null!;
        }
        public List<PlayerEntity> GetAllPlayers()
        {
            try
            {
                var players = _playerRepository.GetAllFromDB().ToList();
                return players;
            }
            catch (Exception ex) { Debug.WriteLine("ERROR: " + ex.Message); }
            return null!;
        }
        public PlayerEntity GetPlayer(string playerName)
        {
            try
            {
                var playerToReturn = _playerRepository.GetOneFromDB(x => x.CharacterName == playerName);
                if (playerToReturn != null)
                    return playerToReturn;
            }
            catch (Exception ex) { Debug.WriteLine("Error: " + ex.Message); }
            return null!;
        }
        public bool DeletePlayer(PlayerEntity player)
        {
            try
            {
                if (_playerRepository.Exists(x => x.CharacterName == player.CharacterName))
                {
                    var Isdeleted = _playerRepository.DeleteFromDB(x => x.CharacterName == player.CharacterName);
                    if (Isdeleted)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }
        public bool UpdatePlayer(PlayerEntity updatedPlayer)
        {
            try
            {
                if(_playerRepository.Exists(p => p.Id == updatedPlayer.Id))
                {
                   var updated =  _playerRepository.UpdateEntityInDB(updatedPlayer, p => p.Id == updatedPlayer.Id);
                    if(updated != null)
                        return true;
                }
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }
    }
}
