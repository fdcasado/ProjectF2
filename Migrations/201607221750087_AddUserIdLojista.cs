namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdLojista : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lojistas", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lojistas", "UserId");
        }
    }
}
