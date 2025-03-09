using CharacterApi.BusinessLogic.Models;
using CharacterApi.DbContext;
using CharacterApi.Models;
using CharacterApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace CharacterApi.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly CharacterDbContext _characterDbContext;
        public ItemRepository(CharacterDbContext characterDbContext)
        {
            _characterDbContext = characterDbContext;
        }

        public int AddItemToCharacter(CharacterItem characterItem)
        {
            _characterDbContext.CharacterItem.Add(characterItem);

            return _characterDbContext.SaveChanges();
        }

        public int CreateItem(Item item)
        {
            _characterDbContext.Items.Add(item);

            return _characterDbContext.SaveChanges();
        }

        public Item GetItemById(int id)
        {
            return _characterDbContext.Items.Find(id);
        }

        public List<Item> GetItems()
        {
            return _characterDbContext.Items.ToList();
        }

        public int GiftItem(GiftItemRequest giftItemRequest)
        {
            return _characterDbContext.CharacterItem
                    .Where(ci => (ci.ItemId == giftItemRequest.GiftItemId && ci.CharacterId == giftItemRequest.CharacterFromId))
                    .ExecuteUpdate(ci => ci.SetProperty(c => c.CharacterId, c => giftItemRequest.CharacterToId));
        }
    }
}
