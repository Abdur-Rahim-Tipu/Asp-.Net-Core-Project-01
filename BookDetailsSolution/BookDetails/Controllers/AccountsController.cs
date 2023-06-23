using BookDetails.Models;
using BookDetails.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookDetails.Controllers
{
  
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration config;
        public readonly PublisherDbContext db;
        public AccountsController(UserManager<IdentityUser> userManager, IConfiguration config, PublisherDbContext db)
        {
            this.userManager = userManager;
            this.config = config;
            this.db = db;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            //Thread.Sleep(2000);
            var user = await userManager.FindByNameAsync(model.Username.ToUpper());

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                //var roles = await userManager.GetRolesAsync(user);
                var signingKey =
                  Encoding.UTF8.GetBytes(config["Jwt:SigningKey"] ?? "");
                var expiryDuration = int.Parse(config["Jwt:ExpiryInMinutes"] ?? "");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = null,              // Not required as no third-party is involved
                    Audience = null,            // Not required as no third-party is involved
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                    Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("username",user.UserName ?? ""),
                        new Claim("expires", DateTime.UtcNow.AddMinutes(expiryDuration).ToString("yyyy-MM-ddTHH:mm:ss"))
                    }
                    ),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                var token = jwtTokenHandler.WriteToken(jwtToken);
                return View();
            }
            return BadRequest();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return View();
            }
            return BadRequest("Regiteration failed");

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
