using System.ComponentModel.DataAnnotations;

namespace Projet1.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string ProfilePicture { get; set; }
    }
}
