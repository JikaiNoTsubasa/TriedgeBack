using System;
using triedge_api.Database;
using triedge_api.Database.Models;

namespace triedge_api.JobManagers;

public class BlogManager(TriContext context) : TriManager(context)
{
    public List<Blog> FetchPublicBlogs()
    {
        return [.. _context.Blogs];
    }
}
