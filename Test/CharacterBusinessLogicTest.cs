using AutoMapper;
using CharacterApi.BusinessLogic;
using CharacterApi.BusinessLogic.Models;
using CharacterApi.Models;
using CharacterApi.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace Test
{
    public class CharacterBusinessLogicTest
    {
        #region Private fields
        private readonly Mock<ICharacterRepository> _characterRepository;
        private readonly IMapper _mapper;
        private MapperConfiguration _config;
        private ICharacterBusinessLogic _characterBusinessLogic;
        private Mock<IHttpContextAccessor> _contextAccessor;
        #endregion
        #region Public constructor
        public CharacterBusinessLogicTest()
        {
            _characterRepository = new Mock<ICharacterRepository>();
            _contextAccessor = new Mock<IHttpContextAccessor>();
            _config = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "Character.Api" }));

            _mapper = _config.CreateMapper();
        }
        #endregion

        #region GetCharacters tests
        [Fact]
        public void GetCharacters_GetCharactersRepositoryMethodReturnsData_ResponseShouldNotBeNullDataShouldNotBeNullMessageShouldBeNullSuccessShouldBeTrue()
        {
            //Arrange
            _characterRepository.Setup(cr => cr.GetCharacters()).Returns(GetCharactersResponse());
            _characterBusinessLogic = new CharacterBusinessLogic(_mapper, _characterRepository.Object, _contextAccessor.Object);

            //Act
            var response = _characterBusinessLogic.GetCharacters();

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().NotBeNull();
            response.Message.Should().BeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void GetCharacters_GetCharactersRepositoryMethodThrowsException_ResponseShouldNotBeNullDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeFalse()
        {
            //Arrange
            _characterRepository.Setup(cr => cr.GetCharacters()).Throws(new Exception("Exception in character repository method GetCharacters"));
            _characterBusinessLogic = new CharacterBusinessLogic(_mapper, _characterRepository.Object, _contextAccessor.Object);

            //Act
            var response = _characterBusinessLogic.GetCharacters();

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeFalse();
        }
        #endregion

        #region GetCharacterById tests
        [Fact]
        public void GetCharacterById_GetCharacterByIdRepositoryMethodReturnsCharacter_ResponseShouldNotBeNUllDataShouldNotBeNullMessageShouldBeNullSuccessShouldBeTrue()
        {
            //Arrange
            int id = 1;
            _characterRepository.Setup(cr => cr.GetCharacterById(It.IsAny<int>())).Returns(GetCharacterByIdResponse());
            _characterBusinessLogic = new CharacterBusinessLogic(_mapper, _characterRepository.Object, _contextAccessor.Object);

            //Act
            var response = _characterBusinessLogic.GetCharacterById(id);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().NotBeNull();
            response.Message.Should().BeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void GetCharacterById_GetCharacterByIdRepositoryMethodReturnsNull_ResponseShouldNotBeNUllDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeTrue()
        {
            //Arrange
            int id = 2;
            _characterRepository.Setup(cr => cr.GetCharacterById(It.IsAny<int>())).Returns(GetCharacterByIdResponse(false));
            _characterBusinessLogic = new CharacterBusinessLogic(_mapper, _characterRepository.Object, _contextAccessor.Object);

            //Act
            var response = _characterBusinessLogic.GetCharacterById(id);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void GetCharacterById_GetCharacterByIdRepositoryMethodThrowsException_ResponseShouldNotBeNUllDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeFalse()
        {
            //Arrange
            int id = 3;
            _characterRepository.Setup(cr => cr.GetCharacterById(It.IsAny<int>())).Throws(new Exception("Exception in character repository method GetCharacterById"));
            _characterBusinessLogic = new CharacterBusinessLogic(_mapper, _characterRepository.Object, _contextAccessor.Object);

            //Act
            var response = _characterBusinessLogic.GetCharacterById(id);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeFalse();
        }
        #endregion
        #region CreateCharacter tests
        [Fact]
        public void CreateCharacter_CreateCharacterRepositoryReturnsNonZero_ResponseShouldNotBeNullDataShouldNotBeNullMessageShouldBeNullSuccessShouldBeTrue()
        {
            //Arrange
            var characterPost = CreateCharacterRequest();
            _characterRepository.Setup(cr => cr.CreateCharacter(It.IsAny<Character>())).Returns(2);
            _contextAccessor.Setup(h => h.HttpContext.User).Returns(new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new Claim[] { new Claim(ClaimTypes.NameIdentifier, "Unit test") }
                        )));
            _characterBusinessLogic = new CharacterBusinessLogic(_mapper, _characterRepository.Object, _contextAccessor.Object);

            //Act
            var response = _characterBusinessLogic.CreateCharacter(characterPost);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().NotBeNull();
            response.Message.Should().BeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void CreateCharacter_CreateCharacterRepositoryReturnsZero_ResponseShouldNotBeNullDataShouldNotBeNullMessageShouldBeNullSuccessShouldBeTrue()
        {
            //Arrange
            var characterPost = CreateCharacterRequest();
            _characterRepository.Setup(cr => cr.CreateCharacter(It.IsAny<Character>())).Returns(0);
            _contextAccessor.Setup(h => h.HttpContext.User).Returns(new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new Claim[] { new Claim(ClaimTypes.NameIdentifier, "Unit test") }
                        )));
            _characterBusinessLogic = new CharacterBusinessLogic(_mapper, _characterRepository.Object, _contextAccessor.Object);

            //Act
            var response = _characterBusinessLogic.CreateCharacter(characterPost);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void CreateCharacter_CreateCharacterRepositoryThrowsException_ResponseShouldNotBeNullDataShouldNotBeNullMessageShouldBeNullSuccessShouldBeFalse()
        {
            //Arrange
            var characterPost = CreateCharacterRequest();
            _characterRepository.Setup(cr => cr.CreateCharacter(It.IsAny<Character>())).Throws(new Exception("Exception in character repository method CreateCharacter"));
            _contextAccessor.Setup(h => h.HttpContext.User).Returns(new ClaimsPrincipal(
                        new ClaimsIdentity(
                            new Claim[] { new Claim(ClaimTypes.NameIdentifier, "Unit test") }
                        )));
            _characterBusinessLogic = new CharacterBusinessLogic(_mapper, _characterRepository.Object, _contextAccessor.Object);

            //Act
            var response = _characterBusinessLogic.CreateCharacter(characterPost);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeFalse();
        }
        #endregion
        #region Private methods
        private List<Character> GetCharactersResponse()
        {
           return new List<Character> {
                    new Character()
                    {
                        Id = 1,
                        Name = "Test character",
                        Health = 0,
                        Mana = 1,
                        BaseAgility = 1,
                        BaseFaith = 1,
                        BaseIntelligence = 1,
                        BaseStrength = 1,
                        CreatedBy = "Unit test"
                    }
                };
        }

        private Character GetCharacterByIdResponse(bool success = true)
        {
            if (success)
            {
                return new Character()
                {
                    Id = 1,
                    Name = "Test character",
                    Health = 0,
                    Mana = 1,
                    BaseAgility = 1,
                    BaseFaith = 1,
                    BaseIntelligence = 1,
                    BaseStrength = 1,
                    CreatedBy = "Unit test",
                    Items = new List<Item>
                    {
                        new Item()
                        {
                            Name = "Unit test item",
                            Description = "Item for unit testing",
                            BonusStrength = 5,
                            BonusAgility = 1,
                            BonusFaith = 1,
                            BonusIntelligence = 1
                        }
                    }
                };
            }
            else
            {
                return null;
            }
        }

        private CharacterPost CreateCharacterRequest()
        {
            return new CharacterPost()
            {
                Name = "Unit Test",
                Health = 1,
                Mana = 2,
                BaseStrength = 3,
                BaseAgility = 4,
                BaseIntelligence = 5,
                BaseFaith = 6,
                Class = new ClassPost() { Name = "Unit test name", Description = "Unit test description"}
            };
        }
        #endregion
    }
}