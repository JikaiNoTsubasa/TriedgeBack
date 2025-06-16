using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using triedge_api.Database.Models;
using triedge_api.Global;
using triedge_api.JobManagers;
using triedge_api.JobModels;
using triedge_api.JobModels.BlogModels;

namespace triedge_api.JobControllers;

[Authorize]
public class BlogController(BlogManager blogManager) : TriController
{
    protected BlogManager _blogManager = blogManager;

    #region Blog

    [HttpGet]
    [Route("api/myblogs")]
    public IActionResult FetchMyBlogs()
    {
        return Return(new ApiResult() { HttpCode = StatusCodes.Status200OK, Content = _blogManager.FetchMyBlogs(_loggedUserId).Select(b => b.ToDTO()).ToList() });
    }

    [HttpGet]
    [Route("api/myblog/{id}")]
    public IActionResult FetchMyBlogById([FromRoute] long id)
    {
        return Return(new ApiResult() { HttpCode = StatusCodes.Status200OK, Content = _blogManager.FetchMyBlogById(id, _loggedUserId).ToDTO() });
    }

    [HttpPost]
    [Route("api/blog")]
    public IActionResult CreateBlog([FromBody] RequestCreateBlog model)
    {
        Blog blog = _blogManager.CreateBlog(_loggedUserId, model.Title, model.Content, model.Image, model.CategoryIds);
        return Return(new ApiResult() { HttpCode = StatusCodes.Status201Created, Content = blog.ToDTO() });
    }

    [HttpPut]
    [Route("api/myblog/{id}")]
    public IActionResult UpdateMyBlog([FromRoute] long id, [FromBody] RequestUpdateBlog model)
    {
        Blog blog = _blogManager.UpdateMyBlog(id, _loggedUserId, model.Title, model.Content, model.Image, model.CategoryIds);
        return Return(new ApiResult() { HttpCode = StatusCodes.Status200OK, Content = blog.ToDTO() });
    }

    [HttpPatch]
    [Route("api/blog/{id}/Publish")]
    public IActionResult PublishBlog([FromRoute] long id)
    {
        Blog blog = _blogManager.PublishBlog(id);
        return Return(new ApiResult() { HttpCode = StatusCodes.Status200OK, Content = blog.ToDTO() });
    }

    #endregion
    #region Category
    [HttpPost]
    [Route("api/category")]
    public IActionResult CreateCategory([FromBody] RequestCreateCategory model)
    {
        Category category = _blogManager.CreateCategory(model.Name);
        return Return(new ApiResult() { HttpCode = StatusCodes.Status201Created, Content = category.ToDTO() });
    }
    #endregion
}
