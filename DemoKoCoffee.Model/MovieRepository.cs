﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DemoKoCoffee.Model
{
    public class MovieRepository
    {
        private readonly IMongoCollection<Movie> _movieCollection;

        public static IEnumerable<Movie> DefaultMovies = new [] 
        {
            new Movie {Id = ObjectId.GenerateNewId(), Title = "Blazing Saddles", ReleaseDate = "Mar 1, 1972"},
            new Movie {Id = ObjectId.GenerateNewId(), Title = "Young Frankenstain", ReleaseDate = "Jan 1, 1972"},
            new Movie {Id = ObjectId.GenerateNewId(), Title = "Spaceballs", ReleaseDate = "Mar 3, 1980"}
        };

        public MovieRepository()
        {
            var server = new MongoClient();
            var library = server.GetDatabase("MovieLibrary");
            _movieCollection = library.GetCollection<Movie>("movies");
        }

        public void Clear()
        {
            _movieCollection.DeleteManyAsync(m => true).Wait();
        }

        public bool IsEmpty => _movieCollection.CountAsync(m => true).Result > 0;

        public IEnumerable<Movie> All()
        {
            return this._movieCollection.Find(m => true).ToListAsync().Result.ToList();
        }

        public void Save(Movie movie)
        {
            this._movieCollection.InsertOneAsync(movie).Wait();
        } 
    }
}
