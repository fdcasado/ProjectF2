namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lojista : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Lojistas", "ChaveConfirmacao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lojistas", "ChaveConfirmacao", c => c.String(maxLength: 20));
        }
    }
}
