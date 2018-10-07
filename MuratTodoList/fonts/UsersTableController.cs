using MuratTodoList.Context;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MuratTodoList.Controllers
{
    public class UsersTableController : Controller
    {
        private TodoListContext dbContext = new TodoListContext();
        public ActionResult Logout()
        {
            Session["UserName"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users Model)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserName == Model.UserName && x.Password == Model.Password);
            if (user != null)
            {
                Session["UserName"] = user;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        // GET: UsersTable
        public ActionResult Index()
        {
            return View(dbContext.Users.ToList());
        }

        // GET: UsersTable/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users usersTable = dbContext.Users.Find(id);
            if (usersTable == null)
            {
                return HttpNotFound();
            }
            return View(usersTable);
        }

        // GET: UsersTable/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersTable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,Email,Date")] Users usersTable)
        {
            if (ModelState.IsValid)
            {
                dbContext.Users.Add(usersTable);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usersTable);
        }

        // GET: UsersTable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usersTable = dbContext.Users.Find(id);
            if (usersTable == null)
            {
                return HttpNotFound();
            }
            return View(usersTable);
        }

        // POST: UsersTable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,Email,Date")] Users usersTable)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(usersTable).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usersTable);
        }

        // GET: UsersTable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usersTable = dbContext.Users.Find(id);
            if (usersTable == null)
            {
                return HttpNotFound();
            }
            return View(usersTable);
        }

        // POST: UsersTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usersTable = dbContext.Users.Find(id);
            dbContext.Users.Remove(usersTable);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
