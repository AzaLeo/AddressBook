using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class HomeController : Controller
    {
        private DBManage _db;

        public HomeController()
        {
            _db = new DBManage();
        }

        public ActionResult Index()
        {
            return View(_db.GetAllContacts());
        }

        public ActionResult Details(int id)
        {
            IQueryable<Contacts> contact = _db.GetContact(id);
            ViewBag.TitleLastName = contact.Select(ln => ln.LastName).Single();
            ViewBag.TitleFirstName += contact.Select(fn => fn.FirstName).Single();
            ViewBag.Address = _db.GetAddress(id);
            ViewBag.Type = _db.GetType(id);
            return View(contact);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
