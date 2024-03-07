namespace Auth.Server.Model
{
    public class DatabaseUser
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public RegisterUser User { get; set; }
    }
}
