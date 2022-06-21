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
    public class FormationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public IWebHostEnvironment _webHostEnvironment { get; set; }

        public FormationsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
        }

        // GET: Formations
        public async Task<IActionResult> Index()
        
        
        {
            var model = await _context.formations.Include(c => c.Categorie).ToListAsync();
            return View(model);
        }

        public void Del_file(int id)
        {
            var filename = _context.categories.Find(id).ProfilePicture;
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, FileLocation.FileUploadFolder);
            string filePath = Path.Combine(uploadsFolder, filename);
            System.IO.File.Delete(filePath);

        }


        public string ProcessUploadedFile(FormationViewModel model)
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

        // GET: Formations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formation = await _context.formations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formation == null)
            {
                return NotFound();
            }

            return View(formation);
        }

        
        public async Task<IActionResult> AddOrEdit(int id)
        {
            ViewBag.categ = _context.categories.ToList();
            if (id == 0)
            {
                return View();
            }
            else
            {
                var model = await _context.formations.FindAsync(id);
                var formation = new FormationViewModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    ExistingImage = model.ProfilePicture
                };
                if (model == null)
                {
                    return NotFound();
                }
                return View(formation);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(FormationViewModel model, int id)
        {
            if (id == 0)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Formation formation = new Formation
                {
                    Name = model.Name,
                    ProfilePicture = uniqueFileName,
                    CategorieId=model.CategorieId,
                };
                _context.Add(formation);
                await _context.SaveChangesAsync();
            }
            else
            {
                var picture = _context.formations.Find(id).ProfilePicture;
                if (model.Picture != null)
                {
                    if (picture != null)
                        Del_file(id);
                    picture = ProcessUploadedFile(model);
                }
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.formations.Include(c=>c.Categorie).ToList()) });
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Del_file(id);
            var formation = await _context.formations.FindAsync(id);
            _context.formations.Remove(formation);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.formations.ToList()) });
        }

        private bool FormationExists(int id)
        {
            return _context.formations.Any(e => e.Id == id);
        }
    }
}
