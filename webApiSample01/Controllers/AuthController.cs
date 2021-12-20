using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApiSample01.ViewModel;
using webApiSample01.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace webApiSample01.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : Controller
    {

        private readonly LeaveSystemContext _context;
        private readonly IConfiguration _config;

        public AuthController(LeaveSystemContext context,IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> AuthAsync([FromBody] LoginViewModel logindm)
        {
            if (ModelState.IsValid)
            {
                
                var resUser = await ValidateUserAsync(logindm);
                if (resUser == null)
                {
                    return NotFound();
                }
                else
                {
                    var result = new LoginResponse
                    {
                        token = GenerateToken(resUser)
                    };

                    return Ok(result);
                }
            }

            return BadRequest();
            
        }

        [HttpGet]
        [Authorize]
        [Route("auth")]
        public async Task<IActionResult> Get()
        {
            if (ModelState.IsValid)
            {
                var user = await GetUsers();

                return Ok(user);

            }

            return BadRequest();

        }

        [HttpGet]
        [Authorize]
        [Route("auth/profile")]
        public async Task<IActionResult> Get(int id)
        {
            if (ModelState.IsValid)
            {
                var user = await GetSingleUser(id);

                if (user == null) {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }

            }

            return BadRequest();

        }

        [HttpDelete]
        [Authorize]
        [Route("auth")]
        public async Task<IActionResult> DeleteAuth(int id)
        {
            if (ModelState.IsValid)
            {
                var currentUser = HttpContext.User;
                if (currentUser.HasClaim(cl => cl.Type == "role"))
                {
                    if (GetAdminRole(currentUser))
                    {
                        var resUser = await DeleteUserAsync(id, currentUser);
                        if (!resUser)
                        {
                            return BadRequest();
                        }
                        else
                        {

                            return Ok("Success");
                        }
                    }

                }

            }

            return BadRequest();

        }

        [HttpPut]
        [Authorize]
        [Route("auth")]
        public async Task<IActionResult> UpdateAuth([FromBody] UpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = HttpContext.User;
                if (currentUser.HasClaim(cl => cl.Type == "role"))
                {
                    if (GetAdminRole(currentUser))
                    {
                        var resUser = await UpdateUserAsync(model, currentUser);
                        if (!resUser)
                        {
                            return BadRequest();
                        }
                        else
                        {

                            return Ok("Success");
                        }
                    }

                }

            }

            return BadRequest();

        }

        private async Task<List<UserAccount>> GetUsers()
        {
            var user = await _context.UserAccount.Include(u => u.UserRoleNavigation).ToListAsync();

            return user;
        }
        private async Task<UserAccount> GetSingleUser(int id)
        {
            var user = await _context.UserAccount
               .Include(u => u.UserRoleNavigation)
               .FirstOrDefaultAsync(m => m.UserId == id);

            return user;
        }

        private async Task<UserAccount> ValidateUserAsync(LoginViewModel logindm)
        {
            var user = await _context.UserAccount
               .Include(u => u.UserRoleNavigation)
               .FirstOrDefaultAsync(m => m.Username == logindm.username && m.UserPassword == logindm.password);

            return user;
        }
        private string GenerateToken(UserAccount user)
        {
            var key = _config.GetValue<string>("Jwt:key");
            var issuer = _config.GetValue<string>("Jwt:issuer");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claim = new List<Claim>();
            claim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claim.Add(new Claim("uid", user.UserId.ToString()));
            claim.Add(new Claim("uname", user.Username));
            claim.Add(new Claim("role", user.UserRole.ToString()));

            var token = new JwtSecurityToken(issuer, issuer, claim, expires: DateTime.Now.AddDays(1),
           signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt_token;
        }
        private async Task<bool> DeleteUserAsync(int id, ClaimsPrincipal currentUser)
        {
            try
            {
                var user = await _context.UserAccount
               .Include(u => u.UserRoleNavigation)
               .FirstOrDefaultAsync(m => m.UserId == id);

                if (user != null)
                {
                    user.IsDeleted = true;
                    user.UpdatedDate = DateTime.Now;
                    user.UpdatedBy = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "uid").Value);
                    _context.UserAccount.Update(user);

                    return true;
                }

                return false;
            }
            catch (Exception ex) {
                return false;
            }          

        }

        private async Task<bool> UpdateUserAsync(UpdateViewModel model, ClaimsPrincipal currentUser)
        {
            try
            {
                var user = await _context.UserAccount
               .Include(u => u.UserRoleNavigation)
               .FirstOrDefaultAsync(m => m.UserId == model.userid);

                if (user != null)
                {
                    user.UserFullName = model.userfullname;
                    user.UserRole = model.userrole;
                    user.UpdatedDate = DateTime.Now;
                    user.UpdatedBy = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "uid").Value);
                    _context.UserAccount.Update(user);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private bool GetAdminRole(ClaimsPrincipal currentUser)
        {
            var role = _context.UserRoles
                .FirstOrDefault(x => x.RoleId == int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "role").Value));

            if (role != null)
            {
                return (bool)role.IsAdmin;
            }
            else
            {
                return false;
            }
        }
    }
}
