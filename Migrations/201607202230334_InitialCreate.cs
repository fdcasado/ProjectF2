namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lojistas",
                c => new
                    {
                        LojistaId = c.Int(nullable: false, identity: true),
                        RazaoSocial = c.String(nullable: false, maxLength: 60),
                        NomeFantasia = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false, maxLength: 120),
                        Telefone = c.String(nullable: false, maxLength: 14),
                        CNPJ = c.String(nullable: false, maxLength: 14),
                        ChaveConfirmacao = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.LojistaId);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        MarcaId = c.Int(nullable: false, identity: true),
                        NomeMarca = c.String(),
                    })
                .PrimaryKey(t => t.MarcaId);
            
            CreateTable(
                "dbo.Modeloes",
                c => new
                    {
                        ModeloId = c.Int(nullable: false, identity: true),
                        NomeModelo = c.String(),
                        MarcaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModeloId);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        ModeloId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        AnoModelo = c.String(),
                        TipoPecaId = c.Int(nullable: false),
                        DataHora = c.DateTime(nullable: false),
                        DescricaoPedido = c.String(),
                    })
                .PrimaryKey(t => t.PedidoId);
            
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
                        NomeCompleto = c.String(nullable: false, maxLength: 120),
                        Email = c.String(nullable: false, maxLength: 120),
                        TelefoneVisivel = c.Boolean(nullable: false),
                        Telefone = c.String(nullable: false, maxLength: 14),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuarios");
            DropTable("dbo.TipoPecas");
            DropTable("dbo.RespostaPedidos");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Modeloes");
            DropTable("dbo.Marcas");
            DropTable("dbo.Lojistas");
        }
    }
}
