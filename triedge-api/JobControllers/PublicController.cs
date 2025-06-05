using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using triedge_api.Global;
using triedge_api.JobManagers;
using triedge_api.JobModels;

namespace triedge_api.JobControllers;

[AllowAnonymous]
public class PublicController(BlogManager blogManager) : TriController
{
    protected BlogManager blogManager = blogManager;

    [HttpGet]
    [Route("api/public/blogs")]
    public IActionResult FetchPublicBlogs()
    {
        return Return(new ApiResult() { HttpCode = 200, Content = blogManager.FetchPublicBlogs().Select(b => b.ToDTO()).ToList() });
    }
}
