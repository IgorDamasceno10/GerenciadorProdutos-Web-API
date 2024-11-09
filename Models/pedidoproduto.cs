using System.ComponentModel.DataAnnotations;

namespace GerenciadorPedidosAPI.Models
{
    public class PedidoProduto
    {
        [Required]
        public int PedidoId { get; set; }

        [Required]
        public Pedido Pedido { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public Produto Produto { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser um valor positivo.")]
        public int Quantidade { get; set; }
    }
}
