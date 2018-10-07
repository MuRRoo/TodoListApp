using MuratTodoList.Context;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MuratTodoList.Controllers
{
    public class TasksController : Controller
    {
        private readonly TodoListContext dbContext = new TodoListContext();

        public ActionResult Index(string search)
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Users");

            return View(dbContext.Tasks.Where(x => x.Name.Contains(search) || search == null).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks task = dbContext.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tasks task)
        {
            if (ModelState.IsValid)
            {
                var user = Session["user"] as Users;
                task.UserId = user.Id;
                var data = dbContext.Tasks.Add(task);
                dbContext.SaveChanges();
                return Json(data.Id);
            }

            return View(task);
        }


        [HttpPost]
        public ActionResult Edit(int id)
        {
            var task = dbContext.Tasks.Find(id);
            if (task == null)
                return HttpNotFound();

            task.IsChecked = !task.IsChecked;

            dbContext.Entry(task).State = EntityState.Modified;
            dbContext.SaveChanges();

            return Json(task);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var task = dbContext.Tasks.Find(id);
            dbContext.Tasks.Remove(task);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                dbContext.Dispose();

            base.Dispose(disposing);
        }
    }
}
