using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace triedge_api.Database.Models;

public class Blog : Entity
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Identifier { get; set; } = null!;
    public BlogStatus Status { get; set; } = BlogStatus.DRAFT;


    [ForeignKey(nameof(Owner))]
    public long OwnerId { get; set; }
    public User Owner { get; set; } = null!;
    public DateTime? PublishedDate { get; set; }
    public string? Image { get; set; }
    public List<Category>? Categories { get; set; }
    public int Viewed { get; set; }

}
