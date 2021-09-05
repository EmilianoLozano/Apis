using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Results;
using Commands.Persona;

namespace NuevaApi.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class PersonaController : ControllerBase
    {
    
        public List<Persona> ListaPersonas {get;set;}
        private readonly ILogger<PersonaController> _logger;

        public PersonaController(ILogger<PersonaController> logger)
        {
            _logger = logger;

            ListaPersonas = new List<Persona>();

            for (int i = 0; i < 10; i++)
            {
                Persona aux = new Persona();
                aux.Id = i;
                aux.Nombre = "Nombre " + i.ToString();
                aux.Apellido = "Apellido " + i.ToString();
                aux.Soltero = true;
                aux.FechaNacimiento = new DateTime(1998,12,12);

                ListaPersonas.Add(aux);
            }
        }

        [HttpGet]
        [Route("Persona/ObtenerPersonas")]
        public ActionResult<ResultApi> Get()
        {
            var resultado = new ResultApi();
            resultado.Ok = true;
            resultado.Return = ListaPersonas;
            return resultado;
        }

        [HttpGet]
        [Route("Persona/ObtenerPersonas2")]
        public ActionResult<ResultApi> Get2()
        {
            var resultado = new ResultApi();
            resultado.Ok=true;
            resultado.Return = ListaPersonas;
            return resultado;
        }
        
        [HttpGet]
        [Route("Persona/ObtenerPersonas/{idUsuario}")]
        public ActionResult<ResultApi> Get3(int idUsuario)
        {
            var resultado = new ResultApi();
            try
            {
                var per = ListaPersonas.Where(c=> c.Id == idUsuario).FirstOrDefault();
                resultado.Ok = true;
                resultado.Return = per;

                return resultado;
            }
            catch(Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Persona no encontrada";

                return resultado;
            } 
        }

        [HttpPut]
        [Route("Persona/UpdatePersona")]
        public ActionResult<ResultApi> UpdatePersona([FromBody]ComandoUpdatePersona comando)
        {
            var resultado = new ResultApi();
            if (comando.Nombre.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese nombre";
                return resultado;
            }
              if (comando.Apellido.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese apellido";
                return resultado;
            }
            ListaPersonas[comando.idPersona].Nombre = comando.Nombre;
            ListaPersonas[comando.idPersona].Apellido = comando.Apellido;

            resultado.Ok=true;
            resultado.Return = ListaPersonas;

            return resultado;

        }

        [HttpPost]
        [Route("Persona/AltaPersona")]
        public ActionResult<ResultApi> AltaPersona([FromBody]ComandoCrearPersona comando)
        {
            var resultado = new ResultApi();
            if (comando.Nombre.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese nombre";
                return resultado;
            }
              if (comando.Apellido.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese apellido";
                return resultado;
            }
            var per = new Persona();
            per.Nombre = comando.Nombre;
            per.Apellido = comando.Apellido;
            per.Soltero = comando.Soltero;
            per.FechaNacimiento = comando.FechaNacimiento;
            per.Id = ListaPersonas.Count;
            ListaPersonas.Add(per);
            resultado.Ok=true;
            resultado.Return = ListaPersonas;

            return resultado;

        }
        
         
    }
}
