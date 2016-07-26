namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assinaturas",
                c => new
                    {
                        LojistaId = c.Int(nullable: false, identity: false),
                        Marca = c.String(nullable: false),
                        Modelo = c.String(nullable: false),
                        TipoPeca = c.String(nullable: false),
                        Lojista_LojistaId = c.Int(),
                    })
                .PrimaryKey(t => t.LojistaId)
                .ForeignKey("dbo.Lojistas", t => t.LojistaId)
                .Index(t => t.Lojista_LojistaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assinaturas", "Lojista_LojistaId", "dbo.Lojistas");
            DropIndex("dbo.Assinaturas", new[] { "Lojista_LojistaId" });
            DropTable("dbo.Assinaturas");
        }
    }
}
