using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadorPedidosAPI.Data;
using GerenciadorPedidosAPI.Models;

namespace GerenciadorPedidosAPI.Controllers
{
    [Route("v1/pedidos/{pedidoId}/produtos")]
    [ApiController]
    public class PedidoProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidoProdutoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: v1/pedidos/{pedidoId}/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosPorPedido(int pedidoId)
        {
            var produtos = await _context.PedidoProdutos
                .Where(pp => pp.PedidoId == pedidoId)
                .Include(pp => pp.Produto)
                .Select(pp => pp.Produto)
                .ToListAsync();

            if (!produtos.Any())
            {
                return NotFound($"Nenhum produto encontrado para o pedido de ID {pedidoId}.");
            }

            return Ok(produtos);
        }

        // POST: v1/pedidos/{pedidoId}/produtos
        [HttpPost]
        public async Task<ActionResult<PedidoProduto>> PostProdutoNoPedido(int pedidoId, PedidoProduto pedidoProduto)
        {
            var pedido = await _context.Pedidos.FindAsync(pedidoId);
            if (pedido == null)
            {
                return NotFound($"Pedido com o ID {pedidoId} não encontrado.");
            }

            if (pedidoProduto.ProdutoId == 0 || pedidoProduto.Quantidade <= 0)
            {
                return BadRequest("Produto inválido ou quantidade não pode ser zero ou negativa.");
            }

            pedidoProduto.PedidoId = pedidoId;
            _context.PedidoProdutos.Add(pedidoProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProdutosPorPedido), new { pedidoId = pedidoId }, pedidoProduto);
        }

        // DELETE: v1/pedidos/{pedidoId}/produtos/{produtoId}
        [HttpDelete("{produtoId}")]
        public async Task<IActionResult> DeleteProdutoDoPedido(int pedidoId, int produtoId)
        {
            var pedidoProduto = await _context.PedidoProdutos
                .FirstOrDefaultAsync(pp => pp.PedidoId == pedidoId && pp.ProdutoId == produtoId);

            if (pedidoProduto == null)
            {
                return NotFound($"Produto com o ID {produtoId} não encontrado no pedido de ID {pedidoId}.");
            }

            _context.PedidoProdutos.Remove(pedidoProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
