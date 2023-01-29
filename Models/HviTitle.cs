using System;
using System.Collections.Generic;

namespace ExcelManagerIntegralCotton.Models;

public partial class HviTitle
{
    public int TitleId { get; set; }

    public int? Qty { get; set; }

    public DateTime? Date { get; set; }

    public virtual ICollection<Hvi> Hvis { get; } = new List<Hvi>();
}
