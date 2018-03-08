using System;

namespace Database
{
    public class AccessToken
    {
        public int AccessTokenId { get; set; }
        public string TokenString { get; set; }
        public DateTime Expiry { get; set; }
        public User User { get; set; }

        public AccessToken(TimeSpan lifetime, User user)
        {
            TokenString = Guid.NewGuid().ToString();
            Expiry = DateTime.Now + lifetime;
            User = user;
        }

        public AccessToken(DateTime expiry, User user)
        {
            TokenString = Guid.NewGuid().ToString();
            Expiry = expiry;
            User = user;
        }
    }
}