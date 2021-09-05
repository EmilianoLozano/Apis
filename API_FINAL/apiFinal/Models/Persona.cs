using System;
using System.Collections.Generic;

#nullable disable

namespace apiFinal.Models
{
    public partial class Persona
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Soltero { get; set; }
    }
}
