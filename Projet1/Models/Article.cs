using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet1.Models
{
    [Table("tb_articles")]
    public class Article
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        [Display(Name = "Article")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string ProfilePicture { get; set; }

        [Column("formation_Id")]
        public int formationId { get; set; }
        public Formation formation { get; set; }

       
    }
}
