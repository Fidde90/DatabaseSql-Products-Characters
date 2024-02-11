using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Services.PlayerServices;
using Infrastructure.Repositories.PlayerRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services.PlayerServices
{
    public class PlayerService_Tests
    {
        private readonly PlayerDataContext _context = new
            (new DbContextOptionsBuilder<PlayerDataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

        [Fact]
        public void GetsAPlayerEntity_AndThenAddIt_ToTheDatabase_ReturnEntity()
        {
            // Arrange
            var PlayerService = new PlayerService(new PlayerRepository(_context));
            var player = new PlayerEntity
            {
                CharacterName = "TestPlayer",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };

            // Act
            var result = PlayerService.CreatePlayer(player);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetsAPlayerEntity_AndShouldNot_AddToTheDatabase_ReturnNull()
        {
            // Arrange
            var PlayerService = new PlayerService(new PlayerRepository(_context));
            var player = new PlayerEntity
            {
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };

            // Act
            var result = PlayerService.CreatePlayer(player);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetsAllPlayers_FromTheDatabase_WithKindRaceAndWeapons_ReturnList()
        {
            // Arrange
            var PlayerService = new PlayerService(new PlayerRepository(_context));

            // Act
            var result = PlayerService.GetAllPlayers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PlayerEntity>>(result);
        }

        [Fact]
        public void GetsOnePlayer_WithTheNameOfThePlayer_FromTheDatabase_ReturnEntity()
        {
            // Arrange
            var PlayerService = new PlayerService(new PlayerRepository(_context));
            var player = new PlayerEntity
            {
                CharacterName = "TestPlayer",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };
            // Act
            PlayerService.CreatePlayer(player);
            var result = PlayerService.GetPlayer(player.CharacterName);
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldNot_GetOnePlayer_FromTheDatabase_ReturnNull()
        {
            // Arrange
            var PlayerService = new PlayerService(new PlayerRepository(_context));
            var player = new PlayerEntity
            {
                CharacterName = "TestPlayer",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };
            // Act
            var result = PlayerService.GetPlayer(player.CharacterName);
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeletesOnePlayer_FromTheDataBase_ReturnTrue()
        {
            // Arrange
            var PlayerService = new PlayerService(new PlayerRepository(_context));
            var player = new PlayerEntity
            {
                CharacterName = "TestPlayer",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };
            // Act
            PlayerService.CreatePlayer(player);
            var result = PlayerService.DeletePlayer(player);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ShouldNotDeletesOnePlayer_FromTheDataBase_ReturnFalse()
        {
            // Arrange
            var PlayerService = new PlayerService(new PlayerRepository(_context));
            var player = new PlayerEntity
            {
                CharacterName = "TestPlayer",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };
            // Act
            var result = PlayerService.DeletePlayer(player);
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void UpdatesAPlayer_InTheDatabase_ReturnTrue()
        {
            // Arrange
            var PlayerService = new PlayerService(new PlayerRepository(_context));
            var player = new PlayerEntity
            {
                CharacterName = "TestPlayer",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };
            // Act
            var p = PlayerService.CreatePlayer(player);
            p.CharacterName = "Fredrik";
            var result = PlayerService.UpdatePlayer(p);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ShouldNot_UpdateAPlayer_InTheDatabase_ReturnFalse()
        {
            // Arrange
            var PlayerService = new PlayerService(new PlayerRepository(_context));
            var player = new PlayerEntity
            {
                CharacterName = "TestPlayer",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };
            // Act
            var result = PlayerService.UpdatePlayer(player);
            // Assert
            Assert.False(result);
        }
    }
}
