using System;
using System.Collections.Generic;

namespace ExcelManagerIntegralCotton.Models;

public partial class FileUpload
{
    public int Id { get; set; }

    public string? Ruta { get; set; }

    public string? NombreArchivo { get; set; }
}
