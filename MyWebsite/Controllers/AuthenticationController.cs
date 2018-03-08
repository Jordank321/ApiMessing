using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyWebsite.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationData _authenticationData;

        public AuthenticationController(IAuthenticationData authenticationData)
        {
            _authenticationData = authenticationData;
        }
        
        [HttpGet("Nonce/{username}")]
        public async Task<IActionResult> GetNonce(string username)
        {
            if (await _authenticationData.CheckUserExists(username))
            {
                return Ok(_authenticationData.GenerateNonce(username));
            }

            return NotFound();
        }

        [HttpPost("NewUser/{username}/{passwordHash}")]
        public async Task NewUser(string username, string passwordHash)
        {
            await _authenticationData.AddUser(username, passwordHash);
        }

        [HttpGet("AccessToken/{username}/{hash}")]
        public async Task<IActionResult> GetAccessToken(string username, string hash)
        {
            if (await _authenticationData.CheckHash(username, hash))
            {
                return Ok(_authenticationData.GenerateAccessToken(username));
            }

            return Unauthorized();
        }
    }
}
