using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorPedidosAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail do cliente é obrigatório.")]
        [EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
        public string Email { get; set; }

        // Tornamos o Número de Contato obrigatório.
        [Required(ErrorMessage = "O número de contato do cliente é obrigatório.")]
        [Phone(ErrorMessage = "Insira um número de telefone válido.")]
        public string NumeroContato { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }

        // Inicializa a coleção para evitar erros de referência nula
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
