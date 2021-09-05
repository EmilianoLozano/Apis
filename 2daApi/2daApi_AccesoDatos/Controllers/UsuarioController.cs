using System.Runtime.Intrinsics.X86;
using System.Data;
using System.Net;
using System.Linq.Expressions;
using System.Text;
using System.IO.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelsUsuario;
using Respuestas;
using Comandos.Usuario;
using Microsoft.AspNetCore.Cors;

namespace _2daApi.Controllers
{
    [ApiController]
    [EnableCors("Prog3")]
    public class UsuarioController : ControllerBase
    {
        public List<Usuario> ListaUsuarios {get;set;}
    
        public UsuarioController()
        {
            ListaUsuarios=new List<Usuario>();
            
            Usuario u = new Usuario();
            u.email="emilozano425@gmail.com";
            u.activo=true;
            u.fechaAlta=DateTime.Today;
            u.id=1;
            u.contrasenia="1234321";

            ListaUsuarios.Add(u);

            u = new Usuario();
            u.email="emilozano@gmail.com";
            u.activo=false;
            u.fechaAlta=DateTime.Today;
            u.id=2;
            u.contrasenia="123456";

            ListaUsuarios.Add(u);
            
        }
        
         [HttpGet]
        [Route("[controller]/ObtenerUsuario")]
        public ActionResult<RespuestaAPI> Get(){

            var resultado= new RespuestaAPI();

            try{

                resultado.Ok=true;
                resultado.Respuesta=ListaUsuarios;
                return resultado;

            }
            catch{
                resultado.Ok=false;
                resultado.Error="Usuario no encontrado";

                return resultado;

            }


        }

        [HttpPost]
        [Route("Usuario/Login")]

        public ActionResult<RespuestaAPI> Login([FromBody] ComandoUsuarioLogin comando){

            var resultado=new RespuestaAPI();
            var email=comando.email.Trim();
            var contrasenia=comando.contrasenia;

            try{

                var u = ListaUsuarios.FirstOrDefault(c => c.email.Equals(email) && c.contrasenia.Equals(contrasenia));

                if(u != null){
                    if(u.activo == false){
                        resultado.Ok=false;
                        resultado.Error="Usuario bloqueado";
                        return resultado;
                    }
                    
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
        
        //  [HttpGet]
        // [Route("[controller]/{IdUsuario}")]
        // public RespuestaAPI GetUsuarioPorId(int IdUsuario)  //por defecto [FromQuery]int idUsuario
        //     {
            
        //     RespuestaAPI respuesta = new RespuestaAPI();

        //     if(IdUsuario<10){

        //         respuesta.Ok=true;
        //         respuesta.Respuesta = ListaUsuarios[IdUsuario];

        //     }
        //     else{
        //         respuesta.Ok=false;
        //         respuesta.Error="no existe la Usuario con el id: " + IdUsuario;
        //     }

        //     return respuesta;
      
        // }


        // [HttpPost]
        // [Route("[controller]/Login")]
        // public RespuestaAPI CargarUsuario([FromBody]ComandoLogin usuario){
            
        //     RespuestaAPI respuesta = new RespuestaAPI();

        //     var usuarioExiste = ListaUsuarios.Where(o=> o.Email == usuario.email && o.Contrasenia==usuario.contrasenia).FirstOrDefault();

        //     if(usuarioExiste !=null)
        //     {
        //         respuesta.Ok=true;
        //         respuesta.Respuesta=usuarioExiste;
        //     }
        //     else
        //     {
        //         respuesta.Ok=false;
        //         respuesta.Error="Las credenciales son incorrectas, intente nuevamente";
        //     }

        //     return respuesta;
        // }

        //     if(Usuario.Nombre.Equals("") )
        //     {
        //         respuesta.Ok=false;
        //         respuesta.Error = "Ingrese el nombre de la Usuario";
        //         return respuesta;

        //     }

        //     if(string.IsNullOrEmpty(Usuario.Apellido)){

        //          respuesta.Ok=false;
        //         respuesta.Error = "Ingrese el apellido de la Usuario";
        //         return respuesta;

        //     }

        //     Usuario p= new Usuario(){ Nombre = Usuario.Nombre, Apellido = Usuario.Apellido};
        //     ListaUsuarios.Add(p);

        //     respuesta.Ok=true;
        //     respuesta.Respuesta = ListaUsuarios;

        //     return respuesta;
        
        // }






    }
    }

