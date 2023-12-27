using System;
using System.Collections.Generic;

namespace wep_api_token.ContextModel;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; }

    public string CorreoElectronico { get; set; }

    public string ContrasenaHash { get; set; }

    public string TokenAcceso { get; set; }

    public string TokenActualizacion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }
}
