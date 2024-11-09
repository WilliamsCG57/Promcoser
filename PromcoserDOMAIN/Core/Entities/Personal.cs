using System;
using System.Collections.Generic;

namespace PromcoserDOMAIN.Core.Entities;

public partial class Personal
{
    public int IdPersonal { get; set; }

    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }

    public string Direccion { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public bool Estado { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<ParteDiario> ParteDiario { get; set; } = new List<ParteDiario>();
}
