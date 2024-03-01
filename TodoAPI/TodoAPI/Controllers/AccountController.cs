using TodoAPI.Domain.DTO.Account;
using TodoAPI.Domain.DTO.CustomResult;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoAPI.Domain.Models;

namespace TodoAPI.Domain.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    #region Constractor (s)
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    public AccountController(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    #endregion

    #region Login
    [HttpPost("login")]
    public async Task<ActionResult<CustomResultDTO>> Login([FromBody] LoginUserDTO userDTO)
    {
        CustomResultDTO result = new CustomResultDTO();
        if (ModelState.IsValid)
        {
            // Check if user exist in the system
            var userExist = await _userManager.FindByEmailAsync(userDTO.Email);

            // Check Password in the system
            if (userExist != null && await _userManager.CheckPasswordAsync(userExist, userDTO.Password))
            {
                // claims
                var token = GenerateJwtToken(userExist);
                result.IsPass = true;
                result.Data = new
                {
                    // generate token
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                    //userId = userExist.UserId
                };
                result.Message = "token created successfully.";
            }
            else
            {
                result.IsPass = false;
                result.Message = "you are not authorized";
            }
        }
        else
        {
            result.IsPass = false;
            result.Message = "Invalid login account.";
            result.Data = ModelState;
        }
        return result;
    }

    private JwtSecurityToken GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("userId", user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:secretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: _configuration["JWT:validIssuer"],
            audience: _configuration["JWT:validAudience"],
            expires: DateTime.Now.AddHours(8),
            claims: claims,
            signingCredentials: creds
        );
    }
    #endregion

    #region Register
    [HttpPost("register")]
    public async Task<ActionResult<CustomResultDTO>> Register([FromBody] RegisterUserDTO registerModel)
    {
        CustomResultDTO customResult = new CustomResultDTO();
        if (ModelState.IsValid)
        {
            // dublicated Email
            var existingUser = await _userManager.FindByEmailAsync(registerModel.Email);
            if (existingUser != null)
            {
                customResult.IsPass = false;
                customResult.Message = "Email is already registered.";
                return customResult;
            }

            // dubblicated userName 
            var existingUserByUserName = await _userManager.FindByNameAsync(registerModel.UserName);
            if (existingUserByUserName != null)
            {
                customResult.IsPass = false;
                customResult.Message = "Username is already exist.";
                return customResult;
            }
            var highestUserId = await _userManager.Users.AnyAsync() ? await _userManager.Users.MaxAsync(u => u.UserId) : 1;

            // craete new user
            var user = new User
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                PhoneNumber = registerModel.PhoneNumber,
                UserId = highestUserId
            };

            var createResult = await _userManager.CreateAsync(user, registerModel.Password);

            if (createResult.Succeeded)
            {
                customResult.IsPass = true;
                customResult.Message = "Account created succesfully.";
                customResult.Data = $"Account created succesfully at {DateTime.UtcNow}";
            }
            else
            {
                customResult.IsPass = false;
                customResult.Message = "Account creation failed.";
                customResult.Data = ModelState;
            }
        }
        else
        {
            customResult.IsPass = false;
            customResult.Message = "Invalid Register Data.";
            customResult.Data = ModelState;
        }
        return customResult;
    }
    #endregion
}

