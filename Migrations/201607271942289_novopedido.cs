namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novopedido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RespostaPedidos", "IndNovoPedido", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RespostaPedidos", "IndNovoPedido");
        }
    }
}
