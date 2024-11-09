namespace GerenciadorPedidosAPI.Models;

public class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public DateTime Data { get; set; } = DateTime.Now; // Propriedade Data com valor padrão
    public ICollection<PedidoProduto> PedidoProdutos { get; set; }
}
