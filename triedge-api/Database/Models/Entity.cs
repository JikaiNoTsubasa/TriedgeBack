using System;
using System.ComponentModel.DataAnnotations;

namespace triedge_api.Database.Models;

public class Entity
{
    [Key]
    public long Id { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public void MarkAsUpdated() => UpdatedAt = DateTime.UtcNow;
    public void MarkAsCreated()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
