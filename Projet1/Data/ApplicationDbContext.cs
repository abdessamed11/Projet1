using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projet1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projet1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Branch> branches { get; set; }
        public DbSet<Categorie> categories { get; set; }

        public DbSet<Formation> formations { get; set; }
        public DbSet<Etudiant> etudiants { get; set; }

        public DbSet<Article> articles { get; set; }
        public DbSet<Test> test { get; set; }
    }
}
