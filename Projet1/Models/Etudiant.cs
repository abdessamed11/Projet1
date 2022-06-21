namespace Projet1.Models
{
    public class Etudiant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }


    }
}
