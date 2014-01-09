using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebGrease.Css.Extensions;

namespace DemoKoCoffee.Controllers
{
    public class MoviesController : Controller
    {
        // POST: /Movies
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(string title, string releaseDate)
        {
            var repo = Repository();
                
            var movie = new Movie
            {
                Id = ObjectId.GenerateNewId(),
                Title = title, 
                ReleaseDate = releaseDate
            };

            repo.Save(movie);

            var result =
              JsonConvert.SerializeObject(
                new { movie },
                Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
              );

            return this.Content(result, "application/json");
        }

        //
        // GET: /Movies/
        public ActionResult Index()
        {
            var repository = Repository();

            if (repository.Count() == 0)
            {
                PopulateDefaultMovies(repository);
            }

            var movies = repository.FindAll();

            var result =
              JsonConvert.SerializeObject(
                new { movies },
                Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
              );

            return this.Content(result, "application/json");
        }

        private static MongoCollection<Movie> Repository()
        {
            var server = new MongoClient().GetServer();
            var library = server.GetDatabase("MovieLibrary");
            return library.GetCollection<Movie>("movies");
        }

        private static void PopulateDefaultMovies(MongoCollection<Movie> repository)
        {
            var movies = new[]
            {
                new Movie {Id = ObjectId.GenerateNewId(), Title = "Blazing Saddles", ReleaseDate = "Mar 1, 1972"},
                new Movie {Id = ObjectId.GenerateNewId(), Title = "Young Frankenstain", ReleaseDate = "Jan 1, 1972"},
                new Movie {Id = ObjectId.GenerateNewId(), Title = "Spaceballs", ReleaseDate = "Mar 3, 1980"}
            };

            movies.ForEach(m => repository.Save(m));
        }
    }

    public class Movie
    {
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public ObjectId Id { get; set; }
    }
}
