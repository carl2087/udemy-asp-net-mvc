using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Vidly.Areas.Identity.Data;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    // [Route("movies")]
    public class MoviesController : Controller
    {
        private VidlyDBContext _context;
        public MoviesController(VidlyDBContext context)
        {
            _context = context;
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList(); ;

            return View(movies);
        }

        // [Route("random")]
        // public ActionResult Random()
        // {
        //     var movie = new Movie()
        //     {
        //         Name = "Shrek!"
        //     };
        //     var customers = new List<Customer>
        //     {
        //         new Customer { Name = "Customer 1"},
        //         new Customer { Name = "Customer 2"}
        //     };

        //     var viewModel = new RandomMovieViewModel
        //     {
        //         Movie = movie,
        //         Customers = customers
        //     };

        //     return View(viewModel);
        // }

        // public ActionResult Edit(int id)
        // {
        //     return Content("id=" + id);
        // }

        // public ActionResult Index(int? pageIndex, string sortBy)
        // {
        //     if (!pageIndex.HasValue)
        //     {
        //         pageIndex = 1;
        //     }

        //     if (String.IsNullOrWhiteSpace(sortBy))
        //     {
        //         sortBy = "Name";
        //     }

        //     // ActionResults can return different methods one shown below is a redirect with 
        //     // new { page = 1, sortBy = "name" } which appears as a query string in the browser
        //     // return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
        //     return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        // }

        // this defines the custom route the regex enforces the parameters
        // these are known as route contraints
        // see above at top of controller for route starting string
        // [Route("released/{year:regex(\\d{{4}})}/{month:regex(\\d{{2}}):range(1, 12)}")]
        // public ActionResult ByReleaseDate(int year, int month)
        // {
        //     return Content(year + "/" + month);
        // }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public ViewResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleasesDate = movie.ReleasesDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}