using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CharacterApi.BusinessLogic.Models
{
    public class CharacterPost
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int BaseStrength { get; set; }
        public int BaseAgility { get; set; }
        public int BaseIntelligence { get; set; }
        public int BaseFaith { get; set; }
        public ClassPost Class { get; set; }
        [MaxLength(450)]
        [SwaggerIgnore]
        public string CreatedBy { get; set; } = "3b490698-a212-4530-8eba-42f719f49e73"; //to do: ovu informaciju izvuci iz beare tokena
    }
}
