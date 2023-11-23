using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Api.Models;

public partial class User
{
    public int UserId { get; set; } 

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
