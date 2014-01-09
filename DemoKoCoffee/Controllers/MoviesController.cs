using System.Web.Mvc;
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
            var movie = new Movie {Title = title, ReleaseDate = releaseDate};

            Repository().Save(movie);

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

        private MongoCollection<Movie> Repository()
        {
            var server = new MongoClient().GetServer();
            var library = server.GetDatabase("MovieLibrary");
            return library.GetCollection<Movie>("movies");
        }

        private static void PopulateDefaultMovies(MongoCollection<Movie> repository)
        {
            var movies = new[]
            {
                new Movie {Id = 1, Title = "Blazing Saddles", ReleaseDate = "Mar 1, 1972"},
                new Movie {Id = 2, Title = "Young Frankenstain", ReleaseDate = "Jan 1, 1972"},
                new Movie {Id = 3, Title = "Spaceballs", ReleaseDate = "Mar 3, 1980"}
            };

            movies.ForEach(m => repository.Save(m));
        }
    }

    public class Movie
    {
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public int Id { get; set; }
    }
}
