using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Api.Models;

public partial class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }
    [JsonIgnore]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    
    
}
