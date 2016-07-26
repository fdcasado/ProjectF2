namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix12 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RespostaPedidos", "NomeMarca");
            DropColumn("dbo.RespostaPedidos", "NomeModelo");
            DropColumn("dbo.RespostaPedidos", "NomeTipoPeca");
            DropColumn("dbo.RespostaPedidos", "AnoModelo");
            DropColumn("dbo.RespostaPedidos", "DescricaoPedido");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RespostaPedidos", "DescricaoPedido", c => c.String());
            AddColumn("dbo.RespostaPedidos", "AnoModelo", c => c.String());
            AddColumn("dbo.RespostaPedidos", "NomeTipoPeca", c => c.String());
            AddColumn("dbo.RespostaPedidos", "NomeModelo", c => c.String());
            AddColumn("dbo.RespostaPedidos", "NomeMarca", c => c.String());
        }
    }
}
