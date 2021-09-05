using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

#nullable disable

namespace Repaso.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public string NombreUsu { get; set; }
           public string ApellidoUsu { get; set; }

        public int IdRol { get; set; }
     
    //AGREGAR ESA LINEA SINO DA ERROR
        [ForeignKey("IdRol")]
        public virtual Role IdRolNavigation { get; set; }
    }
}
