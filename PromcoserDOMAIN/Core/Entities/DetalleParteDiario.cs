using System;
using System.Collections.Generic;

namespace PromcoserDOMAIN.Core.Entities;

public partial class DetalleParteDiario
{
    public int IdDetalleParteDiario { get; set; }

    public int IdParteDiario { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public string TrabajoEfectuado { get; set; } = null!;

    public string Ocurrencias { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ParteDiario IdParteDiarioNavigation { get; set; } = null!;
}

public partial class DetalleParteDiarioDTO
{
    public int IdParteDiario { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public string TrabajoEfectuado { get; set; } = null!;
    public string Ocurrencias { get; set; } = null!;
    public bool Estado { get; set; }
}

public partial class DetalleParteDiarioUpdateDTO
{
    public int IdDetalleParteDiario { get; set; }
    public int IdParteDiario { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }
    public string TrabajoEfectuado { get; set; } = null!;
    public string Ocurrencias { get; set; } = null!;
    public bool Estado { get; set; }
}