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

namespace _2daApi.Controllers
{
    [ApiController]
    
    public class PersonaController : ControllerBase
    {
      
        public List<Persona> ListaPersonas {get;set;}
        private readonly ILogger<PersonaController> _logger;

        public PersonaController(ILogger<PersonaController> logger)
        {
            _logger = logger;


            ListaPersonas=new List<Persona>();

            
            for(int i=0;i<10;i++){

                var p = new Persona();
                p.IdPersona=i;
                p.Nombre="Emiliano" + i;
                p.Apellido="Lozano" + i;
                p.edad=28 + i;

                ListaPersonas.Add(p);

                /* 
                Otra forma de escribirlo como JSON 

                var p1 = new Persona(){

                    apellido="Apellido"+ i,
                    nombre="Nombre"+i
                };
                 */  
            }
        
        }

        [HttpGet]
        [Route("[controller]")]
        public List<Persona> GetPersonas(){

            return ListaPersonas;

        }
         [HttpGet]
        [Route("[controller]/Persona2")]
        public List<Persona> GetPersonas2(){

            return ListaPersonas;

        }
        /*
         [HttpGet]
        [Route("[controller]/{idPersona}")]
        public Persona GetPersonaPorId(int idPersona){
            

            return ListaPersonas[idPersona];

        }
        */
         [HttpGet]
        [Route("[controller]/{IdPersona}")]
        public RespuestaAPI GetPersonaPorId(int IdPersona)  //por defecto [FromQuery]int idPersona
            {
            
            RespuestaAPI respuesta = new RespuestaAPI();

            if(IdPersona<10){

                respuesta.Ok=true;
                respuesta.Respuesta = ListaPersonas[IdPersona];

            }
            else{
                respuesta.Ok=false;
                respuesta.Error="no existe la persona con el id: " + IdPersona;
            }

            return respuesta;
            
            //    return BadRequest(); 
            
          //error 400
            //return ListaPersonas[idPersona];
        // el 404 es NotFound
        }

        
        [HttpPost]
        [Route("[controller]/CargarPersona")]
        public RespuestaAPI CargarPersona([FromBody]ComandoPersona persona){
            
            RespuestaAPI respuesta = new RespuestaAPI();


            if(persona.Nombre.Equals("") )
            {
                respuesta.Ok=false;
                respuesta.Error = "Ingrese el nombre de la persona";
                return respuesta;

            
            }
            if(string.IsNullOrEmpty(persona.Apellido)){

                 respuesta.Ok=false;
                respuesta.Error = "Ingrese el apellido de la persona";
                return respuesta;

            }

            Persona p= new Persona(){ Nombre = persona.Nombre, Apellido = persona.Apellido};
            ListaPersonas.Add(p);

            respuesta.Ok=true;
            respuesta.Respuesta = ListaPersonas;

            return respuesta;
        
        }

        
        // [HttpPut]
        // [Route("[controller]/ActualizarPersona")]
        //   public RespuestaAPI ActualizarPersona([FromBody]ComandoPersona persona){
            
        //     RespuestaAPI respuesta = new RespuestaAPI();

        //     if(persona.Nombre.Equals("") )
        //     {
        //         respuesta.Ok=false;
        //         respuesta.Error = "Ingrese el nombre de la persona";
        //         return respuesta;

        //     }

        //     if(string.IsNullOrEmpty(persona.Apellido)){

        //          respuesta.Ok=false;
        //         respuesta.Error = "Ingrese el apellido de la persona";
        //         return respuesta;

        //     }


            // var exite=false;
            // foreach (Persona p in ListaPersonas){
        // if(p.IdPersona == persona.Id)
        // {
        //     p.nombre=persona.Nombre;
        //     p.Apellido=persona.Apellido;
        //     existe=true;
        //     break;
        // }
            //}
        // respuesta.Ok=existe;
        // respuesta.InfoAdicional=existe ? "" : "No existe la persona que desea actualizar"; //lo mismo q hacer if else
        // respuesta.Respuesta=ListaPersonas;
        // return respuesta;
        //   }



        //CON LINQ
/*
        var pers=ListaPersonas.Where(o => o.IdPersona==persona.Id).FirstOrDefault(); //ToList muestra una lista

        if(pers !=null)
        {
             pers.Nombre=persona.Nombre;
             pers.Apellido=persona.Apellido;
             existe=true;
        }
        else
            existe=false;

           respuesta.Ok=existe;
           respuesta.InfoAdicional=existe ? "" : "No existe la persona que desea actualizar"; //lo mismo q hacer if else
           respuesta.Respuesta=ListaPersonas;
           return respuesta;
        
*/






              
    }
}
