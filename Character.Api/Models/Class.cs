using System.ComponentModel.DataAnnotations;

namespace CharacterApi.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}
