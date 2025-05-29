using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static EikonApi.Factura;

namespace EikonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController(EikonApiDbContext context): Controller
    {
        private readonly EikonApiDbContext _context = context;

        #region Cabecera Factura


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var facturas = await _context.Facturas.ToListAsync();

            if (facturas.Any())
            {
                return Ok(new Response<IEnumerable<Factura>>
                {
                    IsSuccess = true,
                    Result = facturas,
                    Message = "Listado De Facturas"
                });
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearActualizarFactura model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<CrearActualizarFactura>
                {
                    IsSuccess = false,
                    Result = model,
                    Message = "Los campos no son correstos"
                });

            }
          
            await using var transaction = await _context.Database.BeginTransactionAsync();
            var fcIngreso = new Factura()
            {
                Cliente = model.Cliente,
                Fecha = DateTime.Now,
                Numero = model.Numero,

            };
            try
            {
               
                List<FacturaDetalle> facturaDetalles = new List<FacturaDetalle>();
                foreach (var item in model.Detalle)
                {
                    facturaDetalles.Add(new FacturaDetalle() { Descripcion = item.Descripcion, Precio = item.Precio });
                }

                await _context.Facturas.AddAsync(fcIngreso);
                await _context.SaveChangesAsync();

                foreach (FacturaDetalle item in facturaDetalles)
                {
                    item.IdCab = fcIngreso.Id;
                    await _context.FacturaDetalles.AddAsync(item);
                }
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // TODO: Esto deberia loguearse en algun lado, minimo log4net 
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok(new Response<Factura>
            {
                IsSuccess = true,
                Result = fcIngreso,
                Message = "Factura creada correctamente"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            //El id siempre es necesario
            if (id == 0)
            {
                return BadRequest(new Response<Factura>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = "El id es necesario"
                });
            }
            var fc = await GetFacturaCab(id);
            if (fc != null)
            {
                return Ok(new Response<Factura>
                {
                    IsSuccess = true,
                    Result = fc,
                    Message = "Factura actualizada correctamente"
                });
            }

            return NotFound(new Response<Factura>
            {
                IsSuccess = false,
                Result = null,
                Message = "No hay coincidencias"
            });
        }

        private async Task<Factura> GetFacturaCab(int id)
        {
            var fc = await _context.Facturas.Where(x => x.Id == id).FirstOrDefaultAsync();
            return fc;

        }

        #endregion

        #region Detalle Factura

        [HttpGet("Detalle/{id}")]
        public async Task<IActionResult> GetDetallebyId(int id)
        {
            //El id siempre es necesario
            if (id == 0)
            {
                return BadRequest(new Response<FacturaDetalle>
                {
                    IsSuccess = false,
                    Result = null,
                    Message = "El id es necesario"
                });
            }
            var fc = await GetFacturaDet(id);
            if (fc != null)
            {
                return Ok(new Response<List<FacturaDetalle>>
                {
                    IsSuccess = true,
                    Result = fc,
                    Message = "Factura obtenida correctamente"
                });
            }

            return NotFound(new Response<Factura>
            {
                IsSuccess = false,
                Result = null,
                Message = "No hay coincidencias"
            });
        }
        private async Task<List<FacturaDetalle>> GetFacturaDet(int id)
        {
            var fc = await _context.FacturaDetalles.Where(x => x.IdCab == id).ToListAsync();
            return fc;

        }
        #endregion

    }

}
