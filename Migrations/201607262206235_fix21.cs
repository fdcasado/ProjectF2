namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix21 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Assinaturas", "Lojista_LojistaId");
        }
        
        public override void Down()
        {
        }
    }
}
