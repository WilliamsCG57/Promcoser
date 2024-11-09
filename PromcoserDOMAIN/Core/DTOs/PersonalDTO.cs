using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromcoserDOMAIN.Core.DTOs
{
    public class PersonalDTO
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

        public DateOnly FechaNacimiento { get; set; }

        public bool Estado { get; set; }
    }

    public class PersonalRequestAuthDTO
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Usuario { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;
        public DateOnly FechaNacimiento { get; set; }
        public string Direccion { get; set; } = null!;

    }

    public class PersonalResponseAuthDTO
    {
        public int IdPersonal { get; set; }
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Usuario { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;
        public DateOnly? FechaNacimiento { get; set; }
        public string Direccion { get; set; } = null!;
        public string? Token { get; set; }
        public bool IsEmailSent { get; set; }

    }

    public class PersonalByIdResponseDTO
    {
        public int IdPersonal { get; set; }
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Usuario { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;
        public DateOnly FechaNacimiento { get; set; }


    }

    public class PersonalAuthDTO
    {
        public string Usuario { get; set; } = null!;

        public string Contrasena { get; set; } = null!;
    }



}
