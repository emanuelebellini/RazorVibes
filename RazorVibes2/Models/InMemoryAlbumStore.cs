using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorVibes2.Models
{
    public static class InMemoryAlbumStore
    {
        private static readonly object SyncRoot = new object();
        private static readonly List<Album> Albums = new List<Album>();
        private static int _nextId = 1;

        static InMemoryAlbumStore()
        {
            Seed();
        }

        public static IReadOnlyList<Album> GetAll()
        {
            lock (SyncRoot)
            {
                return Albums.Select(Clone).ToList();
            }
        }

        public static Album GetById(int id)
        {
            lock (SyncRoot)
            {
                var album = Albums.FirstOrDefault(a => a.Id == id);
                return album == null ? null : Clone(album);
            }
        }

        public static void Add(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException(nameof(album));
            }

            lock (SyncRoot)
            {
                var toStore = Clone(album);
                toStore.Id = _nextId++;
                Albums.Add(toStore);
                album.Id = toStore.Id;
            }
        }

        public static bool Update(Album album)
        {
            if (album == null)
            {
                throw new ArgumentNullException(nameof(album));
            }

            lock (SyncRoot)
            {
                var existing = Albums.FirstOrDefault(a => a.Id == album.Id);
                if (existing == null)
                {
                    return false;
                }

                existing.Title = album.Title;
                existing.Artist = album.Artist;
                existing.Genre = album.Genre;
                existing.ReleaseDate = album.ReleaseDate;
                existing.Price = album.Price;

                return true;
            }
        }

        public static bool Remove(int id)
        {
            lock (SyncRoot)
            {
                var existing = Albums.FirstOrDefault(a => a.Id == id);
                if (existing == null)
                {
                    return false;
                }

                Albums.Remove(existing);
                return true;
            }
        }

        private static void Seed()
        {
            Albums.Clear();
            _nextId = 1;

            Add(new Album
            {
                Title = "Midnight Horizons",
                Artist = "Neon Echoes",
                Genre = "Synthwave",
                ReleaseDate = new DateTime(2022, 10, 14),
                Price = 11.99m
            });

            Add(new Album
            {
                Title = "Skyline Stories",
                Artist = "Razor Vibes",
                Genre = "Indie Rock",
                ReleaseDate = new DateTime(2021, 5, 21),
                Price = 9.99m
            });

            Add(new Album
            {
                Title = "City Lights",
                Artist = "Pulse Theory",
                Genre = "Electronic",
                ReleaseDate = new DateTime(2023, 3, 2),
                Price = 12.49m
            });
        }

        private static Album Clone(Album album)
        {
            return new Album
            {
                Id = album.Id,
                Title = album.Title,
                Artist = album.Artist,
                Genre = album.Genre,
                ReleaseDate = album.ReleaseDate,
                Price = album.Price
            };
        }
    }
}
