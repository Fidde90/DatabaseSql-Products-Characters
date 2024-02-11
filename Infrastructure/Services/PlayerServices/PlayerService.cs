using Infrastructure.Entities;
using Infrastructure.Repositories.PlayerRepositories;
using System.Diagnostics;

namespace Infrastructure.Services.PlayerServices
{
    public class PlayerService(PlayerRepository playerRepository)
    {
        private readonly PlayerRepository _playerRepository = playerRepository;
        public PlayerEntity PlayerDetails { get; set; } = null!;

        /// <summary>
        /// Creates a player.
        /// </summary>
        /// <param name="newPlayer">The player to add the the database</param>
        /// <returns>Return created player.</returns>
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

        /// <summary>
        /// Gets all players from the database.
        /// </summary>
        /// <returns>returns a list of all players, else null</returns>
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

        /// <summary>
        /// Gets one player from the database.
        /// </summary>
        /// <param name="playerName">Name of the player</param>
        /// <returns>returns the player, else null</returns>
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

        /// <summary>
        /// Deletes one player from the database.
        /// </summary>
        /// <param name="player">the player to delete</param>
        /// <returns>returns true if the player was deleted, else false</returns>
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

        /// <summary>
        /// Updates a player inte the database.
        /// </summary>
        /// <param name="updatedPlayer">Player with the new values</param>
        /// <returns>returns true if the players was updated, else false</returns>
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
