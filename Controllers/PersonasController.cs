using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practica.Data;
using Practica.Models;

namespace Practica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public PersonasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetActionResult()
        {
            string connectionString = _configuration.GetConnectionString("conexionSql");
            var personas = PersonaData.ObtenerTodos(connectionString);
            return Ok(personas);
        }

        [HttpPost]
        public IActionResult CrearPersona([FromBody] Persona persona)
        {
            string connectionString = _configuration.GetConnectionString("ConexionSql");
            var result = PersonaData.Agregar(persona, connectionString);
            if (result)
            {
                return Ok(new { mensaje = "Persona creada correctamente" });
            }
            return BadRequest(new { mensaje = "Erro al crear la perosna" });
        }

        [HttpGet("{Id}")]
        public IActionResult ObtenerPersona(int Id)
        {
            string connectionString = _configuration.GetConnectionString("ConexionSql");
            var persona = PersonaData.Obtener(Id, connectionString);
            return Ok(persona);
        }

        [HttpPatch("{Id}")]
        public IActionResult ActualizarPersona(int Id, [FromBody] Persona persona)
        {
            string connectionString = _configuration.GetConnectionString("ConexionSql");
            var personaActualizada = PersonaData.Actualizar(Id, persona, connectionString);
            if (personaActualizada != null)
            {
                return Ok(new { mensaje = "Persona actualizada correctamente", persona = personaActualizada });
            }
            return NotFound(new { mensaje = "No se encontró la persona para actualizar" });
        }
        [HttpDelete("{id}")]
        public IActionResult EliminarPersona(int id)
        {
            string connectionString = _configuration.GetConnectionString("ConexionSql");
            var result = PersonaData.Eliminar(id, connectionString);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}