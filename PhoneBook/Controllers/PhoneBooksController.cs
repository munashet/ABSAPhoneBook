using PhoneBook.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class PhoneBooksController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.PhoneBooks.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Search(string search)
        {
            return View("Index", await db.PhoneBooks.Where(i => i.Name.Contains(search)).ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.PhoneBook phoneBook = await db.PhoneBooks.FindAsync(id);
            if (phoneBook == null)
            {
                return HttpNotFound();
            }
            return View(phoneBook);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PhoneBookID,Name")] Models.PhoneBook phoneBook)
        {
            if (ModelState.IsValid)
            {
                db.PhoneBooks.Add(phoneBook);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(phoneBook);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.PhoneBook phoneBook = await db.PhoneBooks.FindAsync(id);
            if (phoneBook == null)
            {
                return HttpNotFound();
            }
            return View(phoneBook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PhoneBookID,Name")] Models.PhoneBook phoneBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phoneBook).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(phoneBook);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.PhoneBook phoneBook = await db.PhoneBooks.FindAsync(id);
            if (phoneBook == null)
            {
                return HttpNotFound();
            }
            return View(phoneBook);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Models.PhoneBook phoneBook = await db.PhoneBooks.FindAsync(id);
            db.PhoneBooks.Remove(phoneBook);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
