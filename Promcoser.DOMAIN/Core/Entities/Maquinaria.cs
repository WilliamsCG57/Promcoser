using System;
using System.Collections.Generic;

namespace Promcoser.DOMAIN.Core.Entities;

public partial class Maquinaria
{
    public long IdMaquinaria { get; set; }

    public string Placa { get; set; } = null!;

    public decimal HorometroInicio { get; set; }

    public decimal HorometroFin { get; set; }

    public decimal HorasInicio { get; set; }

    public decimal HorasFin { get; set; }

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public string? TipoMaquinaria { get; set; }

    public string? Estado { get; set; }

    public int? AnoFabricacion { get; set; }

    public virtual ICollection<ParteDiario> ParteDiario { get; set; } = new List<ParteDiario>();
}
