using AutoMapper;
using BusinessLogic.Models;
using CharacterApi.BusinessLogic.Models;
using CharacterApi.Models;
using CharacterApi.Repository;

namespace CharacterApi.BusinessLogic
{
    /// <summary>
    /// ItemBusinessLogic for item manipulation
    /// </summary>
    public class ItemBusinessLogic : IItemBusinessLogic
    {

        #region Private fields
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Public constructors
        public ItemBusinessLogic(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        #endregion

        #region Public methods
        public CommandResponse<List<ItemGet>> GetItems()
        {
            var itemsResponse = new CommandResponse<List<ItemGet>>();

            try
            {
                var items = _itemRepository.GetItems();
                itemsResponse.Data = _mapper.Map<List<ItemGet>>(items);
            }
            catch (Exception ex)
            {
                itemsResponse.Success = false;
                itemsResponse.Message = new CommandMessage()
                {
                    Text = "Failed to show all items",
                    Type = MessageType.Error.ToString()
                };
            }
            return itemsResponse;
        }


        public CommandResponse<ItemPost> CreateItem(ItemPost itemPost)
        {
            var createItemResponse = new CommandResponse<ItemPost>() { Data = itemPost};

            try
            {
                var item = _mapper.Map<Item>(itemPost);

                int rows = _itemRepository.CreateItem(item);

                if(rows == 0)
                {
                    createItemResponse.Data = null;
                    createItemResponse.Message = new CommandMessage()
                    {
                        Text = "Item is not created",
                        Type = MessageType.Information.ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                createItemResponse.Data = null;
                createItemResponse.Success = false;
                createItemResponse.Message = new CommandMessage()
                {
                    Text = "Failed to create item",
                    Type = MessageType.Error.ToString()
                };
            }
            return createItemResponse;
        }

        public CommandResponse<ItemGetById> GetItemById(int id)
        {
            var itemByIdResponse = new CommandResponse<ItemGetById>();
            try
            {
                var item = _itemRepository.GetItemById(id);

                if (item == null)
                {
                    itemByIdResponse.Message = new CommandMessage()
                    {
                        Text = "There is no item with id: " + id,
                        Type = MessageType.Information.ToString()
                    };

                    return itemByIdResponse;
                }

               var itemById = _mapper.Map<ItemGetById>(item);

               itemByIdResponse.Data = AddItemNameSuffix(itemById);
            }
            catch (Exception ex)
            {
                itemByIdResponse.Success = false;
                itemByIdResponse.Message = new CommandMessage()
                {
                    Text = "Failed to find item with id: " + id,
                    Type = MessageType.Error.ToString()
                };
            }
            return itemByIdResponse;
        }

        public CommandResponse<GrantItem> AddItemToCharacter(GrantItem grantItem)
        {
            var grantItemResponse = new CommandResponse<GrantItem>() { Data = grantItem};

            try
            {
                var characterItem = _mapper.Map<CharacterItem>(grantItem);

                int rows = _itemRepository.AddItemToCharacter(characterItem);

                if(rows == 0)
                {
                    grantItemResponse.Data = null;
                    grantItemResponse.Message = new CommandMessage()
                    {
                        Text = "Item is not added to character",
                        Type = MessageType.Information.ToString()
                    };
                }
            }
            catch (Exception e)
            {
                grantItemResponse.Data = null;
                grantItemResponse.Success = false;
                grantItemResponse.Message = new CommandMessage()
                {
                    Text = "An error occured whilde adding item to character",
                    Type = MessageType.Error.ToString()
                };
            }
            return grantItemResponse;
        }

        public CommandResponse<GiftItemRequest> GiftItem(GiftItemRequest giftItem)
        {
            var giftItemResponse = new CommandResponse<GiftItemRequest>() { Data = giftItem};

            try
            {
                int rows = _itemRepository.GiftItem(giftItem);

                if(rows == 0)
                {
                    giftItemResponse.Data = null;
                    giftItemResponse.Message = new CommandMessage()
                    {
                        Text = "Item is not gifted to character",
                        Type = MessageType.Information.ToString()
                    };
                }
            }
            catch (Exception e)
            {
                giftItemResponse.Data = null;
                giftItemResponse.Success = false;
                giftItemResponse.Message = new CommandMessage()
                {
                    Text = String.Format("An error occured while gifting item with id {0} from character {1} to character with id {2}",
                    giftItem.GiftItemId, giftItem.CharacterFromId, giftItem.CharacterToId),
                    Type = MessageType.Error.ToString()
                };
            }
            return giftItemResponse;
        }
        #endregion

        #region Private methods
        private ItemGetById AddItemNameSuffix(ItemGetById itemGetById)
        {
            var stats = new Dictionary<string, int>()
            {
                { " Of The Bear", itemGetById.BonusStrength },
                { " Of The Cobra", itemGetById.BonusAgility},
                { " Of The Owl", itemGetById.BonusIntelligence },
                { " Of The Unicorn", itemGetById.BonusFaith }
            };

            int maxValue = stats.Values.Max();

            itemGetById.Name = itemGetById.Name + stats.FirstOrDefault(x => x.Value == maxValue).Key;

            return itemGetById;
        }

        #endregion
    }

}
