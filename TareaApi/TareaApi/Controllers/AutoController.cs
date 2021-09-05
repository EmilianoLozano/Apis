using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Resultados;
using Comando.Auto;
using Comando.Autos;

namespace TareaApi.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class AutoController : ControllerBase
    {

        public List<Auto> ListaAutos{get;set;}
    
        private readonly ILogger<AutoController> _logger;

        public AutoController(ILogger<AutoController> logger)
        {
            _logger = logger;


            ListaAutos = new List<Auto>();

            for (int i = 0; i <10; i++)
            {
                Auto a = new Auto();
                a.idAuto=i;
                a.marca="marca "+ i ;
                a.modelo="modelo "+i;
                a.año=2000+i;
                a.combustible="combustible "+i;

                ListaAutos.Add(a);
            }
        }


        [HttpGet]
        [Route("[controller]/GetAutos")]
        public ActionResult<ResultadoApi> Get1()
        {
            var resultado= new ResultadoApi();
            resultado.Ok=true;
            resultado.Return= ListaAutos;
            return resultado;
        }

        [HttpGet]
        [Route("[controller]/GetAuto/{idAuto}")]
        public ActionResult<ResultadoApi> Get(int idAuto)
        {
            var resultado= new ResultadoApi();
            try
            {
                var aut=ListaAutos[idAuto];
                resultado.Ok=true;
                resultado.Return=aut;   

                return resultado;
            }
            catch
            {
                resultado.Ok=false ;
                resultado.Error="Auto no encontrado, id no existe";
                return resultado;
            }
        }

        [HttpPost]
        [Route("[controller]/PostAuto")]
        public ActionResult<ResultadoApi> PostAuto([FromBody]ComandoCrearAutos comando)
        {
            var resultado= new ResultadoApi();

            if(comando.marca.Equals(""))
            {
                resultado.Ok=false;
                resultado.Error="Ingrese la marca del vehiculo";
                return resultado;
            }
            
            if(comando.modelo.Equals(""))
            {
                resultado.Ok=false;
                resultado.Error="Ingrese el modelo del vehiculo";
                return resultado;
            }

            var aut = new Auto();
            aut.marca=comando.marca;
            aut.modelo=comando.modelo;
            aut.año=comando.año;
            aut.combustible=comando.combustible;
            aut.idAuto= ListaAutos.Count;


            ListaAutos.Add(aut);
            resultado.Ok=true;
            resultado.Return = ListaAutos;

            return resultado;
        }

         [HttpPut]
        [Route("[controller]/PutAuto")]
        public ActionResult<ResultadoApi> PutAuto([FromBody]ComandoActualizarAutos comando)
        {
            var resultado= new ResultadoApi();
            
            if(comando.marca.Equals(""))    
            { 
                resultado.Ok=false;
                resultado.Error="Ingrese la marca del vehiculo";
                return resultado;
            }
            
            if(comando.modelo.Equals(""))
            {
                resultado.Ok=false;
                resultado.Error="Ingrese el modelo del vehiculo";
                return resultado;
            }

            ListaAutos[comando.idAuto].marca=comando.marca;
            ListaAutos[comando.idAuto].modelo=comando.modelo;


            resultado.Ok=true;
            resultado.Return = ListaAutos;

            return resultado;
        }





        
    }
}
