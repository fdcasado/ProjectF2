namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix16 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RespostaPedidos", "IndNegadoLojista");
            DropColumn("dbo.RespostaPedidos", "MotivoNegarResposta");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RespostaPedidos", "MotivoNegarResposta", c => c.Int(nullable: false));
            AddColumn("dbo.RespostaPedidos", "IndNegadoLojista", c => c.Boolean(nullable: false));
        }
    }
}
