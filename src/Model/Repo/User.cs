using System;
using System.Collections.Generic;

namespace src.Model.Repo;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Vcode { get; set; }

    public string UserToken { get; set; } = null!;

    public bool? Deleted { get; set; }
}
