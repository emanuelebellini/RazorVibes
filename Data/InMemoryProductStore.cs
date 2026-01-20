using System;
using System.Collections.Generic;
using System.Linq;
using RazorVibes.Models;

namespace RazorVibes.Data
{
    public class InMemoryProductStore
    {
        private static readonly List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Razor Guitar", Description = "A classic electric guitar.", Price = 799.99m, Stock = 5 },
            new Product { Id = 2, Name = "Vibe Amp", Description = "Tube amplifier with warm tone.", Price = 1199.00m, Stock = 2 },
            new Product { Id = 3, Name = "Studio Mic", Description = "Condenser microphone for vocals.", Price = 299.50m, Stock = 10 }
        };

        private static int _nextId = Products.Max(p => p.Id) + 1;

        public IEnumerable<Product> GetAll()
        {
            return Products.OrderBy(p => p.Name).ToList();
        }

        public Product GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            product.Id = _nextId++;
            Products.Add(product);
        }

        public bool Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var existing = GetById(product.Id);
            if (existing == null)
            {
                return false;
            }

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.Stock = product.Stock;

            return true;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null)
            {
                return false;
            }

            Products.Remove(existing);
            return true;
        }
    }
}
