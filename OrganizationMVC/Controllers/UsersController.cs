using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OrganizationMVC.Models;

namespace OrganizationMVC.Controllers
{
    public class UsersController : Controller
    {
        private OrganizationDbContext db = new OrganizationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Department).Include(u => u.UserDocuments);
            return View(users.ToList());
        }

        // GET: Users/Search?searchTerm=abc
        public PartialViewResult Search(string searchTerm)
        {
            var users = db.Users.Include(u => u.Department).Include(u => u.UserDocuments);

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
            User user = db.Users.Include(u => u.UserDocuments).FirstOrDefault(u => u.UserId == id);
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
        public ActionResult Create([Bind(Include = "UserId,UserName,Email,DepartmentId")] User user, IEnumerable<HttpPostedFileBase> documents)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges(); // Save user first to get UserId

                if (documents != null)
                {
                    foreach (var document in documents)
                    {
                        if (document != null && document.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(document.FileName);
                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                            string path = Path.Combine(Server.MapPath("~/Uploads/"), uniqueFileName);

                            if (!Directory.Exists(Server.MapPath("~/Uploads/")))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Uploads/"));
                            }

                            document.SaveAs(path);

                            var userDoc = new UserDocument
                            {
                                FileName = fileName,
                                FilePath = "/Uploads/" + uniqueFileName,
                                UserId = user.UserId
                            };
                            db.UserDocuments.Add(userDoc);
                        }
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", user.DepartmentId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Include(u => u.UserDocuments).FirstOrDefault(u => u.UserId == id);
            if (user == null) return HttpNotFound();
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", user.DepartmentId);
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Email,DepartmentId")] User user, IEnumerable<HttpPostedFileBase> documents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                
                if (documents != null)
                {
                    foreach (var document in documents)
                    {
                        if (document != null && document.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(document.FileName);
                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                            string path = Path.Combine(Server.MapPath("~/Uploads/"), uniqueFileName);

                            if (!Directory.Exists(Server.MapPath("~/Uploads/")))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Uploads/"));
                            }

                            document.SaveAs(path);

                            var userDoc = new UserDocument
                            {
                                FileName = fileName,
                                FilePath = "/Uploads/" + uniqueFileName,
                                UserId = user.UserId
                            };
                            db.UserDocuments.Add(userDoc);
                        }
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", user.DepartmentId);
            return View(user);
        }

        // GET: Users/DownloadDocument/5
        public ActionResult DownloadDocument(int id)
        {
            var document = db.UserDocuments.Find(id);
            if (document == null) return HttpNotFound();

            string fullPath = Server.MapPath("~" + document.FilePath);
            if (!System.IO.File.Exists(fullPath)) return HttpNotFound();

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, document.FileName);
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

        // POST: Users/DeleteDocument/5
        [HttpPost]
        public JsonResult DeleteDocument(int id)
        {
            try
            {
                var document = db.UserDocuments.Find(id);
                if (document == null) return Json(new { success = false, message = "Document not found" });

                string fullPath = Server.MapPath("~" + document.FilePath);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                db.UserDocuments.Remove(document);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
