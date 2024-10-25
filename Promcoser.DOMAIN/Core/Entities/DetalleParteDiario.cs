using System;
using System.Collections.Generic;

namespace Promcoser.DOMAIN.Core.Entities;

public partial class DetalleParteDiario
{
    public long IdDetalleParte { get; set; }

    public long IdParteDiario { get; set; }

    public decimal Horas { get; set; }

    public string TrabajoEfectuado { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal CantidadPetroleo { get; set; }

    public decimal? CantidadAceite { get; set; }

    public virtual ParteDiario IdParteDiarioNavigation { get; set; } = null!;
}
