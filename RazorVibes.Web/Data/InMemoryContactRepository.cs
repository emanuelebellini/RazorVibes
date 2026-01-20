using System;
using System.Collections.Generic;
using System.Linq;
using RazorVibes.Web.Models;

namespace RazorVibes.Web.Data
{
    public class InMemoryContactRepository
    {
        private static readonly List<Contact> Contacts = new List<Contact>
        {
            new Contact { Id = 1, Name = "Ariana Grande", Email = "ariana@example.com", Notes = "Pop vocalist", CreatedAt = DateTime.UtcNow.AddDays(-2) },
            new Contact { Id = 2, Name = "Bruno Mars", Email = "bruno@example.com", Notes = "Stage performance", CreatedAt = DateTime.UtcNow.AddDays(-1) },
            new Contact { Id = 3, Name = "SZA", Email = "sza@example.com", Notes = "R&B vibes", CreatedAt = DateTime.UtcNow }
        };

        private static int _nextId = 4;

        public IEnumerable<Contact> GetAll() => Contacts.OrderBy(c => c.Name).ToList();

        public Contact GetById(int id) => Contacts.FirstOrDefault(c => c.Id == id);

        public void Add(Contact contact)
        {
            contact.Id = _nextId++;
            contact.CreatedAt = DateTime.UtcNow;
            Contacts.Add(contact);
        }

        public void Update(Contact contact)
        {
            var existing = GetById(contact.Id);
            if (existing == null)
            {
                return;
            }

            existing.Name = contact.Name;
            existing.Email = contact.Email;
            existing.Notes = contact.Notes;
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                Contacts.Remove(existing);
            }
        }
    }
}
