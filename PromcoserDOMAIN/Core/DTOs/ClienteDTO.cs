using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromcoserDOMAIN.Core.DTOs
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string RazonSocial { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public string Ruc { get; set; } = null!;

        public bool Estado { get; set; }
    }

}
