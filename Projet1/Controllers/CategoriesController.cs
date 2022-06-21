using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projet1.Data;
using Projet1.Models;
using Projet1.ViwModels;

namespace Projet1.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IWebHostEnvironment _webHostEnvironment { get; set; }

        public CategoriesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.categories.ToListAsync());
        }

        public void Del_file(int id)
        {
            var filename = _context.categories.Find(id).ProfilePicture;
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, FileLocation.FileUploadFolder);
            string filePath = Path.Combine(uploadsFolder, filename);
            System.IO.File.Delete(filePath);

        }
       

        public string ProcessUploadedFile(CategoriesViewModel model)
        {
            string uniqueFileName = null;

            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, FileLocation.FileUploadFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

       
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                var categorie = _context.categories.Find(id);

                var categories = new CategoriesViewModel()
                {
                    Id = categorie.Id,
                    Name = categorie.Name,
                    ExistingImage = categorie.ProfilePicture


                };
                if (categorie == null) return NotFound();

                return View(categories);
            }

        }

        
        public IActionResult Details(CategoriesViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, CategoriesViewModel model)
        {

            if (id == 0)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Categorie categorie = new Categorie
                {
                    Name = model.Name,
                    ProfilePicture = uniqueFileName
                };
                _context.Add(categorie);
                await _context.SaveChangesAsync();
            }
            else
            {

                var etudiant = await _context.categories.FindAsync(model.Id);
                etudiant.Id = model.Id;
                etudiant.Name = model.Name;

                if (model.Picture != null)
                {
                    if (etudiant.ProfilePicture != null)
                        Del_file(id);
                   etudiant.ProfilePicture = ProcessUploadedFile(model);
                }


                _context.Update(etudiant);
                await _context.SaveChangesAsync();
            }

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.categories.ToList()) });

        }

 

        // POST: Transactions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,Categorie model)
        {
            Del_file(id);
            var transactionModel = await _context.categories.FindAsync(id);
            _context.categories.Remove(transactionModel);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.categories.ToList()) });
        }

        private bool TransactionModelExists(int id)
        {
            return _context.categories.Any(e => e.Id == id);
        }


    }
}
