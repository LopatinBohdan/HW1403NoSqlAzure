using HW1403NoSql.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HW1403NoSql.Controllers
{
    public class CategoryController : Controller
    {
        MyContext db;
        List<Category> categories;
        public CategoryController(MyContext db) { this.db=new MyContext(); }
        public async Task<IActionResult> Index()
        {
            categories = await db.Categories.ToListAsync();
            return View(categories);
        }

        //Create Category
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
        //Edit Categorie
        public async Task<IActionResult> Edit(int? id)
        {
            Category category = await db.Categories.FindAsync(id);
            if (category != null)
            {
                return View(category);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (category != null)
            {
                db.Update(category);
                await db.SaveChangesAsync();
                return View("Index", db.Categories.ToList());
            }
            return View(category);
        }
        //Delete Category
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Category category= await db.Categories.FindAsync(id); 
                if (category != null) 
                {
                    db.Categories.Remove(category);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

    }
}
