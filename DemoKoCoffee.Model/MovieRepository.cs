using System.Collections.Generic;
using MongoDB.Driver;

namespace DemoKoCoffee.Model
{
    public class MovieRepository
    {
        private readonly MongoCollection<Movie> _movieCollection;

        public MovieRepository()
        {
            var server = new MongoClient().GetServer();
            var library = server.GetDatabase("MovieLibrary");
            this._movieCollection = library.GetCollection<Movie>("movies");
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
