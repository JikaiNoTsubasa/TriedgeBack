using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace triedge_api.Database.Models;

public class Blog : Entity
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;

    [ForeignKey(nameof(Owner))]
    public long OwnerId { get; set; }
    public User Owner { get; set; } = null!;
    public DateTime? PublishedDate { get; set; }

}
