using AddressBook.Models;
using System.Collections.Generic;

namespace AddressBook.Repositories
{
    public interface IRepository
    {
        string AddContact(Contacts newContact);
        string EditContact(Contacts edit);
        string DeleteContact(Contacts delContat);
        Contacts GetContactById(int id);
        IEnumerable<Types> GetTypeList();
        IEnumerable<UserProfile> GetAllUsers();
        IEnumerable<Contacts> GetAllContacts();
        IEnumerable<Contacts> SearchByFirstLetterFirstName(string symbol);
        IEnumerable<Contacts> SearchByFirstLetterLastName(string symbol);
        IEnumerable<Contacts> SearchByFirstLetterPhone(string symbol);
        IEnumerable<Contacts> SearchByFirstLetterEmail(string symbol);
    }
}
