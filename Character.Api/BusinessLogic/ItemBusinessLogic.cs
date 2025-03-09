using AutoMapper;
using BusinessLogic.Models;
using CharacterApi.BusinessLogic.Models;
using CharacterApi.DbContext;
using CharacterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CharacterApi.BusinessLogic
{
    /// <summary>
    /// ItemBusinessLogic for item manipulation
    /// </summary>
    public class ItemBusinessLogic : IItemBusinessLogic
    {

        #region Private fields
        private readonly CharacterDbContext _characterDbContext;
        private readonly IMapper _mapper;
        #endregion

        #region Public constructors
        public ItemBusinessLogic(CharacterDbContext characterDbContext, IMapper mapper)
        {
            _characterDbContext = characterDbContext;
            _mapper = mapper;
        }

        #endregion

        #region Public methods
        public CommandResponse<List<ItemGet>> GetItems()
        {
            var itemsResponse = new CommandResponse<List<ItemGet>>();

            try
            {
                var items = _characterDbContext.Items.ToList();
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

                _characterDbContext.Items.Add(item);

                _characterDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
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
                var item = _characterDbContext.Items.Find(id);

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

                _characterDbContext.CharacterItem.Add(characterItem);

                _characterDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                grantItemResponse.Success = false;
                grantItemResponse.Message = new CommandMessage()
                {
                    Text = "Failed to create item",
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
                var r = _characterDbContext.CharacterItem
                    .Where(ci => (ci.ItemId == giftItem.GiftItemId && ci.CharacterId == giftItem.CharacterFromId))
                    .ExecuteUpdate(ci => ci.SetProperty(c => c.CharacterId, c => giftItem.CharacterToId));
            }
            catch (Exception e)
            {
                giftItemResponse.Success = false;
                giftItemResponse.Message = new CommandMessage()
                {
                    Text = String.Format("Failed to gift item with id {0} from character {1} to character with id {2}",
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
