using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DemoKoCoffee.Model
{
    public class MovieRepository
    {
        private readonly MongoCollection<Movie> _movieCollection;

        public static IEnumerable<Movie> DefaultMovies = new [] 
        {
            new Movie {Id = ObjectId.GenerateNewId(), Title = "Blazing Saddles", ReleaseDate = "Mar 1, 1972"},
            new Movie {Id = ObjectId.GenerateNewId(), Title = "Young Frankenstain", ReleaseDate = "Jan 1, 1972"},
            new Movie {Id = ObjectId.GenerateNewId(), Title = "Spaceballs", ReleaseDate = "Mar 3, 1980"}
        };

        public MovieRepository()
        {
            var server = new MongoClient().GetServer();
            var library = server.GetDatabase("MovieLibrary");
            this._movieCollection = library.GetCollection<Movie>("movies");
        }

        public void Clear()
        {
            this._movieCollection.RemoveAll();
        }

        public bool IsEmpty
        {
            get { return this._movieCollection.Count() == 0; }
        }

        public IEnumerable<Movie> All()
        {
            return _movieCollection.FindAll();
        }

        public void Save(Movie movie)
        {
            this._movieCollection.Save(movie);
        }
    }
}
