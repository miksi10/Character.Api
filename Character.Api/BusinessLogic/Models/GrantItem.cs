using System.ComponentModel.DataAnnotations;

namespace CharacterApi.BusinessLogic.Models
{
    public class GrantItem
    {
        [Required]
        public int CharacterId { get; set; }

        [Required]
        public int ItemId { get; set; }
    }
}
