using System.Collections.Generic;
using System.Linq;

namespace RazorVibes.Models
{
    public static class InMemoryItemRepository
    {
        private static readonly List<Item> Items = new List<Item>
        {
            new Item { Id = 1, Name = "Vinyl Cutter", Description = "Precision blade for clean edges." },
            new Item { Id = 2, Name = "Studio Monitor", Description = "Accurate playback for mixing." }
        };

        private static int _nextId = 3;

        public static IEnumerable<Item> GetAll() => Items;

        public static Item GetById(int id) => Items.FirstOrDefault(item => item.Id == id);

        public static void Add(Item item)
        {
            item.Id = _nextId++;
            Items.Add(item);
        }

        public static void Update(Item updated)
        {
            var existing = GetById(updated.Id);
            if (existing == null)
            {
                return;
            }

            existing.Name = updated.Name;
            existing.Description = updated.Description;
        }

        public static void Delete(int id)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                Items.Remove(existing);
            }
        }
    }
}
