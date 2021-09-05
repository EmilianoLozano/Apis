using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.IO.Pipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Respuestas;
using Comandos_Persona;
using Data;

namespace _2daApi.Controllers
{
    [ApiController]  //!!!!!!!CODE FIRST!!!!!!
    
    public class PersonaController : ControllerBase
    {

        private readonly Context db = new Context();

        private readonly ILogger<PersonaController> _logger;

        public PersonaController(ILogger<PersonaController> logger)
        {
            _logger = logger;

            }
        
        [HttpGet]
        [Route("[controller]/ObtenerPersonas")]
        public ActionResult<RespuestaAPI> Get()
        {
            var resultado = new RespuestaAPI();

            resultado.Ok=true;
            resultado.Respuesta= db.Personas.ToList(); //obtener personas como un select*
            return resultado;
        }

         [HttpGet]
        [Route("[controller]/{idUsuario}")]
        public ActionResult<RespuestaAPI> Get2(int idUsuario)  //por defecto [FromQuery]int idPersona
            {
            
            var resultado = new RespuestaAPI();
            try
            {
                var per= db.Personas.Where(c => c.IdPersona == idUsuario).FirstOrDefault();
                resultado.Ok=true;
                resultado.Respuesta=per;

                return resultado;
            }
            catch(Exception ex)
            {
                resultado.Ok=false;
                resultado.Error="persona no encontrada - "+ex.Message;

                return resultado;
            }
        }

        
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

        [HttpPut]
        [Route("[controller]/UpdatePersona")]
        public ActionResult<RespuestaAPI> UpdatePersona([FromBody]ComandoPersona comando){
            
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

            var per = db.Personas.Where( c => c.IdPersona == comando.idPersona).FirstOrDefault();

            if(per!=null){
                per.Nombre=comando.Nombre;
                per.Apellido=comando.Apellido;
                
                db.Personas.Update(per);
                db.SaveChanges();

            }

            resultado.Ok=true;
            resultado.Respuesta=db.Personas.ToList();
            return resultado;
    
        }

    }

}