using System;
using System.Collections.Generic;

namespace ExcelManagerIntegralCotton.Models;

public partial class ExcelDatum
{
    public int Id { get; set; }

    public string Uhml { get; set; } = null!;

    public string Ui { get; set; } = null!;

    public string Strength { get; set; } = null!;

    public string Sfi { get; set; } = null!;

    public string Mic { get; set; } = null!;

    public string Colorgrade { get; set; } = null!;

    public string TrashId { get; set; } = null!;
}
