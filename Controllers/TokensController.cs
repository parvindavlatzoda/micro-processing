using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MP.Data;
using MP.Models.Token;
using MP.Services;
using Newtonsoft.Json;

namespace MP.Controllers {
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("/api/{version:apiVersion}/tokens")]
    public class TokensController : ControllerBase {
        #region Private Members

        #endregion Private Members

        #region Contructor
        public TokensController(IAccountRepository repository, IConfiguration configuration,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {
            // Instantiate the required classes through DI.
            Repository = repository;
            Configuration = configuration;
            UserManager = userManager;
            SignInManager = signInManager;
            // Instantiate a single JsonSerializerSettings object
            // that can be reused multiple times.
            JsonSettings = new JsonSerializerSettings() {
                Formatting = Formatting.Indented
            };
        }
        #endregion Contructor

        #region Shared Properties
        protected IAccountRepository Repository { get; private set; }
        protected IConfiguration Configuration { get; private set; }
        protected UserManager<AppUser> UserManager { get; private set; }
        protected SignInManager<AppUser> SignInManager { get; private set; }
        protected JsonSerializerSettings JsonSettings { get; private set; }
        #endregion Shared Properties


        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody] TokenRequestDto tokenRequestDto) {
            if (tokenRequestDto == null)
                return new StatusCodeResult(500);

            //return Ok("You're retarded cunt");
            return await GetToken(tokenRequestDto);
        }

        private async Task<IActionResult> GetToken(TokenRequestDto tokenRequestDto) {
            try {
                // Check if there's an user with the given email
                var user = await UserManager.FindByEmailAsync(tokenRequestDto.Email);
                // or username
                if (user == null) {
                    user = await UserManager.FindByNameAsync(tokenRequestDto.Email);
                }

                if (user == null
                    || !await UserManager.CheckPasswordAsync(user, tokenRequestDto.Password)) {
                    // User doesn't exists or password mismatch.
                    return new UnauthorizedResult();
                }

                // Email and password matches. Create and return the JWT token.
                DateTime now = DateTime.UtcNow;

                // Add the registered claims for JWT.
                var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString()),
                    new Claim("Username", user.UserName)
                    // TODO: 
                    // Add additional claims here.
                };

                // Get user claims from database.
                var claimsFromDb = new List<Claim>(await UserManager.GetClaimsAsync(user));
                claims.AddRange(claimsFromDb);

                var tokenExpirationMins = Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
                var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));

                var token = new JwtSecurityToken(
                    issuer: Configuration["Auth:Jwt:Issuer"],
                    audience: Configuration["Auth:Jwt:Audience"],
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                    signingCredentials: new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                // Build and return the response.
                var response = new TokenResponseDto() {
                    Token = encodedToken,
                    Expiration = tokenExpirationMins
                };

                return Ok(response);
            }
            catch {
                return new UnauthorizedResult();
            }
        }
    }
}