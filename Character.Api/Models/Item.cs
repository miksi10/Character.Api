using System.ComponentModel.DataAnnotations;

namespace CharacterApi.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        public int BonusStrength { get; set; }
        public int BonusAgility { get; set; }
        public int BonusIntelligence { get; set; }
        public int BonusFaith { get; set; }
        public ICollection<Character> Characters { get; set; }
        public ICollection<CharacterItem> CharacterItems { get; set; }
    }
}
