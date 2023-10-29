namespace DataLayer
{
    class Data
    {
        public static List<Movie> Movies { get; }
        public static List<User> Users { get; set; }

        static Data()
        {
            Movies = new List<Movie>
            {
                new Movie{Id = "tt0325710", Title = "The Last Samurai", Type = "movie"},
                new Movie{Id = "tt4912910", Title = "Mission: Impossible - Fallout", Type = "movie"},
                new Movie{Id = "tt0181689", Title = "Minority Report", Type = "movie"},
                new Movie{Id = "tt1631867", Title = "Edge of Tomorrow", Type = "movie"},
                new Movie{Id = "tt5255760", Title = "Edge of Tomorrow: Storming the Beach", Type = "video"}
            };

            Users = new List<User>
            {
                new User {Id = 1, Name = "Test User", Username = "testing"}
            };
        }
    }
}