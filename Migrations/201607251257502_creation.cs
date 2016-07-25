namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lojistas",
                c => new
                    {
                        LojistaId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        RazaoSocial = c.String(nullable: false, maxLength: 60),
                        NomeFantasia = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false, maxLength: 120),
                        Telefone = c.String(nullable: false, maxLength: 14),
                        CNPJ = c.String(nullable: false, maxLength: 14),
                    })
                .PrimaryKey(t => t.LojistaId);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        MarcaId = c.Int(nullable: false, identity: true),
                        NomeMarca = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MarcaId);
            
            CreateTable(
                "dbo.Modeloes",
                c => new
                    {
                        ModeloId = c.Int(nullable: false, identity: true),
                        NomeModelo = c.String(nullable: false),
                        MarcaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModeloId)
                .ForeignKey("dbo.Marcas", t => t.MarcaId, cascadeDelete: true)
                .Index(t => t.MarcaId);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        ModeloId = c.Int(nullable: false),
                        UserId = c.String(),
                        AnoModelo = c.String(nullable: false, maxLength: 4),
                        TipoPecaId = c.Int(nullable: false),
                        DataHora = c.DateTime(nullable: false),
                        DescricaoPedido = c.String(nullable: false),
                        Usuario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.PedidoId)
                .ForeignKey("dbo.Modeloes", t => t.ModeloId, cascadeDelete: true)
                .ForeignKey("dbo.TipoPecas", t => t.TipoPecaId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_UsuarioId)
                .Index(t => t.ModeloId)
                .Index(t => t.TipoPecaId)
                .Index(t => t.Usuario_UsuarioId);
            
            CreateTable(
                "dbo.TipoPecas",
                c => new
                    {
                        TipoPecaId = c.Int(nullable: false, identity: true),
                        NomeTipoPeca = c.String(),
                    })
                .PrimaryKey(t => t.TipoPecaId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        NomeCompleto = c.String(nullable: false, maxLength: 120),
                        Email = c.String(nullable: false, maxLength: 120),
                        TelefoneVisivel = c.Boolean(nullable: false),
                        Telefone = c.String(nullable: false, maxLength: 14),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.RespostaPedidos",
                c => new
                    {
                        RespostaPedidosId = c.Int(nullable: false, identity: true),
                        PedidoId = c.Int(nullable: false),
                        LojistaId = c.Int(nullable: false),
                        DataHoraResposta = c.DateTime(nullable: false),
                        Resposta = c.String(),
                        Valor = c.String(),
                        CondicoesPagamento = c.String(),
                        CondicoesEntrega = c.String(),
                    })
                .PrimaryKey(t => t.RespostaPedidosId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidoes", "Usuario_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Pedidoes", "TipoPecaId", "dbo.TipoPecas");
            DropForeignKey("dbo.Pedidoes", "ModeloId", "dbo.Modeloes");
            DropForeignKey("dbo.Modeloes", "MarcaId", "dbo.Marcas");
            DropIndex("dbo.Pedidoes", new[] { "Usuario_UsuarioId" });
            DropIndex("dbo.Pedidoes", new[] { "TipoPecaId" });
            DropIndex("dbo.Pedidoes", new[] { "ModeloId" });
            DropIndex("dbo.Modeloes", new[] { "MarcaId" });
            DropTable("dbo.RespostaPedidos");
            DropTable("dbo.Usuarios");
            DropTable("dbo.TipoPecas");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Modeloes");
            DropTable("dbo.Marcas");
            DropTable("dbo.Lojistas");
        }
    }
}
