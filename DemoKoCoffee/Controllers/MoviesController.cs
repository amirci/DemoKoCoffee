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
            MovieRepository.DefaultMovies.ForEach(repository.Save);
        }
    }


}
