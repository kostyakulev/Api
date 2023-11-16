using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Api.Models;

public partial class Auth
{


    public string Username { get; set; } = string.Empty;

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }
    
   
}
