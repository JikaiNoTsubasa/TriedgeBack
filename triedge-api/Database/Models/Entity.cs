using System;
using System.ComponentModel.DataAnnotations;

namespace triedge_api.Database.Models;

public class Entity
{
    [Key]
    public long Id { get; set; }
}
