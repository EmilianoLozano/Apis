using System.Runtime.InteropServices;
using System.Net.Http.Headers;
using System.Xml;
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
using Data;

namespace _2daApi.Controllers
 {
    [ApiController]
    [EnableCors("Prog3")]
    public class UsuarioController : ControllerBase
    {

        private readonly Context db= new Context();
    
        [HttpGet]
        [Route("[controller]/ObtenerUsuario")]
        public ActionResult<RespuestaAPI> Get(){

            var resultado= new RespuestaAPI();

            try{

                resultado.Ok=true;
                resultado.Respuesta=db.Usuarios.ToList();
                return resultado;

            }
            catch (Exception ex){
                resultado.Ok=false;
                resultado.Error="Usuario no encontrado - "+ ex.Message;  
                return resultado;

            }
        }

        [HttpGet]
        [Route("[controller]/ObtenerUsuarioId/{idUsuario}")]
        public ActionResult<RespuestaAPI> Get3(int idUsuario){
            var resultado=new RespuestaAPI();
            try{
                var u=db.Usuarios.Where(c => c.id == idUsuario).FirstOrDefault();

                resultado.Ok=true;
                resultado.Respuesta= u;

                return resultado;

            }
            catch (Exception ex){
                resultado.Ok=false;
                resultado.Error="Usuario no encontrado - "+ex.Message;
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

                var u = db.Usuarios.FirstOrDefault(c => c.email.Equals(email) && c.contrasenia.Equals(contrasenia));

                if(u != null){
                    if(u.activo == false){
                        resultado.Ok=false;
                        resultado.Error="Usuario bloqueado";
                         db.Entry(u).Reference(x => x.rol).Load();
                        resultado.Respuesta=u;
                        return resultado;
                    }

                db.Entry(u).Reference(x => x.rol).Load();
                    
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

