using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OrganizationMVC.Models;

namespace OrganizationMVC.Controllers
{
    public class UsersController : Controller
    {
        private OrganizationDbContext db = new OrganizationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Department);
            return View(users.ToList());
        }

        // GET: Users/Search?searchTerm=abc
        public PartialViewResult Search(string searchTerm)
        {
            var users = db.Users.Include(u => u.Department);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(u => u.UserName.Contains(searchTerm) || u.Email.Contains(searchTerm));
            }

            return PartialView("_UserTable", users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,Email,DepartmentId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", user.DepartmentId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Find(id);
            if (user == null) return HttpNotFound();
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", user.DepartmentId);
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Email,DepartmentId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", user.DepartmentId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Users/DeleteAjax/5
        [HttpPost]
        public JsonResult DeleteAjax(int id)
        {
            try
            {
                User user = db.Users.Find(id);
                if (user == null) return Json(new { success = false, message = "User not found" });

                db.Users.Remove(user);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Users/IsEmailAvailable?Email=abc@xyz.com
        [HttpGet]
        public JsonResult IsEmailAvailable(string Email, int? UserId)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == Email && (UserId == null || u.UserId != UserId));
            return Json(user == null, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
