namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pedidoes", "Usuario_UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Pedidoes", new[] { "Usuario_UsuarioId" });
            DropColumn("dbo.Pedidoes", "UserId");
            RenameColumn(table: "dbo.Pedidoes", name: "Usuario_UsuarioId", newName: "UserId");
            DropPrimaryKey("dbo.Usuarios");
            AlterColumn("dbo.Pedidoes", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Pedidoes", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Usuarios", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Usuarios", "UserId");
            CreateIndex("dbo.Pedidoes", "UserId");
            AddForeignKey("dbo.Pedidoes", "UserId", "dbo.Usuarios", "UserId");
            DropColumn("dbo.Usuarios", "UsuarioId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "UsuarioId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Pedidoes", "UserId", "dbo.Usuarios");
            DropIndex("dbo.Pedidoes", new[] { "UserId" });
            DropPrimaryKey("dbo.Usuarios");
            AlterColumn("dbo.Usuarios", "UserId", c => c.String());
            AlterColumn("dbo.Pedidoes", "UserId", c => c.Int());
            AlterColumn("dbo.Pedidoes", "UserId", c => c.String());
            AddPrimaryKey("dbo.Usuarios", "UsuarioId");
            RenameColumn(table: "dbo.Pedidoes", name: "UserId", newName: "Usuario_UsuarioId");
            AddColumn("dbo.Pedidoes", "UserId", c => c.String());
            CreateIndex("dbo.Pedidoes", "Usuario_UsuarioId");
            AddForeignKey("dbo.Pedidoes", "Usuario_UsuarioId", "dbo.Usuarios", "UsuarioId");
        }
    }
}
