using System;

namespace Database
{
    public class Nonce
    {
        public int NonceId { get; set; }
        public User User { get; set; }
        public DateTime Expiry { get; set; }
        public string NonceString { get; set; }
    }
}