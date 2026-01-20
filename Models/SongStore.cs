using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorVibes.Models;

public static class SongStore
{
    private static readonly object LockObject = new();
    private static readonly List<Song> Songs = new();
    private static int _nextId = 1;

    static SongStore()
    {
        Seed();
    }

    public static IReadOnlyList<Song> GetAll()
    {
        lock (LockObject)
        {
            return Songs
                .OrderBy(song => song.Title)
                .ToList();
        }
    }

    public static Song? GetById(int id)
    {
        lock (LockObject)
        {
            return Songs.FirstOrDefault(song => song.Id == id);
        }
    }

    public static void Add(Song song)
    {
        lock (LockObject)
        {
            song.Id = _nextId++;
            Songs.Add(song);
        }
    }

    public static void Update(Song updatedSong)
    {
        lock (LockObject)
        {
            var existing = Songs.FirstOrDefault(song => song.Id == updatedSong.Id);
            if (existing is null)
            {
                return;
            }

            existing.Title = updatedSong.Title;
            existing.Artist = updatedSong.Artist;
            existing.Genre = updatedSong.Genre;
            existing.ReleasedOn = updatedSong.ReleasedOn;
            existing.Rating = updatedSong.Rating;
        }
    }

    public static void Delete(int id)
    {
        lock (LockObject)
        {
            var existing = Songs.FirstOrDefault(song => song.Id == id);
            if (existing is null)
            {
                return;
            }

            Songs.Remove(existing);
        }
    }

    private static void Seed()
    {
        var seedSongs = new List<Song>
        {
            new()
            {
                Title = "Midnight Metro",
                Artist = "Neon Pulse",
                Genre = "Synthwave",
                ReleasedOn = new DateTime(2022, 6, 10),
                Rating = 9
            },
            new()
            {
                Title = "Golden Hour",
                Artist = "Sunset Avenue",
                Genre = "Indie Pop",
                ReleasedOn = new DateTime(2021, 9, 24),
                Rating = 8
            },
            new()
            {
                Title = "Echoes in Glass",
                Artist = "City Echo",
                Genre = "Ambient",
                ReleasedOn = new DateTime(2020, 2, 14),
                Rating = 7
            }
        };

        foreach (var song in seedSongs)
        {
            Add(song);
        }
    }
}
