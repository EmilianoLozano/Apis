using System;

namespace ModelsUsuario
{
    public class Usuario
    {
        public int id{get;set;}
        public DateTime fechaAlta{get;set;}
        public bool activo{get;set;}
        public string email{get;set;}
        public string contrasenia{get;set;}

    

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
