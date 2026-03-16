using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OrganizationMVC.Models;

namespace OrganizationMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private OrganizationDbContext db = new OrganizationDbContext();


        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        // GET: Departments/Search?searchTerm=abc
        public PartialViewResult Search(string searchTerm)
        {
            var departments = db.Departments.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                departments = departments.Where(d => d.DepartmentName.Contains(searchTerm));
            }

            return PartialView("_DepartmentTable", departments.ToList());
        }

        // POST: Departments/DeleteAjax/5
        [HttpPost]
        public JsonResult DeleteAjax(int id)
        {
            try
            {
                Department department = db.Departments.Find(id);
                if (department == null) return Json(new { success = false, message = "Department not found" });

                db.Departments.Remove(department);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Department department = db.Departments.Find(id);
            if (department == null) return HttpNotFound();
            return View(department);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Department department = db.Departments.Find(id);
            if (department == null) return HttpNotFound();
            return View(department);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Department department = db.Departments.Find(id);
            if (department == null) return HttpNotFound();
            return View(department);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
