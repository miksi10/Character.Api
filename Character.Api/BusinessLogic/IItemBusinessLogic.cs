using BusinessLogic.Models;
using CharacterApi.BusinessLogic.Models;

namespace CharacterApi.BusinessLogic
{
    public interface IItemBusinessLogic
    {
        CommandResponse<List<ItemGet>> GetItems();

        CommandResponse<ItemPost> CreateItem(ItemPost item);

        CommandResponse<ItemGetById> GetItemById(int id);

        CommandResponse<GrantItem> AddItemToCharacter(GrantItem grantItem);

        CommandResponse<GiftItemRequest> GiftItem(GiftItemRequest giftItem);
    }
}
