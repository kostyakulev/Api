﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Api.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    [JsonIgnore]
    public virtual User? User { get; set; }
}
