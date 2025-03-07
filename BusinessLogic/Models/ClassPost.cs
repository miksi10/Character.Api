using System.ComponentModel.DataAnnotations;

namespace CharacterApi.BusinessLogic.Models
{
    public class ClassPost
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
