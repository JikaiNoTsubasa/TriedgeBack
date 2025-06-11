using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using triedge_api.Global;
using triedge_api.JobManagers;
using triedge_api.JobModels;

namespace triedge_api.JobControllers;

[AllowAnonymous]
public class PublicController(BlogManager blogManager, UserManager userManager) : TriController
{
    protected BlogManager blogManager = blogManager;
    protected UserManager userManager = userManager;

    #region Blog

    [HttpGet]
    [Route("api/public/blogs")]
    public IActionResult FetchPublicBlogs()
    {
        return Return(new ApiResult() { HttpCode = StatusCodes.Status200OK, Content = blogManager.FetchPublicBlogs().Select(b => b.ToDTO()).ToList() });
    }

    [HttpGet]
    [Route("api/blog/{slug}")]
    public IActionResult FetchBlogBySlug([FromRoute] string slug)
    {
        return Return(new ApiResult() { HttpCode = StatusCodes.Status200OK, Content = blogManager.FetchBlogBySlug(slug).ToDTO() });
    }

    #endregion

    #region User

    [HttpGet]
    [Route("api/user/{id}")]
    public IActionResult FetchUserById([FromRoute] long id)
    {
        return Return(new ApiResult() { HttpCode = StatusCodes.Status200OK, Content = userManager.FetchUserById(id).ToDTO() });
    }
    #endregion

    #region Category
    [HttpGet]
    [Route("api/categories")]
    public IActionResult FetchCategories()
    {
        return Return(new ApiResult() { HttpCode = StatusCodes.Status200OK, Content = blogManager.FetchCategories().Select(c => c.ToDTO()).ToList() });
    }
    #endregion
}
