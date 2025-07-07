using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using triedge_api.Database.Models;
using triedge_api.Global;
using triedge_api.JobManagers;
using triedge_api.JobModels;
using triedge_api.JobModels.ProfileModels;

namespace triedge_api.JobControllers;

[Authorize]
public class ProfileController(UserManager userManager): TriController
{
    protected UserManager _userManager = userManager;

    [HttpPatch]
    [Route("api/profile/password")]
    public IActionResult UpdateMyPassword([FromForm] string password)
    {
        User user =_userManager.UpdateUserPassword(_loggedUserId, password);
        return Return(new ApiResult() { HttpCode = StatusCodes.Status200OK, Content = user.ToDTO() });
    }
}
