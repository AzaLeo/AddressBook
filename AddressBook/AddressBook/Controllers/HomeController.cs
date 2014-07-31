using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AddressBook.Models;
using WebMatrix.WebData;

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

        public ActionResult ContactList(string sort)
        {
            var orderedList = _db.GetAllContacts();
            switch (sort)
            {
                case "firstname":
                    orderedList = orderedList.OrderBy(f => f.FirstName);
                    break;
                case "lastname":
                    orderedList = orderedList.OrderBy(l => l.LastName);
                    break;
                case "phone":
                    orderedList = orderedList.OrderBy(p => p.Phone);
                    break;
                case "email":
                    orderedList = orderedList.OrderBy(e => e.Email);
                    break;
            }
            return PartialView(orderedList);
        }

        public ActionResult Details(int id)
        {
            return PartialView(_db.GetContact(id));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TypeList = GetDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Contacts newContact)
        {
            newContact.UserId = WebSecurity.CurrentUserId;
            TempData["ChangeDBInfo"] = _db.AddContact(newContact);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.TypeList = GetDropDownList();
            return View(_db.GetContact(id));
        }

        [HttpPost]
        public ActionResult Edit(Contacts editContact)
        {
            TempData["ChangeDBInfo"] = _db.EditContact(editContact);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            TempData["ChangeDBInfo"] = _db.DeleteContact(_db.GetContact(id));
            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> GetDropDownList()
        {
            return from c in _db.GetTypeList()
                   select new SelectListItem
                     {
                         Value = c.TypeId.ToString(),
                         Text = c.Name
                     };
        }
    }
}
