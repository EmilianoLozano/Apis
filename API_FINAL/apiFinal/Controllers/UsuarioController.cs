using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
//using System.Runtime.InteropServices.WindowsRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiFinal.Comandos;
using apiFinal.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Respuestas;

namespace apiFinal.Controllers
{
    [ApiController]
    [EnableCors("Prog3")]
    
    public class UsuarioController : ControllerBase
    {

        ejemploProg3Context db = new ejemploProg3Context();


        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }


        // get personas por id

        [HttpGet]
        [Route ("Usuario/usuarios/{id}")]
        public Usuario ObtenerPorId(int id){

            Usuario usu = db.Usuarios.FirstOrDefault(x => x.Id == id);
            db.Entry(usu).Reference(x=> x.IdRolNavigation).Load();
            return usu;

        }


        // Para obtener todas las personas

    [HttpGet]
    [Route ("Usuario/todos")]
   
    public ActionResult<RespuestaAPI> ObtenerTodos(){

        var respuestas = new RespuestaAPI();

        respuestas.Ok=true;
        respuestas.Respuesta= db.Usuarios.ToList();

        return respuestas;
    }

    // Obtener los roles
    [HttpGet]
    [Route ("Usuario/Roles")]
    public List<Role> ObtenerTipos(){

        return db.Roles.ToList();
    }


    // Alta usuario

    [HttpPost]
    [Route ("Usuarios/Alta")]

    public ActionResult<RespuestaAPI> AltaUsuario([FromBody] comandoUsuario comando){

        var respuestas = new RespuestaAPI();

        if(comando.Email.Equals("")){
            respuestas.Ok=false;
            respuestas.Error="Ingrese el email";
            return respuestas;
        }


        var usuario = new Usuario();

        usuario.Email=comando.Email;
        usuario.Contrasenia=comando.Contrasenia;
        usuario.Activo=comando.Activo;
        usuario.IdRol=comando.rol;

        db.Usuarios.Add(usuario);
        db.SaveChanges();

        db.Entry(usuario).Reference( x => x.IdRolNavigation).Load();

        respuestas.Ok=true;
        respuestas.Respuesta=db.Usuarios.ToList();
        return respuestas;
        
    }

        [HttpPut]
        [Route ("Usuario/actualizar")]
        public ActionResult<RespuestaAPI> ActalizarUsuario([FromBody] comandoActualizar comando){

            var respuestas = new RespuestaAPI();

            if(comando.contrasenia.Equals("")){
                respuestas.Ok=false;
                respuestas.Error="Ingrese contraseña";
                return respuestas;

            }

            var usuario = db.Usuarios.Where( c => c.Id == comando.IdUsuario).FirstOrDefault();

            if(usuario!= null){
                usuario.Contrasenia=comando.contrasenia;
                usuario.IdRol=comando.rol;

            }
            db.Usuarios.Update(usuario);
            db.SaveChanges();

            respuestas.Ok=true;
            respuestas.Respuesta=db.Usuarios.ToList();
            return respuestas;
            
        }


        
    [HttpPost]
    [Route ("Usuarios/Login")]

    public ActionResult<RespuestaAPI> Login([FromBody] comandoLogin comando){

        var respuestas = new RespuestaAPI();
        var email=comando.Email.Trim();
        var contrasenia=comando.Contrasenia;

        try{
            var usuario = db.Usuarios.FirstOrDefault(c => c.Email.Equals(email) && c.Contrasenia.Equals(contrasenia));
            if(usuario!=null){
                if(usuario.Activo==false){
                    respuestas.Ok=false;
                    respuestas.Error="Usuario bloqueado";
                    db.Entry(usuario).Reference(x=> x.IdRolNavigation).Load();
                    respuestas.Respuesta=usuario;
                    return respuestas;
                }
                db.Entry(usuario).Reference(x => x.IdRolNavigation).Load();
                    
                respuestas.Ok=true;
                respuestas.Respuesta=usuario;

                }
            else{
                respuestas.Ok=false;
                respuestas.Error="usuario o contraseña incorrecta";
            }

            return respuestas;
               

            }

            catch{
                respuestas.Ok=false;
                respuestas.Error="Usuario no encontrado";
                return respuestas;
            }
        }
     
        
    















    }
    
}
