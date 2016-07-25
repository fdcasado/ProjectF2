namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedidoes", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedidoes", "Status");
        }
    }
}
