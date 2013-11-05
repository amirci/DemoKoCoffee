using System;
using System.Web.Mvc;

namespace DemoKoCoffee.Controllers
{
    public class MoviesController : Controller
    {
        //
        // GET: /Movies/
        public ActionResult Index()
        {
            var result = new
            {
                movies = new[]
                    {
                        new {title = "Blazing Saddles", releaseDate = "Mar 1, 1972"},
                        new {title = "Young Frankenstain", releaseDate = "Jan 1, 1972"},
                        new {title = "Spaceballs", releaseDate = "Mar 3, 1980"}
                    }
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
