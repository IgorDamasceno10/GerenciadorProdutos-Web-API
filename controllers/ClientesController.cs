using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadorPedidosAPI.Data;
using GerenciadorPedidosAPI.Models;

namespace GerenciadorPedidosAPI.Controllers
{
    [Route("v1/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: v1/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var clientes = await _context.Clientes
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(clientes);
        }

        // GET: v1/clientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound(new { message = "Cliente não encontrado." });
            }

            return Ok(cliente);
        }

        // POST: v1/clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            // Verificação para garantir que o campo NumeroContato está presente.
            if (string.IsNullOrEmpty(cliente.NumeroContato))
            {
                return BadRequest(new { message = "O número de contato do cliente é obrigatório." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos.", details = ModelState });
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        // PUT: v1/clientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest(new { message = "ID do cliente não corresponde." });
            }

            // Verificação para garantir que o campo NumeroContato está presente.
            if (string.IsNullOrEmpty(cliente.NumeroContato))
            {
                return BadRequest(new { message = "O número de contato do cliente é obrigatório." });
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound(new { message = "Cliente não encontrado para atualização." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: v1/clientes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound(new { message = "Cliente não encontrado para exclusão." });
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
