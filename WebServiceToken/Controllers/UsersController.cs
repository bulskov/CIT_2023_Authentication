using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebServiceToken.Models;
using WebServiceToken.Services;

namespace WebServiceToken.Controllers;

[Route("api/v3/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly Hashing _hashing;
    private readonly IConfiguration _configuration;

    public UsersController(IDataService dataService, Hashing hashing, IConfiguration configuration)
    {
        _dataService = dataService;
        _hashing = hashing;
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult SignIn(CreateUserModel model)
    {
        if(_dataService.GetUser(model.Username) != null)
        {
            return BadRequest();
        }

        if(string.IsNullOrEmpty(model.Password))
        {
            return BadRequest();
        }

        (var hashedPwd, var salt) = _hashing.Hash(model.Password);

        _dataService.CreateUser(model.Name, model.Username, hashedPwd, salt, model.Role);

        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login(UserLoginModel model)
    {
        var user = _dataService.GetUser(model.Username);

        if(user == null)
        {
            return BadRequest();
        }

        if(!_hashing.Verify(model.Password, user.Password, user.Salt))
        {
            return BadRequest();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var secret = _configuration.GetSection("Auth:Secret").Value;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(4),
            signingCredentials: creds
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { user.Username, token = jwt });
    }
}
