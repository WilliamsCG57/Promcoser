using System;
using System.Collections.Generic;

namespace Promcoser.DOMAIN.Core.Entities;

public partial class LugarTrabajo
{
    public long IdLugarTrabajo { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<ParteDiario> ParteDiario { get; set; } = new List<ParteDiario>();
}
