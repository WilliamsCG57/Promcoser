using System;
using System.Collections.Generic;

namespace Promcoser.DOMAIN.Core.Entities;

public partial class Rol
{
    public long IdRol { get; set; }

    public string DescripcionRol { get; set; } = null!;

    public virtual ICollection<Personal> Personal { get; set; } = new List<Personal>();
}
