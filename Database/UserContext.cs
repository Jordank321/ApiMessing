using System;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PasswordHash> Hashes { get; set; }
        public DbSet<Nonce> Nonces { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
    }
}
