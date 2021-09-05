using System.Data.SqlTypes;
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
    [ApiController]
    
    public class RolesController : ControllerBase
    {

        private readonly Context db = new Context();

        private readonly ILogger<RolesController> _logger;

        public RolesController(ILogger<RolesController> logger)
        {
            _logger = logger;

            }
        
        [HttpGet]
        [Route("[controller]/roles")]
        public ActionResult<RespuestaAPI> Get()
        {
            var resultado=new RespuestaAPI();

            resultado.Ok=true;
            resultado.Respuesta=db.Roles.ToList();
            return resultado;
        }
        

    }

}