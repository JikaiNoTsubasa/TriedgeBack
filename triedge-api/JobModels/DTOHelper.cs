using System;
using triedge_api.Database.Models;
using triedge_api.JobModels.BlogModels;
using triedge_api.JobModels.UserModels;

namespace triedge_api.JobModels;

public static class DTOHelper
{
    #region User
    public static ResponseUser ToDTO(this User user)
    {
        return new()
        {
            Id = user.Id,
            Login = user.Login,
            Name = user.Name,
            Avatar = user.Avatar,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            CanLogin = user.CanLogin
        };
    }
    #endregion

    #region Blog
    public static ResponseBlog ToDTO(this Blog blog)
    {
        return new()
        {
            Id = blog.Id,
            Title = blog.Title,
            Content = blog.Content,
            Slug = blog.Slug,
            Owner = blog.Owner.ToDTO(),
            CreatedAt = blog.CreatedAt,
            UpdatedAt = blog.UpdatedAt,
            PublishedDate = blog.PublishedDate,
            Image = blog.Image,
            Categories = blog.Categories?.Select(c => c.ToDTO()).ToList()
        };
    }

    public static ResponseCategory ToDTO(this Category category)
    {
        return new()
        {
            Id = category.Id,
            Name = category.Name,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }
    #endregion
}
