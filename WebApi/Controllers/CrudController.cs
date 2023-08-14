using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Threading.Tasks;
using WebApi.Helpers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        [HttpGet]
        [Route("ListarClientes")]
        public async Task<IActionResult> LIstarDatos()
        {
            //var persona = new
            //{
            //    Nombre = "Juancho",
            //    Direccion = "Avenida Las Palmas 31"
            //};
            //return Ok(persona);
            var lista = await Globales.ServicioWebRemoto.ListarCliente();

            return Ok(lista.DataTableToJSON());
        }
        [HttpPost]
        [Route("RegistrarCliente")]
        public async Task<IActionResult> RegistrarDatos([FromBody] Cliente ent)
        {
            var res = await Globales.ServicioWebRemoto.RegistrarCliente(ent);
            return Ok(res);
        }

        [HttpPost]
        [Route("EliminarCliente")]
        public async Task<IActionResult> EliminarCliente([FromBody] Cliente ent)
        {
            var res = await Globales.ServicioWebRemoto.EliminarCliente(ent);
            return Ok(res);
        }
        [HttpPost]
        [Route("ActualizarCliente")]
        public async Task<IActionResult> ActualizarCliente([FromBody] Cliente ent)
        {
            var res = await Globales.ServicioWebRemoto.ActualizarCliente(ent);
            return Ok(res);
        }
    }
}
