using System.ComponentModel.DataAnnotations;

namespace CharacterApi.BusinessLogic.Models
{
    public class GiftItemRequest
    {
        [Required]
        public int CharacterFromId { get; set; }

        [Required]
        public int CharacterToId { get; set; }

        [Required]
        public int GiftItemId { get; set; }
    }
}
