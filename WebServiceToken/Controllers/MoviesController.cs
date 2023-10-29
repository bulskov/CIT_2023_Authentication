using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebServiceToken.Models;

namespace WebServiceSimple.Controllers
{
    [ApiController]
    [Route("api/v3/movies")]
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
        [Authorize]
        public IActionResult GetMovies()
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var user = _dataService.GetUser(userName);
                var movies = _dataService.GetMovies(user.Id);
                return Ok(movies.Select(CreateMovieDto));
            }
            catch 
            {
                return Unauthorized();
            }
        }

        [HttpGet("{movieId}", Name = nameof(GetMovie))]
        [Authorize(Roles = "Admin")]
        public IActionResult GetMovie(string movieId)
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var user = _dataService.GetUser(userName);
                var movie = _dataService.GetMovie(user.Id, movieId);
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
