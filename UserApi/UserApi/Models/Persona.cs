using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}   
        public string Nombre {get;set;}
        public string Apellido {get;set;}
        public DateTime FechaNacimiento {get;set;}
        public bool Soltero {get;set;}

        public Persona(){}


    }
}