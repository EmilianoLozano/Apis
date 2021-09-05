using System;
using System.Collections.Generic;


#nullable disable

namespace Parcial.Models
{
    public partial class Vehiculo
    {
        public int Id { get; set; }
        
        public string Modelo { get; set; }
        public int Año { get; set; }
        public string Color { get; set; }
        public int CapBaul { get; set; }
        public int CantPersonas { get; set; }
        public int IdMarca { get; set; }

        public virtual Marca IdMarcaNavigation { get; set; }
    }
}
