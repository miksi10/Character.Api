using System.ComponentModel.DataAnnotations;

namespace CharacterApi.Models
{
    public class CharacterItem
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }

        public int ItemId { get; set; }

        public Character Character { get; set; }

        public Item Item { get; set; }
    }
}
