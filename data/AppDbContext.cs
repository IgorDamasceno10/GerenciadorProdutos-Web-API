using Microsoft.EntityFrameworkCore;
using GerenciadorPedidosAPI.Models;
using System;

namespace GerenciadorPedidosAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProduto> PedidoProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da chave composta para PedidoProduto
            modelBuilder.Entity<PedidoProduto>()
                .HasKey(pp => new { pp.PedidoId, pp.ProdutoId });

            // Configuração de relacionamento e restrições para PedidoProduto
            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.PedidoProdutos)
                .HasForeignKey(pp => pp.PedidoId)
                .OnDelete(DeleteBehavior.Cascade); // Exclui relações se o pedido for excluído

            modelBuilder.Entity<PedidoProduto>()
                .HasOne(pp => pp.Produto)
                .WithMany(p => p.PedidoProdutos)
                .HasForeignKey(pp => pp.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade); // Exclui relações se o produto for excluído

            // Configurações adicionais de propriedades de Cliente
            modelBuilder.Entity<Cliente>()
                .Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            // Atributo NumeroContato com validação de não ser nulo ou vazio
            modelBuilder.Entity<Cliente>()
                .Property(c => c.NumeroContato)
                .IsRequired()
                .HasMaxLength(15); // Definindo um tamanho máximo adequado para o número de telefone

            modelBuilder.Entity<Cliente>()
                .Property(c => c.DataNascimento)
                .HasColumnType("DATE");

            // Configurações adicionais de propriedades de Produto
            modelBuilder.Entity<Produto>()
                .Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Valor)
                .HasPrecision(10, 2); // Define precisão para valores monetários

            // Configurações adicionais de propriedades de Pedido
            modelBuilder.Entity<Pedido>()
                .Property(p => p.Data)
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Define valor padrão de data

            // Seeds iniciais (opcional, pode ser removido)
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, Nome = "Cliente 1", Email = "cliente1@example.com", NumeroContato = "1234567890", DataNascimento = new DateTime(1990, 1, 1) }
            );

            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Produto 1", Tipo = "Tipo A", Valor = 9.99m }
            );
        }
    }
}
