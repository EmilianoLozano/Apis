using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    [Table("roles")]
    public class Rol
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id{get;set;}
        public string Nombre{get;set;}
        public string Descripcion{get;set;}
    }
}
