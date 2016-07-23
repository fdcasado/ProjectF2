namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarcaeModelofk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Modeloes", "Marca_Key", c => c.String(maxLength: 128));
            CreateIndex("dbo.Modeloes", "Marca_Key");
            AddForeignKey("dbo.Modeloes", "Marca_Key", "dbo.Marcas", "Key");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modeloes", "Marca_Key", "dbo.Marcas");
            DropIndex("dbo.Modeloes", new[] { "Marca_Key" });
            DropColumn("dbo.Modeloes", "Marca_Key");
        }
    }
}
