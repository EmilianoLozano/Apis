using System.ComponentModel.DataAnnotations.Schema;
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
using Comandos;
using Microsoft.AspNetCore.Cors;

namespace _2daApi.Controllers
{
    [ApiController]
    [EnableCors("Prog3")]
    
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
        [Route("[controller]/ObtenerPersona")]
        public ActionResult<RespuestaAPI> GetPersonas(){

            var resultado=new RespuestaAPI();

            resultado.Ok=true;
            resultado.Respuesta=ListaPersonas;
            return resultado;

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
        public ActionResult<RespuestaAPI> GetPersonaPorId(int Id)  //por defecto [FromQuery]int idPersona
            {
            
            var resultado=new RespuestaAPI();


            try{

                // var per2 = ListaPersonas.FirstOrDefault(c => c.Apellido.Equals("Lozano")); // mas eficiente q la siguiente
                var per = ListaPersonas.Where(c => c.IdPersona == Id).FirstOrDefault();
                resultado.Ok=true;
                resultado.Respuesta=per;
                return resultado;

            }
            catch(Exception ex){
                resultado.Ok=false;
                resultado.Error="No existe id - " + ex.Message;
                return resultado;

            }

           
           
            //    return BadRequest(); 
            
          //error 400
            //return ListaPersonas[idPersona];
        // el 404 es NotFound
        }

        [HttpPost]
        [Route("[controller]/AltaPersona")] 

        public ActionResult<RespuestaAPI> AltaPersona ([FromBody]ComandoPersona comando)
        {
            var resultado=new RespuestaAPI();


            if(comando.Nombre.Equals("")){
                resultado.Ok=false;
                resultado.Error="ingrese nombre";
                return resultado;
            }
            if(comando.Apellido.Equals("")){
                resultado.Ok=false;
                resultado.Error="ingrese apellido";
                return resultado;
            }

            var per= new Persona();
            per.Nombre=comando.Nombre;
            per.Apellido=comando.Apellido;
            per.fechaNacimiento=comando.FechaNacimiento;
            per.Soltero=comando.Soltero;
            per.IdPersona=ListaPersonas.Count;

            ListaPersonas.Add(per);
            resultado.Ok=true;
            resultado.Respuesta=ListaPersonas;

            return resultado;




        }

        
        [HttpPut]
        [Route("[controller]/Update")] 

        public ActionResult<RespuestaAPI> UpdatePersona ([FromBody]ComandoUpdatePersona comando)
        {
            var resultado=new RespuestaAPI();


            if(comando.Nombre.Equals("")){
                resultado.Ok=false;
                resultado.Error="ingrese nombre";
                return resultado;
            }
            if(comando.apellido.Equals("")){
                resultado.Ok=false;
                resultado.Error="ingrese apellido";
                return resultado;
            }

            ListaPersonas[comando.IdPersona].Nombre=comando.Nombre;
            ListaPersonas[comando.IdPersona].Apellido=comando.apellido;
            


           
            resultado.Ok=true;
            resultado.Respuesta=ListaPersonas;

            return resultado;




        }

        // [HttpPost]
        // [Route("[controller]/CargarPersona")]
        // public RespuestaAPI CargarPersona([FromBody]ComandoPersona persona){
            
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

        //     Persona p= new Persona(){ Nombre = persona.Nombre, Apellido = persona.Apellido};
        //     ListaPersonas.Add(p);

        //     respuesta.Ok=true;
        //     respuesta.Respuesta = ListaPersonas;

        //     return respuesta;
        
        // }

        
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
