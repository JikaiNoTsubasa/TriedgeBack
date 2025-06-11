using System;
using Microsoft.EntityFrameworkCore;
using triedge_api.Database;
using triedge_api.Database.Models;
using SBIDotnetUtils.Extensions;
using System.Globalization;
using System.Text;
using log4net;
using triedge_api.Exceptions;

namespace triedge_api.JobManagers;

public class BlogManager(TriContext context) : TriManager(context)
{
    private static readonly ILog log = LogManager.GetLogger(typeof(BlogManager));

    #region Blog

    private IQueryable<Blog> GenerateBlogQuery() => _context.Blogs.Include(b => b.Owner).Include(b => b.Categories);

    public List<Blog> FetchPublicBlogs()
    {
        return [.. GenerateBlogQuery()
            .Where(b => b.Status == BlogStatus.PUBLISHED)
            .OrderByDescending(b => b.PublishedDate)];
    }

    public Blog FetchBlogBySlug(string slug)
    {
        return GenerateBlogQuery()
            .FirstOrDefault(b => b.Slug == slug) ?? throw new TriEntityNotFoundException($"Blog not found for slug {slug}");
    }

    public List<Blog> FetchMyBlogs(long userId)
    {
        return [.. GenerateBlogQuery()
            .Where(b => b.OwnerId == userId)];
    }

    public Blog FetchMyBlogById(long id, long userId)
    {
        return GenerateBlogQuery()
            .FirstOrDefault(b => b.Id == id && b.OwnerId == userId) ?? throw new TriEntityNotFoundException($"Blog not found for id {id} and user {userId}");
    }

    public Blog UpdateMyBlog(long id, long userId, string? title = null, string? content = null, string? image = null)
    {
        Blog blog = _context.Blogs.Include(b => b.Owner).FirstOrDefault(b => b.Id == id && b.OwnerId == userId) ?? throw new TriEntityNotFoundException($"Blog not found for id {id}");
        blog.Title = title ?? blog.Title;
        blog.Content = content ?? blog.Content;
        blog.Image = image ?? blog.Image;
        blog.MarkAsUpdated();
        _context.SaveChanges();
        return blog;
    }

    public Blog CreateBlog(long ownerId, string title, string content, string? image = null)
    {
        User user = _context.Users.FirstOrDefault(u => u.Id == ownerId)!;
        Blog blog = new()
        {
            Owner = user,
            Title = title,
            Content = content,
            Image = image,
            Identifier = Guid.NewGuid().ToString(),
            Slug = title.ToSlug(),
        };
        blog.MarkAsCreated();
        _context.Blogs.Add(blog);
        _context.SaveChanges();
        return blog;
    }

    public Blog PublishBlog(long id)
    {
        log.Info($"Publishing blog {id}");
        var blog = _context.Blogs.Include(b => b.Owner).FirstOrDefault(b => b.Id == id)!;
        blog.Status = BlogStatus.PUBLISHED;
        blog.PublishedDate = DateTime.UtcNow;
        blog.Slug = string.Format("{0:yyyy-MM-dd}-{1}", blog.PublishedDate, blog.Title).ToSlug();
        blog.MarkAsUpdated();
        _context.SaveChanges();
        IndexBlogContentAsync(id).Wait();
        log.Info($"Published blog {id}");
        return blog;
    }

    public async Task IndexBlogContentAsync(long id)
    {
        log.Info($"Indexing blog {id}");
        var blog = _context.Blogs.Include(b => b.Owner).FirstOrDefault(b => b.Id == id)!;

        HttpClient client = new();
        string url = "https://ft.coregeek.fr/api/index/queue";
        var data = new
        {
            Name = blog.Title,
            Content = blog.Content,
            Identifier = blog.Identifier,
            Application = "triedgeblog"
        };
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            log.Info($"Sent blog {blog.Identifier} to indexing queue");
        }
        else
        {
            log.Error($"Failed to send blog {blog.Identifier} to indexing queue");
        }
        log.Info($"Indexed blog {id}");
    }

    #endregion
    #region Category

    public Category CreateCategory(string name)
    {
        Category category = new()
        {
            Name = name
        };
        category.MarkAsCreated();
        _context.Categories.Add(category);
        _context.SaveChanges();
        return category;
    }

    public List<Category> FetchCategories() => [.. _context.Categories];

    public Category FetchCategoryById(long id) => _context.Categories.FirstOrDefault(c => c.Id == id) ?? throw new TriEntityNotFoundException($"Category not found for id {id}");

    public Category UpdateCategory(long id, string name)
    {
        Category category = FetchCategoryById(id);
        category.Name = name;
        category.MarkAsUpdated();
        _context.SaveChanges();
        return category;
    }
    #endregion
}
