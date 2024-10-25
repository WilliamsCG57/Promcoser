using System;
using System.Collections.Generic;

namespace Promcoser.DOMAIN.Core.Entities;

public partial class Personal
{
    public long IdPersonal { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public long? IdRol { get; set; }

    public string Dni { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }

    public string Direccion { get; set; } = null!;

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<ParteDiario> ParteDiario { get; set; } = new List<ParteDiario>();

    public virtual ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
}
