using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharacterApi.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int BaseStrength { get; set; }
        public int BaseAgility {  get; set; }
        public int BaseIntelligence { get; set; }
        public int BaseFaith { get; set; }
        public Class Class { get; set; }

        public int ClassId { get; set; }
        public ICollection<Item> Items { get; set; }

        [MaxLength(450)]
        public string CreatedBy { get; set; }

        public ICollection<CharacterItem> CharacterItems { get; set; }

    }
}
