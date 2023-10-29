using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebServiceHardcodedUser.Models;

namespace WebServiceHardcodedUser.Controllers
{
    [ApiController]
    [Route("api/v1/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly LinkGenerator _generator;
        private readonly IConfiguration _configuration;

        public MoviesController(IDataService dataService, LinkGenerator generator, IConfiguration configuration)
        {
            _dataService = dataService;
            _generator = generator;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            try
            {
                User? user = GetUser();
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
                User? user = GetUser();
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

        private User? GetUser()
        {
            var userName = _configuration.GetSection("Auth:UserName").Value;
            var user = _dataService.GetUser(userName);
            return user;
        }

        private MovieModel CreateMovieDto(Movie? movie)
        {
            return new MovieModel
            {
                Url = _generator.GetUriByName(HttpContext, nameof(GetMovie), new { movieId = movie?.Id}),
                Title = movie?.Title,
                Type = movie?.Type
            };
        }
    }
}
