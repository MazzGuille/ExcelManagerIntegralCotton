using System;
using System.Collections.Generic;

namespace ExcelManagerIntegralCotton.Models;

public partial class Hvi
{
    public int Id { get; set; }

    public decimal? Uhml { get; set; }

    public decimal? Ui { get; set; }

    public decimal? Strength { get; set; }

    public decimal? Sfi { get; set; }

    public decimal? Mic { get; set; }

    public string? Colorgrade { get; set; }

    public decimal? Trashid { get; set; }

    public int? FileName { get; set; }

    public DateTime? Date { get; set; }

    public virtual HviTitle? FileNameNavigation { get; set; }
}
