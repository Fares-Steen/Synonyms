using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class AddSynonymDto
    {
        [Required]
        [MaxLength(100)]
        public string Word { get; set; } = "";

        [Required]
        [MaxLength(100)]
        public string Synonym { get; set; } = "";
    }
}
