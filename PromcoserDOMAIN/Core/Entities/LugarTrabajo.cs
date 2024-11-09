using System;
using System.Collections.Generic;

namespace PromcoserDOMAIN.Core.Entities;

public partial class LugarTrabajo
{
    public int IdLugarTrabajo { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<ParteDiario> ParteDiario { get; set; } = new List<ParteDiario>();
}
