using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AddressBook.Models;
using WebMatrix.WebData;
using System.Web.Security;

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

        public ActionResult SortingList(string sort)
        {
            IEnumerable<Contacts> modelForSort = (IEnumerable<Contacts>)Session["ModelForSort"];

            switch (sort)
            {
                case "firstname":
                    modelForSort = modelForSort.OrderBy(f => f.FirstName);
                    break;
                case "lastname":
                    modelForSort = modelForSort.OrderBy(l => l.LastName);
                    break;
                case "phone":
                    modelForSort = modelForSort.OrderBy(p => p.Phone);
                    break;
                case "email":
                    modelForSort = modelForSort.OrderBy(e => e.Email);
                    break;
            }
            return PartialView("ContactList", modelForSort);
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
        [Authorize]
        public ActionResult Create(Contacts newContact)
        {
            newContact.UserId = WebSecurity.CurrentUserId;
            TempData["ChangeDBInfo"] = _db.AddContact(newContact);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (WebSecurity.CurrentUserId != _db.GetContact(id).UserId && !User.IsInRole("Admin"))
            {
                TempData["ChangeDBInfo"] = Resources.Controllers.СanEditOnlyTheirContact;
                return RedirectToAction("Index");
            }
            ViewBag.TypeList = GetDropDownList();
            return View(_db.GetContact(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(Contacts editContact)
        {
            TempData["ChangeDBInfo"] = _db.EditContact(editContact);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            if (WebSecurity.CurrentUserId != _db.GetContact(id).UserId && !User.IsInRole("Admin"))
            {
                TempData["ChangeDBInfo"] = Resources.Controllers.CanDeleteOnlyTheirContact;
                return RedirectToAction("Index");
            }
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
