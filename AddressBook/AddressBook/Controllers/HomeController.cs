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
            return View(_db.GetContact(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.TypeList = from c in _db.GetTypeList()
                               select new SelectListItem
                               {
                                   Value = c.Name,
                                   Text = c.Name
                               };
            return View();
        }

        [HttpPost]
        public ActionResult Create(Contacts newContact, IEnumerable<SelectListItem> test)
        {
            Contacts s = newContact;
            IEnumerable<SelectListItem> asdf = test;
            return View();
        }
    }
}
