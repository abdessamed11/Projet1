namespace Projet1.Models
{
    public class Formation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }

        public string ProfilePicture { get; set; }
    }
}
