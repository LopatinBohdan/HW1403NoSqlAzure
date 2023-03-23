using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using HW1403NoSql.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HW1403NoSql.Controllers
{
    public class GoodController : Controller
    {
        MyContext db;
        List<Good> goods;
        QueueServiceClient serviceClient;
        QueueClient queue;
        public static string connStr= "DefaultEndpointsProtocol=https;AccountName=lopatinqueue;AccountKey=to1FWzNyEc7oJWtOD+4dhRNtBbAlMC0rAH6l/zmk/GowelTadS0AtaUBlM1pwPdES/NJO1hhrNNX+ASt/Kw3wg==;EndpointSuffix=core.windows.net";
        public static string queueName= "lopatinqueue";
        string path = "log.txt";

        public GoodController(MyContext db)
        {
            this.db = db;
            //CreateQueue();
        }

        public void CreateQueue(string connStr, string queueName)
        {
            serviceClient = new QueueServiceClient(connStr);
            queue = serviceClient.CreateQueue(queueName);
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
        //Buy Good
        [HttpPost]
        public async Task<IActionResult> Buy(int id)
        {
            if (id != null){
                Good good = await db.Goods.FindAsync(id);
                if (good != null)
                {
                    serviceClient = new QueueServiceClient(connStr);
                    try
                    {
                        queue = await serviceClient.CreateQueueAsync(queueName);
                    }
                    catch (Exception)
                    {
                        queue = serviceClient.GetQueueClient(queueName);
                    }
                    
                    string mess = System.Text.Json.JsonSerializer.Serialize(good);
                    SendReceipt receipt = await queue.SendMessageAsync(mess);
                    using (StreamWriter sw=new StreamWriter(path, true))
                    {
                        sw.WriteLine(mess+ " was bought...");
                    }
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
