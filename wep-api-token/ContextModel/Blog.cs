using System;
using System.Collections.Generic;

namespace wep_api_token.ContextModel;

public partial class Blog
{
    public int Id { get; set; }

    public string Titulo { get; set; }

    public string Autor { get; set; }

    public DateOnly FechaPublicacion { get; set; }

    public string SubTitulo { get; set; }

    public virtual ICollection<DetalleBlog> DetalleBlogs { get; set; } = new List<DetalleBlog>();
}
