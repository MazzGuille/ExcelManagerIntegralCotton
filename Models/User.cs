using System;
using System.Collections.Generic;

namespace ExcelManagerIntegralCotton.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string UserPassword { get; set; } = null!;
}
