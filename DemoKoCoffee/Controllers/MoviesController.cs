using System.Web.Mvc;
using DemoKoCoffee.Model;
using MongoDB.Bson;
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
            var repo = new MovieRepository();
                
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
            var repository = new MovieRepository();

            if (repository.IsEmpty)
            {
                PopulateDefaultMovies(repository);
            }

            var movies = repository.All();

            var result =
              JsonConvert.SerializeObject(
                new { movies },
                Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
              );

            return this.Content(result, "application/json");
        }


        private static void PopulateDefaultMovies(MovieRepository repository)
        {
            var movies = new[]
            {
                new Movie {Id = ObjectId.GenerateNewId(), Title = "Blazing Saddles", ReleaseDate = "Mar 1, 1972"},
                new Movie {Id = ObjectId.GenerateNewId(), Title = "Young Frankenstain", ReleaseDate = "Jan 1, 1972"},
                new Movie {Id = ObjectId.GenerateNewId(), Title = "Spaceballs", ReleaseDate = "Mar 3, 1980"}
            };

            movies.ForEach(repository.Save);
        }
    }


}
