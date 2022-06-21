using Projet1.Models;

namespace Projet1.ViwModels
{
    public class FormationViewModel : EditImageViewModel
    {
        public string Name { get; set; }
        public int CategorieId { get; set; }
        public Categorie categorie { get; set; }

    }
}
