namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarcaeModelofk1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedidoes", "Modelo_Key", c => c.String(maxLength: 128));
            CreateIndex("dbo.Pedidoes", "UsuarioId");
            CreateIndex("dbo.Pedidoes", "TipoPecaId");
            CreateIndex("dbo.Pedidoes", "Modelo_Key");
            AddForeignKey("dbo.Pedidoes", "Modelo_Key", "dbo.Modeloes", "Key");
            AddForeignKey("dbo.Pedidoes", "TipoPecaId", "dbo.TipoPecas", "TipoPecaId", cascadeDelete: true);
            AddForeignKey("dbo.Pedidoes", "UsuarioId", "dbo.Usuarios", "UsuarioId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidoes", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Pedidoes", "TipoPecaId", "dbo.TipoPecas");
            DropForeignKey("dbo.Pedidoes", "Modelo_Key", "dbo.Modeloes");
            DropIndex("dbo.Pedidoes", new[] { "Modelo_Key" });
            DropIndex("dbo.Pedidoes", new[] { "TipoPecaId" });
            DropIndex("dbo.Pedidoes", new[] { "UsuarioId" });
            DropColumn("dbo.Pedidoes", "Modelo_Key");
        }
    }
}
