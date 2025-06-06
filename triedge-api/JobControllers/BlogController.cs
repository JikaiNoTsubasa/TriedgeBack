using System;
using Microsoft.AspNetCore.Mvc;
using triedge_api.Database.Models;
using triedge_api.Global;
using triedge_api.JobManagers;
using triedge_api.JobModels;
using triedge_api.JobModels.BlogModels;

namespace triedge_api.JobControllers;

public class BlogController(BlogManager blogManager) : TriController
{
    protected BlogManager _blogManager = blogManager;

    [HttpPost]
    [Route("api/blog")]
    public IActionResult CreateBlog([FromBody] RequestCreateBlog model)
    {
        Blog blog = _blogManager.CreateBlog(1, model.Title, model.Content, model.Image);
        return Return(new ApiResult() { HttpCode = 200, Content = blog.ToDTO() });
    }

    [HttpPatch]
    [Route("api/blog/{id}/Publish")]
    public IActionResult PublishBlog([FromRoute] long id)
    {
        Blog blog = _blogManager.PublishBlog(id);
        return Return(new ApiResult() { HttpCode = 200, Content = blog.ToDTO() });
    }
}
