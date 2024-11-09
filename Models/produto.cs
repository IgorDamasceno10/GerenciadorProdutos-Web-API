using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorPedidosAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O tipo do produto é obrigatório.")]
        [StringLength(50, ErrorMessage = "O tipo do produto deve ter no máximo 50 caracteres.")]
        public string Tipo { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do produto deve ser positivo.")]
        public decimal Valor { get; set; }

        public ICollection<PedidoProduto> PedidoProdutos { get; set; } = new List<PedidoProduto>();
    }
}
