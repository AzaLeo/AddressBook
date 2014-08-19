using AddressBook.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook.Models
{
    public class AddressBookRepository : IRepository
    {
        private AddressBookEntities _abe;
        private UsersContext _uc;

        public AddressBookRepository()
        {
            _abe = new AddressBookEntities();
            _uc = new UsersContext();
        }

        public IEnumerable<Contacts> GetAllContacts()
        {
            return _abe.Contacts;
        }

        public Contacts GetContactById(int id)
        {
            return _abe.Contacts.SingleOrDefault(i => i.ContactsId == id);
        }

        public IEnumerable<Types> GetTypeList()
        {
            return _abe.Types;
        }

        public string AddContact(Contacts newContact)
        {
            _abe.Contacts.Add(newContact);

            try
            {
                _abe.SaveChanges();
            }
            catch (Exception e)
            {
                return Resources.Models.AddContactFailed + " " + e.Message;
            }

            return Resources.Models.AddContactSuccess;
        }

        public string EditContact(Contacts edit)
        {
            Contacts original = GetContactById(edit.ContactsId);
            original.FirstName = edit.FirstName;
            original.MiddleName = edit.MiddleName;
            original.LastName = edit.LastName;
            original.Phone = edit.Phone;
            original.Email = edit.Email;
            original.TypeId = edit.TypeId;
            UpdateAddress(original.Addresses, edit.Addresses);

            try
            {
                _abe.SaveChanges();
            }
            catch (Exception e)
            {
                return Resources.Models.EditContactFailed + " " + e.Message;
            }

            return Resources.Models.EditContactSuccess;
        }

        private void UpdateAddress(Addresses original, Addresses edit)
        {
            original.Country = edit.Country;
            original.City = edit.City;
            original.Street = edit.Street;
            original.House = edit.House;
            original.Room = edit.Room;
        }

        public string DeleteContact(Contacts delContat)
        {
            _abe.Addresses.Remove(delContat.Addresses);
            _abe.Contacts.Remove(delContat);

            try
            {
                _abe.SaveChanges();
            }
            catch (Exception e)
            {
                return Resources.Models.DeleteContactFailed + " " + e.Message;
            }

            return Resources.Models.DeleteContactSuccess;
        }

        public IEnumerable<UserProfile> GetAllUsers()
        {
            return _uc.UserProfiles;
        }

        public IEnumerable<Contacts> SearchByFirstLetterFirstName(string symbol)
        {
            return _abe.Contacts.Where(s => s.FirstName.Substring(0, 1) == symbol);
        }

        public IEnumerable<Contacts> SearchByFirstLetterLastName(string symbol)
        {
            return _abe.Contacts.Where(s => s.LastName.Substring(0, 1) == symbol);
        }

        public IEnumerable<Contacts> SearchByFirstLetterPhone(string symbol)
        {
            return _abe.Contacts.Where(s => s.Phone.Substring(0, 1) == symbol);
        }

        public IEnumerable<Contacts> SearchByFirstLetterEmail(string symbol)
        {
            return _abe.Contacts.Where(s => s.Email.Substring(0, 1) == symbol);
        }
    }
}