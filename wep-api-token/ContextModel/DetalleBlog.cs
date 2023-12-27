using System;
using System.Collections.Generic;

namespace wep_api_token.ContextModel;

public partial class DetalleBlog
{
    public int Id { get; set; }

    public int? BlogId { get; set; }

    public string Subtitulo { get; set; }

    public string Titulo { get; set; }

    public string ImagenUno { get; set; }

    public string ImagenDos { get; set; }

    public string ParrafoUno { get; set; }

    public string ParrafoDos { get; set; }

    public virtual Blog Blog { get; set; }
}
