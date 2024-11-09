using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadorPedidosAPI.Data;
using GerenciadorPedidosAPI.Models;

namespace GerenciadorPedidosAPI.Controllers
{
    [Route("v1/pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: v1/pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var pedidos = await _context.Pedidos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(p => p.Cliente)
                .ToListAsync();

            if (pedidos.Count == 0)
            {
                return NotFound(new { message = "Nenhum pedido encontrado." });
            }

            return Ok(pedidos);
        }

        // GET: v1/pedidos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.Include(p => p.Cliente).FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound(new { message = "Pedido não encontrado." });
            }

            return Ok(pedido);
        }

        // POST: v1/pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos.", details = ModelState });
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, pedido);
        }

        // PUT: v1/pedidos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return BadRequest(new { message = "ID do pedido não corresponde ao ID informado." });
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound(new { message = "Pedido não encontrado para atualização." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: v1/pedidos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound(new { message = "Pedido não encontrado para exclusão." });
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
