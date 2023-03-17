using HW1403NoSql.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace HW1403NoSql.Controllers
{
    public class GoodController : Controller
    {
        MyContext db;
        List<Good> goods;
        public GoodController(MyContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = db.Categories.ToList();
            goods = await db.Goods.ToListAsync();
            return View("Index",goods);
        }
        [HttpPost]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.Categories = db.Categories.ToList();
            goods = await db.Goods.Where(g => g.CategoryId == id).ToListAsync();
            return View("Index", goods);
        }

        //Search by title
        [HttpPost]
        public async Task<IActionResult> IndexSearch(string searchstring)
        {
            ViewBag.Categories = db.Categories.ToList();
            goods = await db.Goods.Where(g=>g.Title!.ToLower().Contains(searchstring.ToLower())).ToListAsync();
            return View("Index",goods);
        }

        //Create Good
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Title");
            return View(new Good());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Good good)
        {
            if (ModelState.IsValid)
            {
                db.Goods.Add(good);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        //Edit Good
        public async Task<IActionResult> Edit(int id)
        {
            Good good = await db.Goods.FindAsync(id);
            if (good!=null)
            {
                ViewBag.Categories = new SelectList(db.Categories, "Id", "Title");
                return View(good);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Good good)
        {
            if (good != null)
            {
                db.Update(good);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(good);
        }
        //Delete Good
        public async Task<IActionResult> Delete(int id)
        {
            if (id != null)
            {
                Good good = await db.Goods.FindAsync(id);
                if (good!=null)
                {
                    db.Goods.Remove(good);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }


    }
}
