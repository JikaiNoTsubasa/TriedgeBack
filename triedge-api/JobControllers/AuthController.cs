using System;
using log4net;
using Microsoft.AspNetCore.Mvc;
using triedge_api.Database;
using triedge_api.JobModels.AuthModel;
using triedge_api.Services;

namespace triedge_api.JobControllers;

public class AuthController(TriContext context, AuthService authService, HashService hashService) : TriController
{

    private static readonly ILog log = LogManager.GetLogger(typeof(Program));
    private readonly AuthService _authService = authService;
    private readonly HashService _hashService = hashService;
    private readonly TriContext _context = context;

    [HttpPost]
    [Route("api/auth/login")]
    public IActionResult Login([FromForm] RequestLogin requestLogin)
    {
        if (requestLogin.Login == null || requestLogin.Password == null)
        {
            log.Error("Login or password is null");
            return BadRequest();
        }

        var user = _context.Users.FirstOrDefault(u => u.Login == requestLogin.Login);
        if (user == null)
        {
            log.Error($"User not found for login {requestLogin.Login}");
            return BadRequest();
        }

        if (user.CanLogin == false)
        {
            log.Error($"User {requestLogin.Login} can't login: CanLogin is false");
            return Unauthorized();
        }

        if (!_hashService.VerifyPassword(requestLogin.Password, user.Password))
        {
            log.Error($"User {requestLogin.Login} wrong password");
            return BadRequest();
        }

        var token = _authService.GenerateToken(user, 2);
        user.LastConnection = DateTime.UtcNow;
        _context.SaveChanges();
        return StatusCode(StatusCodes.Status200OK, new ResponseLogin { Token = token });
        
    }
}
