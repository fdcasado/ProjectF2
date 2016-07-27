namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversas",
                c => new
                    {
                        ConversaId = c.Int(nullable: false, identity: true),
                        RespostaPedidosId = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                        DataHoraResposta = c.DateTime(nullable: false),
                        Mensagem = c.String(nullable: false),
                        IndMensagemLida = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ConversaId)
                .ForeignKey("dbo.RespostaPedidos", t => t.RespostaPedidosId, cascadeDelete: true)
                .Index(t => t.RespostaPedidosId);
            
            AddColumn("dbo.RespostaPedidos", "IndNegadoLojista", c => c.Boolean(nullable: false));
            DropColumn("dbo.RespostaPedidos", "DataHoraResposta");
            DropColumn("dbo.RespostaPedidos", "StatusResposta");
            DropColumn("dbo.RespostaPedidos", "DescSolicitarMaisInfo");
            DropColumn("dbo.RespostaPedidos", "Resposta");
            DropColumn("dbo.RespostaPedidos", "CondicoesPagamento");
            DropColumn("dbo.RespostaPedidos", "CondicoesEntrega");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RespostaPedidos", "CondicoesEntrega", c => c.String());
            AddColumn("dbo.RespostaPedidos", "CondicoesPagamento", c => c.String());
            AddColumn("dbo.RespostaPedidos", "Resposta", c => c.String());
            AddColumn("dbo.RespostaPedidos", "DescSolicitarMaisInfo", c => c.String());
            AddColumn("dbo.RespostaPedidos", "StatusResposta", c => c.Int(nullable: false));
            AddColumn("dbo.RespostaPedidos", "DataHoraResposta", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Conversas", "RespostaPedidosId", "dbo.RespostaPedidos");
            DropIndex("dbo.Conversas", new[] { "RespostaPedidosId" });
            DropColumn("dbo.RespostaPedidos", "IndNegadoLojista");
            DropTable("dbo.Conversas");
        }
    }
}
