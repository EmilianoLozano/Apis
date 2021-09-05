using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table ("personas")]
    public class Persona
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdPersona{get;set;}
        public string Nombre{get;set;}
        public string Apellido{get;set;}
        public int edad{get;set;}

        public bool Soltero{get;set;}

        public DateTime FechaNacimiento{get;set;}

    }
}
