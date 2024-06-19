using System;
using System.Collections.Generic;

namespace Prueba2_.Net_Proyecto.Models;

public partial class productos
{
    public int idProducto { get; set; }

    public string nombre { get; set; } = null!;

    public string? descripcion { get; set; }

    public int precio { get; set; }

    public DateTime? creacion { get; set; }
}
