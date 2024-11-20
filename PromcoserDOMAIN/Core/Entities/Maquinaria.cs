using System;
using System.Collections.Generic;

namespace PromcoserDOMAIN.Core.Entities;

public partial class Maquinaria
{
    public int IdMaquinaria { get; set; }

    public int IdMarca { get; set; }

    public string Placa { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public decimal HorometroCompra { get; set; }

    public decimal HorometroActual { get; set; }

    public string TipoMaquinaria { get; set; } = null!;

    public int AnoFabricacion { get; set; }

    public bool Estado { get; set; }

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual ICollection<ParteDiario> ParteDiario { get; set; } = new List<ParteDiario>();
}

public partial class MaquinariaDTO
{

    public int IdMarca { get; set; }

    public string Placa { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public decimal HorometroCompra { get; set; }

    public decimal HorometroActual { get; set; }

    public string TipoMaquinaria { get; set; } = null!;

    public int AnoFabricacion { get; set; }

    public bool Estado { get; set; }

}

public class MaquinariaUpdateDTO
{
    public int IdMaquinaria { get; set; }
    public int IdMarca { get; set; }
    public string Placa { get; set; }
    public string Modelo { get; set; }
    public decimal HorometroCompra { get; set; }
    public decimal HorometroActual { get; set; }
    public string TipoMaquinaria { get; set; }
    public int AnoFabricacion { get; set; }
    public bool Estado { get; set; }
}