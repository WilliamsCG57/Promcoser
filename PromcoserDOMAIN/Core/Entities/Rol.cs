using System;
using System.Collections.Generic;

namespace PromcoserDOMAIN.Core.Entities;

public partial class Rol
{
    public int IdRol { get; set; }

    public string DescripcionRol { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Personal> Personal { get; set; } = new List<Personal>();
}
