namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RespostaPedidos", "StatusResposta", c => c.Int(nullable: false));
            AddColumn("dbo.RespostaPedidos", "NomeMarca", c => c.String());
            AddColumn("dbo.RespostaPedidos", "NomeModelo", c => c.String());
            AddColumn("dbo.RespostaPedidos", "NomeTipoPeca", c => c.String());
            AddColumn("dbo.RespostaPedidos", "AnoModelo", c => c.String());
            AddColumn("dbo.RespostaPedidos", "DescricaoPedido", c => c.String());
            AddColumn("dbo.RespostaPedidos", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.RespostaPedidos", "MotivoNegarResposta", c => c.Int(nullable: true));
            DropColumn("dbo.RespostaPedidos", "IndNegarResposta");
            DropColumn("dbo.RespostaPedidos", "IndSolicitarMaisInfo");
            DropColumn("dbo.RespostaPedidos", "Valor");
            DropColumn("dbo.RespostaPedidos", "IndStatusRespondido");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RespostaPedidos", "IndStatusRespondido", c => c.Boolean(nullable: false));
            AddColumn("dbo.RespostaPedidos", "Valor", c => c.String());
            AddColumn("dbo.RespostaPedidos", "IndSolicitarMaisInfo", c => c.Boolean(nullable: false));
            AddColumn("dbo.RespostaPedidos", "IndNegarResposta", c => c.Boolean(nullable: false));
            AlterColumn("dbo.RespostaPedidos", "MotivoNegarResposta", c => c.String());
            DropColumn("dbo.RespostaPedidos", "Discriminator");
            DropColumn("dbo.RespostaPedidos", "DescricaoPedido");
            DropColumn("dbo.RespostaPedidos", "AnoModelo");
            DropColumn("dbo.RespostaPedidos", "NomeTipoPeca");
            DropColumn("dbo.RespostaPedidos", "NomeModelo");
            DropColumn("dbo.RespostaPedidos", "NomeMarca");
            DropColumn("dbo.RespostaPedidos", "StatusResposta");
        }
    }
}
