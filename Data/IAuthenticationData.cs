
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Data
{
    public interface IAuthenticationData
    {
        Task<string> GenerateNonce(string username);

        Task<bool> CheckHash(string username, string hash);

        Task<bool> AddUser(string username, string passwordHash);

        Task<bool> CheckUserExists(string username);

        Task<string> GenerateAccessToken(string username);
    }
}
