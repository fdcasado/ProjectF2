namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix13 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RespostaPedidos", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RespostaPedidos", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
