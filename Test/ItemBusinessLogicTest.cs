using AutoMapper;
using CharacterApi.BusinessLogic;
using CharacterApi.BusinessLogic.Models;
using CharacterApi.Models;
using CharacterApi.Repository;
using FluentAssertions;
using Moq;

namespace Test
{
    public class ItemBusinessLogicTest
    {
        #region Private fields
        private readonly Mock<IItemRepository> _itemRepository;
        private readonly IMapper _mapper;
        private MapperConfiguration _config;
        private IItemBusinessLogic _itemBusinessLogic;
        #endregion
        #region Public Constructor
        public ItemBusinessLogicTest()
        {
            _itemRepository = new Mock<IItemRepository>();
            _config = new MapperConfiguration(cfg => cfg.AddMaps(new[] { "Character.Api" }));
            _mapper = _config.CreateMapper();
        }
        #endregion
        #region GetItems tests
        [Fact]
        public void GetItems_GetItemsRepositoryMethodReturnsData_ResponseShouldNotBeNullDataShouldNotBeNullMessageShouldBeNullSuccessShouldBeTrue()
        {
            //Arrange
            _itemRepository.Setup(cr => cr.GetItems()).Returns(GetItemsResponse());
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.GetItems();

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().NotBeNull();
            response.Message.Should().BeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void GetItems_GetItemsRepositoryMethodThrowsException_ResponseShouldNotBeNullDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeFalse()
        {
            //Arrange
            _itemRepository.Setup(cr => cr.GetItems()).Throws(new Exception("Repository method GetItems throws exception"));
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.GetItems();

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeFalse();
        }
        #endregion
        #region GetItemById tests
        [Fact]
        public void GetItemById_GetItemByIdRepositoryMethodReturnsItem_ResponseShouldNotBeNullDataShouldNotBeNullMessageShouldBeNullSuccessShouldBeTrue()
        {
            //Arrange
            int id = 1;
            _itemRepository.Setup(cr => cr.GetItemById(It.IsAny<int>())).Returns(GetItemByIdResponse());
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.GetItemById(id);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().NotBeNull();
            response.Message.Should().BeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void GetItemById_GetItemByIdRepositoryMethodReturnsNull_ResponseShouldNotBeNullDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeTrue()
        {
            //Arrange
            int id = 1;
            _itemRepository.Setup(cr => cr.GetItemById(It.IsAny<int>())).Returns(GetItemByIdResponse(false));
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.GetItemById(id);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void GetItemById_GetItemByIdRepositoryMethodThrowsException_ResponseShouldNotBeNullDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeFalse()
        {
            //Arrange
            int id = 1;
            _itemRepository.Setup(cr => cr.GetItemById(It.IsAny<int>())).Throws(new Exception("Repository method GetItemById throws exception"));
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.GetItemById(id);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeFalse();
        }
        #endregion
        #region CreateItem tests
        [Fact]
        public void CreateItem_CreateItemRepositoryReturnsNonZero_ResponseShouldNotBeNullDataShouldNotBeNullMessageShouldBeNullSuccessShouldBeTrue()
        {
            //Arrange
            var itemCreate = CreateItemRequest();
            _itemRepository.Setup(cr => cr.CreateItem(It.IsAny<Item>())).Returns(1);
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.CreateItem(itemCreate);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().NotBeNull();
            response.Message.Should().BeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void CreateItem_CreateItemRepositoryReturnsZero_ResponseShouldNotBeNullDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeTrue()
        {
            //Arrange
            var itemCreate = CreateItemRequest();
            _itemRepository.Setup(cr => cr.CreateItem(It.IsAny<Item>())).Returns(0);
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.CreateItem(itemCreate);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void CreateItem_CreateItemRepositoryThrowsException_ResponseShouldNotBeNullDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeFalse()
        {
            //Arrange
            var itemCreate = CreateItemRequest();
            _itemRepository.Setup(cr => cr.CreateItem(It.IsAny<Item>())).Throws(new Exception("Repository method CreateItem throws exception"));
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.CreateItem(itemCreate);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeFalse();
        }
        #endregion
        #region AddItemToCharacter tests
        [Fact]
        public void AddItemToCharacter_AddItemToCharacterRepositoryReturnsNonZero_ResponseShouldNotBeNullDataShouldNotBeNullMessageShouldBeNullSuccessShouldBeTrue()
        {
            //Arrange
            var addItemToCharacter = CreateAddItemToCharacterRequest();
            _itemRepository.Setup(cr => cr.AddItemToCharacter(It.IsAny<CharacterItem>())).Returns(1);
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.AddItemToCharacter(addItemToCharacter);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().NotBeNull();
            response.Message.Should().BeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void AddItemToCharacter_AddItemToCharacterRepositoryReturnsZero_ResponseShouldNotBeNullDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeTrue()
        {
            //Arrange
            var addItemToCharacter = CreateAddItemToCharacterRequest();
            _itemRepository.Setup(cr => cr.AddItemToCharacter(It.IsAny<CharacterItem>())).Returns(0);
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.AddItemToCharacter(addItemToCharacter);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void AddItemToCharacter_AddItemToCharacterRepositoryThrowsException_ResponseShouldNotBeNullDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeFalse()
        {
            //Arrange
            var addItemToCharacter = CreateAddItemToCharacterRequest();
            _itemRepository.Setup(cr => cr.AddItemToCharacter(It.IsAny<CharacterItem>())).Throws(new Exception("Repository method AddItemToCharacter throws exception"));
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.AddItemToCharacter(addItemToCharacter);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeFalse();
        }
        #endregion
        #region GiftItem tests
        [Fact]
        public void GiftItem_GiftItemRepositoryReturnsNonZero_ResponseShouldNotBeNullDataShouldNotBeNullMessageShouldBeNullSuccessShouldBeTrue()
        {
            //Arrange
            var giftItemRequest = CreateGiftItemRequest();
            _itemRepository.Setup(cr => cr.GiftItem(It.IsAny<GiftItemRequest>())).Returns(1);
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.GiftItem(giftItemRequest);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().NotBeNull();
            response.Message.Should().BeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void GiftItem_GiftItemRepositoryReturnsZero_ResponseShouldNotBeNullDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeTrue()
        {
            //Arrange
            var giftItemRequest = CreateGiftItemRequest();
            _itemRepository.Setup(cr => cr.GiftItem(It.IsAny<GiftItemRequest>())).Returns(0);
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.GiftItem(giftItemRequest);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeTrue();
        }

        [Fact]
        public void GiftItem_GiftItemRepositoryThrowsException_ResponseShouldNotBeNullDataShouldBeNullMessageShouldNotBeNullSuccessShouldBeFalse()
        {
            //Arrange
            var giftItemRequest = CreateGiftItemRequest();
            _itemRepository.Setup(cr => cr.GiftItem(It.IsAny<GiftItemRequest>())).Throws(new Exception("Repository method GiftItem throws exception"));
            _itemBusinessLogic = new ItemBusinessLogic(_mapper, _itemRepository.Object);

            //Act
            var response = _itemBusinessLogic.GiftItem(giftItemRequest);

            //Assert
            response.Should().NotBeNull();
            response.Data.Should().BeNull();
            response.Message.Should().NotBeNull();
            response.Success.Should().BeFalse();
        }
        #endregion
        #region Private methods
        private GiftItemRequest CreateGiftItemRequest()
        {
            return new GiftItemRequest() { CharacterFromId = 1, CharacterToId = 2, GiftItemId = 1 };
        }

        private GrantItem CreateAddItemToCharacterRequest()
        {
            return new GrantItem() { CharacterId = 1, ItemId = 1 };
        }

        private ItemPost CreateItemRequest()
        {
            return new ItemPost()
            {
                Name = "Strength boost",
                Description = "Item for additional strength",
                BonusStrength = 5,
                BonusAgility = 0,
                BonusFaith = 0,
                BonusIntelligence = 0
            };
        }
        private List<Item> GetItemsResponse()
        {
            return new List<Item>()
            {
                new Item()
                {
                Id = 1,
                Name = "Strength boost",
                Description = "Item for additional strength",
                BonusStrength = 5,
                BonusAgility = 0,
                BonusFaith = 0,
                BonusIntelligence = 0
                }
            };
        }

        private Item GetItemByIdResponse(bool success = true)
        {
            if(success)
            {
                return new Item()
                {
                    Id = 1,
                    Name = "Strength boost",
                    Description = "Item for additional strength",
                    BonusStrength = 5,
                    BonusAgility = 0,
                    BonusFaith = 0,
                    BonusIntelligence = 0
                };
            }
            else 
            {
                return null;
            }
        }
        #endregion
    }
}
