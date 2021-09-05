using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace ModelsUsuario
{
    [Table("usuarios")]
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id{get;set;}
        public DateTime fechaAlta{get;set;}
        public bool activo{get;set;}
        public string email{get;set;}
        public string contrasenia{get;set;}

        public string nombreUsu{get;set;}
         public string ApellidoUsu{get;set;}

        //tiene un (foreing key)

        public int idRol{get;set;}
        [ForeignKey ("idRol")]
        public Rol rol{get;set;}        //de la tabla rol es fk

    public Usuario(int id, string email, string contrasenia, bool activo, DateTime fechaAlta){
        this.id=id;
        this.email=email;
        this.contrasenia=contrasenia;
        this.activo=activo;
        this.fechaAlta=fechaAlta;
    }

        public Usuario()
        {
        }
    }

}
