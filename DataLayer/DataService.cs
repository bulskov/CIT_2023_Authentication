namespace DataLayer
{
    public interface IDataService
    {
        IList<Movie> GetMovies(int userId);
        Movie? GetMovie(int userId, string movieId);
        User? GetUser(string? username);
        User? GetUser(int id);
        User CreateUser(string name, string username, string password = null, string salt = null, string role = "User");
    }

    public class DataService : IDataService
    {
        private readonly List<Movie> _movies = Data.Movies;
        private readonly List<User> _users = Data.Users;

        public IList<Movie> GetMovies(int userId)
        {
            if(_users.FirstOrDefault(x => x.Id == userId) == null)
                throw new ArgumentException("User not found");
            return _movies;
        }

        public Movie? GetMovie(int userId, string movieId)
        {
            if (_users.FirstOrDefault(x => x.Id == userId) == null)
                throw new ArgumentException("User not found");
            return _movies.FirstOrDefault(x => x.Id == movieId);
        }

        public User? GetUser(string? username)
        {
            return _users.FirstOrDefault(x => x.Username == username);
        }

        public User? GetUser(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public User CreateUser(string name, string username, 
            string password = null, string salt = null, 
            string role = "User")
        {
            var user = new User
            {
                Id = _users.Max(x => x.Id) + 1,
                Name = name,
                Username = username,
                Password = password,
                Salt = salt,
                Role = role
            };
            _users.Add(user);
            return user;
        }

    }
}
