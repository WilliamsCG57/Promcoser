using System;
using System.Collections.Generic;

namespace PromcoserDOMAIN.Core.Entities;

public partial class ParteDiario
{
    public int IdParteDiario { get; set; }
    public int IdCliente { get; set; }
    public int IdPersonal { get; set; }
    public int IdLugarTrabajo { get; set; }
    public int IdMaquinaria { get; set; }
    public string Serie { get; set; } = null!;
    public string Firmas { get; set; } = null!;
    public DateOnly Fecha { get; set; }
    public decimal HorometroInicio { get; set; }
    public decimal HorometroFinal { get; set; }
    public decimal CantidadPetroleo { get; set; }
    public decimal CantidadAceite { get; set; }
    public bool Estado { get; set; }
    public virtual ICollection<DetalleParteDiario> DetalleParteDiario { get; set; } = new List<DetalleParteDiario>();
    public virtual Cliente IdClienteNavigation { get; set; } = null!;
    public virtual LugarTrabajo IdLugarTrabajoNavigation { get; set; } = null!;
    public virtual Maquinaria IdMaquinariaNavigation { get; set; } = null!;
    public virtual Personal IdPersonalNavigation { get; set; } = null!;
}

public partial class ParteDiarioDTO
{
    public int IdCliente { get; set; }
    public int IdPersonal { get; set; }
    public int IdLugarTrabajo { get; set; }
    public int IdMaquinaria { get; set; }
    public string Serie { get; set; } = null!;
    public string Firmas { get; set; } = null!;
    public DateOnly Fecha { get; set; }
    public decimal HorometroInicio { get; set; }
    public decimal HorometroFinal { get; set; }
    public decimal CantidadPetroleo { get; set; }
    public decimal CantidadAceite { get; set; }
    public bool Estado { get; set; }
}

public partial class ParteDiarioUpdateDTO
{
    public int IdParteDiario { get; set; }
    public int IdCliente { get; set; }
    public int IdPersonal { get; set; }
    public int IdLugarTrabajo { get; set; }
    public int IdMaquinaria { get; set; }
    public string Serie { get; set; } = null!;
    public string Firmas { get; set; } = null!;
    public DateOnly Fecha { get; set; }
    public decimal HorometroInicio { get; set; }
    public decimal HorometroFinal { get; set; }
    public decimal CantidadPetroleo { get; set; }
    public decimal CantidadAceite { get; set; }
    public bool Estado { get; set; }
}