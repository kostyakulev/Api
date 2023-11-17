using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Api.Models;

public partial class User
{  
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;
    [JsonIgnore]
    public int UserId { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
