using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public class DBManage
    {
        private AddressBookEntities _db;

        public DBManage()
        { 
            _db = new AddressBookEntities();
        }

        public IEnumerable<Contacts> GetAllContacts()
        {
            return _db.Contacts;
        }

        public Contacts GetContact(int id)
        {
            return _db.Contacts.SingleOrDefault(i => i.ContactsId == id);
        }

        public IQueryable<Addresses> GetAddress(int id)
        {
            int addressId = GetContact(id).AddressId;
            return _db.Addresses.Where(i => i.AddressId == addressId);
        }

        public IQueryable<Types> GetType(int id)
        {
            int typeId = GetContact(id).TypeId;
            return _db.Types.Where(i => i.TypeId == typeId);
        }

        public void AddContact(Contacts newContact)
        {
            _db.Contacts.Add(newContact);
            _db.SaveChanges();
        }
    }
}