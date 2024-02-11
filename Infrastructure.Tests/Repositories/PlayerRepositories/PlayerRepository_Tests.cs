using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories.PlayerRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.PlayerRepositories
{
    public class PlayerRepository_Tests
    {
        private readonly PlayerDataContext _context = new 
            (new DbContextOptionsBuilder<PlayerDataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

        [Fact]
        public void AddOnePLayer_To_DataBase_Then_Return_Entity()
        {
            // Arrange
            var PlayerRepository = new PlayerRepository(_context);
            var player = new PlayerEntity
            {
                CharacterName = "TestPlayer",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };

            // Act
            var result = PlayerRepository.AddToDB(player);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void ShouldNotAdd_OnePLayer_To_DataBase_Then_Return_Null()
        {
            // Arrange
            var PlayerRepository = new PlayerRepository(_context);
            var player = new PlayerEntity
            {
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };

            // Act
            var result = PlayerRepository.AddToDB(player);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAllPlayersFromDatabase_ReturnList()
        {
            // Arrange
            var PlayerRepository = new PlayerRepository(_context);

            // Act
            var result = PlayerRepository.GetAllFromDB();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PlayerEntity>>(result);
        }

        [Fact]
        public void GetOnePlayerFromDatabase_ReturnEntity()
        {
            // Arrange
            var PlayerRepository = new PlayerRepository(_context);
            var player = new PlayerEntity
            {
                CharacterName = "Test",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };

            // Act
            var added = PlayerRepository.AddToDB(player);
            var result = PlayerRepository.GetOneFromDB(x => x.CharacterName == player.CharacterName);

            // Assert

            Assert.NotNull(added);
            Assert.NotNull(result);
            Assert.IsType<PlayerEntity>(result);
        }

        [Fact]
        public void ShouldNot_GetOnePlayerFromDatabase_ReturnNull()
        {
            // Arrange
            var PlayerRepository = new PlayerRepository(_context);
            var player = new PlayerEntity
            {
                CharacterName = "Test",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };

            // Act

            var result = PlayerRepository.GetOneFromDB(x => x.CharacterName == player.CharacterName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Update_One_Player_InTheDataBase_ReturnEntity()
        {
            // Arrange
            var PlayerRepository = new PlayerRepository(_context);
            var player = new PlayerEntity
            {
                CharacterName = "Test",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };
            // Act
            var p = PlayerRepository.AddToDB(player);
           
            p.CharacterName = "Fredrik";
            var result = PlayerRepository.UpdateEntityInDB(p, x => x.Id == p.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PlayerEntity>(result);
        }

        [Fact]
        public void ShouldNotUpdate_One_Player_InTheDataBase_ReturnNull()
        {
            // Arrange
            var PlayerRepository = new PlayerRepository(_context);
            var player = new PlayerEntity
            {
                CharacterName = "Test",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };
            // Act
            player.CharacterName = "Fredrik";
            var result = PlayerRepository.UpdateEntityInDB(player, x => x.Id == player.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Delete_One_Player_From_DataBase_Return_True()
        {
            // Arrange
            var PlayerRepository = new PlayerRepository(_context);
            var player = new PlayerEntity
            {
                CharacterName = "Test",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };
            // Act
            PlayerRepository.AddToDB(player);
            var result = PlayerRepository.DeleteFromDB(x => x.CharacterName == player.CharacterName);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ShouldNotDelete_One_Player_From_DataBase_Return_False()
        {
            // Arrange
            var PlayerRepository = new PlayerRepository(_context);
            var player = new PlayerEntity
            {
                CharacterName = "Test",
                Age = 33,
                Gender = "Male",
                Kind = 0,
                Race = 0
            };
            // Act
            var result = PlayerRepository.DeleteFromDB(x => x.CharacterName == player.CharacterName);

            // Assert
            Assert.False(result);
        }
    }
}
