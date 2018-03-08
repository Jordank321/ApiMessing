namespace Database
{
    public class PasswordHash
    {
        public int PasswordHashId { get; set; }
        public User User { get; set; }
        public string HashString { get; set; }
    }
}