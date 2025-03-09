using CharacterApi.BusinessLogic.Models;
using CharacterApi.Models;

namespace CharacterApi.Repository
{
    public interface IItemRepository
    {
        List<Item> GetItems();

        int CreateItem(Item item);

        Item GetItemById(int id);

        int AddItemToCharacter(CharacterItem characterItem);

        int GiftItem(GiftItemRequest giftItemRequest);
    }
}
