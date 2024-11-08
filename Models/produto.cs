namespace GerenciadorPedidosAPI.Models; 

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public decimal Valor { get; set; }
    public ICollection<PedidoProduto> PedidoProdutos { get; set; }
}
