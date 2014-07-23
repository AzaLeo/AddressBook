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
            ViewBag.Address = _db.GetAddress(id);
            ViewBag.Type = _db.GetType(id);
            return View(_db.GetContact(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Contacts newContact)
        {
            return View();
        }
    }
}
