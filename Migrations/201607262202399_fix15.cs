namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix15 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Assinaturas", "Lojista_LojistaId", "dbo.Lojistas");
            DropIndex("dbo.Assinaturas", new[] { "Lojista_LojistaId" });
            //DropColumn("dbo.Assinaturas", "LojistaId");
            //RenameColumn(table: "dbo.Assinaturas", name: "Lojista_LojistaId", newName: "LojistaId");
            DropPrimaryKey("dbo.Assinaturas");
            AddColumn("dbo.Assinaturas", "AssinaturaId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Assinaturas", "LojistaId", c => c.Int(nullable: false));
            AlterColumn("dbo.Assinaturas", "LojistaId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Assinaturas", "AssinaturaId");
            CreateIndex("dbo.Assinaturas", "LojistaId");
            AddForeignKey("dbo.Assinaturas", "LojistaId", "dbo.Lojistas", "LojistaId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assinaturas", "LojistaId", "dbo.Lojistas");
            DropIndex("dbo.Assinaturas", new[] { "LojistaId" });
            DropPrimaryKey("dbo.Assinaturas");
            AlterColumn("dbo.Assinaturas", "LojistaId", c => c.Int());
            AlterColumn("dbo.Assinaturas", "LojistaId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Assinaturas", "AssinaturaId");
            AddPrimaryKey("dbo.Assinaturas", "LojistaId");
            RenameColumn(table: "dbo.Assinaturas", name: "LojistaId", newName: "Lojista_LojistaId");
            AddColumn("dbo.Assinaturas", "LojistaId", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Assinaturas", "Lojista_LojistaId");
            AddForeignKey("dbo.Assinaturas", "Lojista_LojistaId", "dbo.Lojistas", "LojistaId");
        }
    }
}
