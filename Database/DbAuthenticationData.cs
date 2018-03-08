using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DbAuthenticationData : IAuthenticationData
    {
        private  UserContext _userContext;

        public DbAuthenticationData(DbContextOptions<UserContext> options)
        {
            _userContext = new UserContext(options);
        }

        public async Task<string> GenerateNonce(string username)
        {
            var nonce = new Nonce
            {
                User = _userContext.Users.First(u => u.Username == username),
                Expiry = DateTime.Now+TimeSpan.FromMinutes(1),
                NonceString = Guid.NewGuid().ToString()
            };
            await _userContext.Nonces.AddAsync(nonce);
            await _userContext.SaveChangesAsync();
            return nonce.NonceString;
        }

        public async Task<bool> CheckHash(string username, string hash)
        {
            var nonces = await _userContext.Nonces.Where(n => n.User.Username == username && n.Expiry > DateTime.Now).ToListAsync();
            var passwordHash = await _userContext.Hashes.FirstAsync(h => h.User.Username == username);
            var auth = false;
            foreach (var nonce in nonces)
            {
                var hasher = SHA256.Create();
                var expectedKey = $"{username}:{nonce.NonceString}:{passwordHash.HashString}";
                var expectedHashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(expectedKey));
                var expectedHash = new StringBuilder();
                for (var i = 0; i < expectedHashBytes.Length; i++)
                {
                    expectedHash.Append(expectedHashBytes[i].ToString("X2"));
                }

                if (!string.Equals(expectedHash.ToString(),hash, StringComparison.InvariantCultureIgnoreCase)) continue;

                auth = true;
                break;
            }

            return auth;
        }

        public async Task<bool> AddUser(string username, string passwordHash)
        {
            if (_userContext.Users.Any(u => u.Username == username)) return false;

            var user = new User
            {
                Username = username
            };
            await _userContext.Users.AddAsync(user);
            await _userContext.Hashes.AddAsync(new PasswordHash
            {
                User = user,
                HashString = passwordHash
            });
            await _userContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckUserExists(string username)
        {
            return await _userContext.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<string> GenerateAccessToken(string username)
        {
            var user = await _userContext.Users.FirstAsync(u => u.Username == username);
            var accessToken = new AccessToken(TimeSpan.FromHours(2), user);
            await _userContext.AccessTokens.AddAsync(accessToken);
            await _userContext.SaveChangesAsync();
            return accessToken.TokenString;
        }
    }
}
