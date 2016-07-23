namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdOpcional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lojistas", "UserId", c => c.String());
            AlterColumn("dbo.Usuarios", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "UserId", c => c.String(nullable: false));
            AlterColumn("dbo.Lojistas", "UserId", c => c.String(nullable: false));
        }
    }
}
