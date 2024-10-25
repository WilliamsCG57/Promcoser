using System;
using System.Collections.Generic;

namespace Promcoser.DOMAIN.Core.Entities;

public partial class ParteDiario
{
    public long IdParte { get; set; }

    public string Serie { get; set; } = null!;

    public string Firmas { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public decimal HorometroInicio { get; set; }

    public decimal HorometroFinal { get; set; }

    public long IdCliente { get; set; }

    public long IdPersonal { get; set; }

    public long IdLugarTrabajo { get; set; }

    public long IdMaquinaria { get; set; }

    public decimal? Inicio { get; set; }

    public decimal? Fin { get; set; }

    public virtual ICollection<DetalleParteDiario> DetalleParteDiario { get; set; } = new List<DetalleParteDiario>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual LugarTrabajo IdLugarTrabajoNavigation { get; set; } = null!;

    public virtual Maquinaria IdMaquinariaNavigation { get; set; } = null!;

    public virtual Personal IdPersonalNavigation { get; set; } = null!;
}
