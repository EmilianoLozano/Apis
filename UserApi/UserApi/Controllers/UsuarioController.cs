using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commands.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Results; 
using Microsoft.AspNetCore.Cors;

namespace UserApi.Controllers
{
    [ApiController]
    [EnableCors("Prog3")]
    public class UsuarioController : ControllerBase
    {
        
        public List<Usuario> ListUsuarios {get;set;}

        public UsuarioController()
        {
            ListUsuarios = new List<Usuario>();
            Usuario user = new Usuario();
            user.id = 1;
            user.email = "emilozano425@gmail.com";
            user.activo = true;
            user.password = "123456";

            ListUsuarios.Add(user);   
        }

        [HttpGet]
        [Route("Usario/ObtenerUsuarios")]
        public ActionResult<ResultApi> Get()
        {
            var resultado = new ResultApi();
            try
            {
                resultado.Ok = true;
                resultado.Return = ListUsuarios;
                return resultado;
            }
            catch(Exception ex)
            {
                 resultado.Ok = false;
                 resultado.Error = "error";
                 return resultado;
            }

        } 

        [HttpPost]
        [Route("Usuario/Login")]
        public ActionResult<ResultApi> Login([FromBody] ComandoUsuarioLogin comando)
        {
            var resultado = new ResultApi();
            var email =  comando.email.Trim();
            var password = comando.password;
            try
            {
                var user = ListUsuarios.FirstOrDefault(x => x.email.Equals(email) && x.password.Equals(password));
                if(user != null)
                {
                    resultado.Ok = true;
                    resultado.Return = user;
                }
                else
                {
                    
                    resultado.Ok = false;
                    resultado.Error = "Usuario o Contraseña incorrectos"; 
                }
                return resultado;
            }
             catch(Exception ex)
            {
                 resultado.Ok = false;
                 resultado.Error = "error";
                 return resultado;
            }
        }
       
    }
}