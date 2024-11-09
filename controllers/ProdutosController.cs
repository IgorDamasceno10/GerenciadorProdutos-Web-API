using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadorPedidosAPI.Data;
using GerenciadorPedidosAPI.Models;

namespace GerenciadorPedidosAPI.Controllers
{
    [Route("v1/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: v1/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var produtos = await _context.Produtos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (produtos.Count == 0)
            {
                return NotFound(new { message = "Nenhum produto encontrado." });
            }

            return Ok(produtos);
        }

        // GET: v1/produtos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound(new { message = "Produto não encontrado." });
            }

            return Ok(produto);
        }

        // POST: v1/produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dados inválidos.", details = ModelState });
            }

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        // PUT: v1/produtos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest(new { message = "ID do produto não corresponde ao ID informado." });
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound(new { message = "Produto não encontrado para atualização." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: v1/produtos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound(new { message = "Produto não encontrado para exclusão." });
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
