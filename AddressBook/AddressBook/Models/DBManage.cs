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

        public IQueryable<Contacts> GetContact(int id)
        {
            return _db.Contacts.Where(i => i.ContactsId == id);
        }

        public IQueryable<Addresses> GetAddress(int id)
        {
            var addressId = GetContact(id).Select(i => i.AddressId);
            return _db.Addresses.Where(i => i.AddressId == addressId.FirstOrDefault());
        }

        public IQueryable<Types> GetType(int id)
        {
            var typeId = GetContact(id).Select(i => i.TypeId);
            return _db.Types.Where(i => i.TypeId == typeId.FirstOrDefault());
        }
    }
}