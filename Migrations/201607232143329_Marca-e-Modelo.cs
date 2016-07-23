namespace ProjectF2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarcaeModelo : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Marcas");
            DropPrimaryKey("dbo.Modeloes");
            AddColumn("dbo.Marcas", "Key", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Marcas", "FipeName", c => c.String());
            AddColumn("dbo.Marcas", "Name", c => c.String());
            AddColumn("dbo.Modeloes", "Key", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Modeloes", "Name", c => c.String());
            AddColumn("dbo.Modeloes", "FipeName", c => c.String());
            AlterColumn("dbo.Marcas", "MarcaId", c => c.Int(nullable: false));
            AlterColumn("dbo.Modeloes", "ModeloId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Marcas", "Key");
            AddPrimaryKey("dbo.Modeloes", "Key");
            DropColumn("dbo.Marcas", "NomeMarca");
            DropColumn("dbo.Modeloes", "NomeModelo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Modeloes", "NomeModelo", c => c.String());
            AddColumn("dbo.Marcas", "NomeMarca", c => c.String());
            DropPrimaryKey("dbo.Modeloes");
            DropPrimaryKey("dbo.Marcas");
            AlterColumn("dbo.Modeloes", "ModeloId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Marcas", "MarcaId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Modeloes", "FipeName");
            DropColumn("dbo.Modeloes", "Name");
            DropColumn("dbo.Modeloes", "Key");
            DropColumn("dbo.Marcas", "Name");
            DropColumn("dbo.Marcas", "FipeName");
            DropColumn("dbo.Marcas", "Key");
            AddPrimaryKey("dbo.Modeloes", "ModeloId");
            AddPrimaryKey("dbo.Marcas", "MarcaId");
        }
    }
}
