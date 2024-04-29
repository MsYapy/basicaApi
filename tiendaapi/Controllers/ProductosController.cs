
using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using tiendaapi.Datos;
using tiendaapi.Modelo;

namespace tiendaapi.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController: ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<Mproductos>>> Get()
        {
            var funcion = new Dproductos();
            var lista = await funcion.Mostrarproductos();
            return lista;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Mproductos parametros) {
            Console.WriteLine("*** POST CREATE PRODUCT ***");
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos incorrectos");
            }
            var funcion = new Dproductos();
            await funcion.InsertarProductos(parametros);
            return CreatedAtAction(nameof(Post), parametros);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Mproductos parametros)
        {
            var funcion = new Dproductos();
            parametros.id = id; 
            await funcion.EditarProductos(parametros);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var funcion = new Dproductos();
            var parametros = new Mproductos();
            parametros.id=id;
            await funcion.EliminarProductos(parametros);
            return NoContent();
        }
    }
}
