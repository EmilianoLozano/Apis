using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repaso.Models;
using Respuestas;
using Comandos.Usuario;
using Comandos_Persona;
using Comandos;
using Comandos_Usuario;
using updateUsuarios;

namespace Repaso.Controllers
{
    [ApiController]
    
    public class PersonaController : ControllerBase
    {
    
        ejemploProg3Context db = new ejemploProg3Context();
        //ejemploProg3Context db2 = new ejemploProg3Context();
        
        private readonly ILogger<PersonaController> _logger;

        public PersonaController(ILogger<PersonaController> logger)
        {
            _logger = logger;
        }


        //GET PERSONAS POR ID--------------------------
        [HttpGet]
        [Route ("[controller]/{id}")]
        public Usuario GetById(int id)

      {

            Usuario u = db.Usuarios.FirstOrDefault(x => x.Id == id);
            db.Entry(u).Reference(x=> x.IdRolNavigation).Load();    //Metodo para obtener por id con la foreign key
            return u;

      }
        //GET PERSONAS---------------------------------
        [HttpGet]
        [Route ("[controller]/GetPersonas")]

        public ActionResult<RespuestaAPI> Get(){

            var resultado=new RespuestaAPI();
            resultado.Ok = true;
            resultado.Respuesta=db.Personas.ToList();
            return resultado;
            //return db.Personas.ToList();
        }

        // GET USUARIO ---------------------------------------
        [HttpGet]
        [Route ("Usuario/GetUsuarios")]

        public IEnumerable<Usuario> GetUsuarios(){

           
            return db.Usuarios.ToList();
               
        }

        //ROLES--------------------------------------
        [HttpGet]
            [Route ("Roles/Tipos")]
        public List<Role> GetTipos()
        {
            return db.Roles.ToList();
        }

        //ALTA PERSONA ----------------------------------
        [HttpPost]
        [Route("[controller]/AltaPersona")]
        public ActionResult<RespuestaAPI> AltaPersona([FromBody]ComandoPersona comando){
            
            var resultado = new RespuestaAPI();


            if(comando.Nombre.Equals("") )
            {
                resultado.Ok=false;
                resultado.Error = "Ingrese el nombre de la persona";
                return resultado;

            
            }
            if(string.IsNullOrEmpty(comando.Apellido)){

                resultado.Ok=false;
                resultado.Error = "Ingrese el apellido de la persona";
                return resultado;

            }

            var per=new Persona();
            per.Nombre=comando.Nombre;
            per.Apellido=comando.Apellido;
            per.Soltero=comando.Soltero;
            per.FechaNacimiento=comando.FechaNacimiento;
            
            db.Personas.Add(per);
            db.SaveChanges();

            resultado.Ok=true;
            resultado.Respuesta=db.Personas.ToList();
            return resultado;
        }

        //ALTA USUARIO--------------------------------------
        [HttpPost]
        [Route("Usuario/AltaUsuario")]
        public ActionResult<RespuestaAPI> AltaUsuario([FromBody]ComandoAltaUsuario comando){
            
            var resultado = new RespuestaAPI();


            if(comando.email.Equals("") )
            {
                resultado.Ok=false;
                resultado.Error = "Ingrese el email del usuario";
                return resultado;

            
            }
            if(string.IsNullOrEmpty(comando.contrasenia)){

                resultado.Ok=false;
                resultado.Error = "Ingrese la contrasenia del usuario";
                return resultado;

            }

            var u = new Usuario();
            u.Email=comando.email;
            u.Contrasenia=comando.contrasenia;
            u.IdRol=comando.rol;
            u.Activo=comando.Activo;
            
            db.Usuarios.Add(u);
            db.SaveChanges();

            db.Entry(u).Reference(x => x.IdRolNavigation).Load();
                    

            resultado.Ok=true;
            resultado.Respuesta=db.Usuarios.ToList();
            return resultado;
        }

        //UPDATE USUARIO    -----------------------------------------------

         [HttpPut]
        [Route("Usuario/UpdateUsuario")]
        public ActionResult<RespuestaAPI> UpdateUsuario([FromBody]ComandoUpdateUsuario comando){
            
            var resultado = new RespuestaAPI();


            if(comando.contrasenia.Equals("") )
            {
                resultado.Ok=false;
                resultado.Error = "Ingrese la contraseñas nueva";
                return resultado;

            
            }
          

            var u = db.Usuarios.Where( c => c.Id == comando.IdUsuario).FirstOrDefault();

            if(u!=null){
                u.Contrasenia=comando.contrasenia;
                u.IdRol=comando.rol;
            }
            

            db.Usuarios.Update(u);
            db.SaveChanges();

            resultado.Ok=true;
            resultado.Respuesta=db.Usuarios.ToList();
            return resultado;
    
        }

        //UPDATE PERSONA ----------------------------------------
        [HttpPut]
        [Route("[controller]/UpdatePersona")]
        public ActionResult<RespuestaAPI> UpdatePersona([FromBody]ComandoUpdatePersona comando){
            
            var resultado = new RespuestaAPI();


            if(comando.Nombre.Equals("") )
            {
                resultado.Ok=false;
                resultado.Error = "Ingrese el nombre de la persona";
                return resultado;

            
            }
            if(string.IsNullOrEmpty(comando.apellido)){

                resultado.Ok=false;
                resultado.Error = "Ingrese el apellido de la persona";
                return resultado;

            }

            var per = db.Personas.Where( c => c.IdPersona == comando.IdPersona).FirstOrDefault();

            if(per!=null){
                per.Nombre=comando.Nombre;
                per.Apellido=comando.apellido;
            }
            

            db.Personas.Update(per);
            db.SaveChanges();

            resultado.Ok=true;
            resultado.Respuesta=db.Personas.ToList();
            return resultado;
    
        }

        // LOGIN USUARIO --------------------------------------------------
        [HttpPost]
        [Route("Usuario/Login")]
        public ActionResult<RespuestaAPI> Login([FromBody] ComandoUsuarioLogin comando){

            var resultado=new RespuestaAPI();
            var email=comando.email.Trim();
            var contrasenia=comando.contrasenia;

            try{

                var u = db.Usuarios.FirstOrDefault(c => c.Email.Equals(email) && c.Contrasenia.Equals(contrasenia));

                if(u != null){
                    if(u.Activo == false){
                        resultado.Ok=false;
                        resultado.Error="Usuario bloqueado";
                         db.Entry(u).Reference(x => x.IdRolNavigation).Load();
                        resultado.Respuesta=u;
                        return resultado;
                    }

                db.Entry(u).Reference(x => x.IdRolNavigation).Load();
                    
                resultado.Ok=true;
                resultado.Respuesta=u;

                }

                else{
                    resultado.Ok=false;
                    resultado.Error="Usuario o contraseña incorrectas";
                }
                return resultado;

            }
            catch{
                resultado.Ok=false;
                resultado.Error="Usuario no encontrado";
                return resultado;
            }
            
        }



    }
}
