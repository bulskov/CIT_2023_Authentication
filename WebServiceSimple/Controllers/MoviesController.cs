using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebServiceSimple.Models;

namespace WebServiceSimple.Controllers
{
    [ApiController]
    [Route("api/v2/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _generator;

        public MoviesController(IDataService dataService, LinkGenerator generator)
        {
            _dataService = dataService;
            _generator = generator;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            try
            {
                var user = HttpContext.Items["User"] as User;
                var movies = _dataService.GetMovies(user.Id);
                return Ok(movies.Select(CreateMovieDto));
            }
            catch 
            {
                return Unauthorized();
            }
        }

        [HttpGet("{movieId}", Name = nameof(GetMovie))]
        public IActionResult GetMovie(string movieId)
        {
            try
            {
                var movie = _dataService.GetMovie(-1, movieId);
                return Ok(CreateMovieDto(movie));
            }
            catch 
            {
                return Unauthorized();
            }
        }

        /**
         *
         * Helper
         *
         */


        private MovieModel CreateMovieDto(Movie? movie)
        {
            return new MovieModel
            {
                Url = _generator.GetUriByName(HttpContext, nameof(GetMovie), new { movieId = movie?.Id }),
                Title = movie?.Title,
                Type = movie?.Type
            };
        }
    }
}
