namespace DataLayer
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Salt { get; set; } = String.Empty;
        public string Role { get; set; } = "User";
    }
}
