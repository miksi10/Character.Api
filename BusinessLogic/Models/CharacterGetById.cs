using BusinessLogic.Models;

namespace CharacterApi.BusinessLogic.Models
{
    public class CharacterGetById
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int BaseStrength { get; set; }
        public int BaseAgility { get; set; }
        public int BaseIntelligence { get; set; }
        public int BaseFaith { get; set; }
        public int ClassId { get; set; }
        public ICollection<ItemGetById> Items { get; set; }
        public string CreatedBy { get; set; }
    }
}
