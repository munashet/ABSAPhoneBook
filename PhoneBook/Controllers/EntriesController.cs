using PhoneBook.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class EntriesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public async Task<ActionResult> Index(int id)
        {
            Session["PhoneBookID"] = id;
            return View(await db.Entries.Where(i => i.PhoneBookID == id).ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Search(string search)
        {
            return View("Index", await db.Entries.Where(i => i.Name.Contains(search)).ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entry entry = await db.Entries.FindAsync(id);
            if (entry == null)
            {
                return HttpNotFound();
            }
            return View(entry);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EntryID,PhoneBookID,Name,PhoneNumber")] Entry entry)
        {
            if (ModelState.IsValid)
            {
                int phoneBookID = (int)Session["PhoneBookID"];
                entry.PhoneBookID = phoneBookID;
                db.Entries.Add(entry);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = phoneBookID });
            }

            return View(entry);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entry entry = await db.Entries.FindAsync(id);
            if (entry == null)
            {
                return HttpNotFound();
            }
            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EntryID,PhoneBookID,Name,PhoneNumber")] Entry entry)
        {
            if (ModelState.IsValid)
            {
                int phoneBookID = (int)Session["PhoneBookID"];
                entry.PhoneBookID = phoneBookID;
                db.Entry(entry).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = entry.PhoneBookID });
            }
            return View(entry);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entry entry = await db.Entries.FindAsync(id);
            if (entry == null)
            {
                return HttpNotFound();
            }
            return View(entry);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Entry entry = await db.Entries.FindAsync(id);
            db.Entries.Remove(entry);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = entry.PhoneBookID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
