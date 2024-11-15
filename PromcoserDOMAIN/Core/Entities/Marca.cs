using System;
using System.Collections.Generic;

namespace PromcoserDOMAIN.Core.Entities;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string NombreMarca { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Maquinaria> Maquinaria { get; set; } = new List<Maquinaria>();
}
