using System;

namespace triedge_api.Database.Models;

public class Category : Entity
{
    public string Name { get; set; } = null!;
    public List<Blog>? Blogs { get; set; }
}
