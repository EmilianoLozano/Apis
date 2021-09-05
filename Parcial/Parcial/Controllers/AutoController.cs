using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parcial.ComandoAltaVehiculos;
using Parcial.Models;
using Respuestas;

namespace Parcial.Controllers
{
    [ApiController]
    [EnableCors("prog3Modelo2")]

    
    public class AutoController : ControllerBase
    {
       
        parcialProg3AutosContext db = new parcialProg3AutosContext();

        private readonly ILogger<AutoController> _logger;

        public AutoController(ILogger<AutoController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route ("Autos/Marca")]
        public List<Marca> GetTipo()
        {
            return db.Marcas.ToList();
        }

        [HttpGet]
        [Route ("Autos")]
        public List<Vehiculo> GetAutos()
        {
            return db.Vehiculos.ToList();
        }


        [HttpPost]
        [Route("Autos/Alta")]
        public ActionResult<RespuestaAPI> AltaAuto([FromBody]ComandoAlta comando){
            
            var resultado = new RespuestaAPI();

            if(comando.IdMarca < 1  ){
                resultado.Ok=false;
                resultado.Error = "Ingrese correctamente la marca del auto";
                return resultado;
            }

            if(comando.Modelo.Equals("") )
            {
                resultado.Ok=false;
                resultado.Error = "Ingrese el modelo";
                return resultado;

            }
            if(comando.Año.Equals("")){

                resultado.Ok=false;
                resultado.Error = "Ingrese el año";
                return resultado;
            }
             if(comando.Color.Equals("")){

                resultado.Ok=false;
                resultado.Error = "Ingrese el color";
                return resultado;
            }
             if(comando.CapBaul.Equals("")){

                resultado.Ok=false;
                resultado.Error = "Ingrese la capacidad de baul";
                return resultado;
            }
             if(comando.CantPersonas.Equals("")){

                resultado.Ok=false;
                resultado.Error = "Ingrese la capacidad de personas";
                return resultado;
            }


            var auto = new Vehiculo();
            auto.IdMarca=comando.IdMarca;
            auto.Modelo=comando.Modelo;
            auto.Año=comando.Año;
            auto.Color=comando.Color;
            auto.CapBaul=comando.CapBaul;
            auto.CantPersonas=comando.CantPersonas;
          
            db.Vehiculos.Add(auto);
            db.SaveChanges();

                    

            resultado.Ok=true;
            resultado.Respuesta=db.Vehiculos.ToList();
            return resultado;
        }











    }
}
