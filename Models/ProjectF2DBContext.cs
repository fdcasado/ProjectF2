using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectF2.Models
{
    public class ProjectF2DBContext : DbContext
    {
        public DbSet<Assinatura> Assinaturas { get; set; }
        public DbSet<Lojista> Lojistas { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<RespostaPedidos> RespostasPedidos { get; set; }
        public DbSet<TipoPeca> TiposPecas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Conversa> Conversas { get; set; }
    }
}
