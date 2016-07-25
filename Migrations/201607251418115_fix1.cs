namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RespostaPedidos", "IndNegarResposta", c => c.Boolean(nullable: false));
            AddColumn("dbo.RespostaPedidos", "MotivoNegarResposta", c => c.String());
            AddColumn("dbo.RespostaPedidos", "IndSolicitarMaisInfo", c => c.Boolean(nullable: false));
            AddColumn("dbo.RespostaPedidos", "DescSolicitarMaisInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RespostaPedidos", "DescSolicitarMaisInfo");
            DropColumn("dbo.RespostaPedidos", "IndSolicitarMaisInfo");
            DropColumn("dbo.RespostaPedidos", "MotivoNegarResposta");
            DropColumn("dbo.RespostaPedidos", "IndNegarResposta");
        }
    }
}
