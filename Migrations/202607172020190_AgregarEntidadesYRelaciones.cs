namespace proyecto.asociacionsolidarista.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarEntidadesYRelaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BitacoraEventos",
                c => new
                    {
                        IdEvento = c.Int(nullable: false, identity: true),
                        IdSinpe = c.Int(),
                        TablaDeEvento = c.String(nullable: false, maxLength: 20),
                        TipoDeEvento = c.String(nullable: false, maxLength: 20),
                        FechaDeEvento = c.DateTime(nullable: false),
                        DescripcionDeEvento = c.String(nullable: false),
                        DatosAnteriores = c.String(),
                        DatosPosteriores = c.String(),
                    })
                .PrimaryKey(t => t.IdEvento)
                .ForeignKey("dbo.PagoSINPE", t => t.IdSinpe)
                .Index(t => t.IdSinpe);
            
            CreateTable(
                "dbo.PagoSINPE",
                c => new
                    {
                        IdSinpe = c.Int(nullable: false, identity: true),
                        IdCaja = c.Int(nullable: false),
                        TelefonoOrigen = c.String(nullable: false, maxLength: 10),
                        NombreOrigen = c.String(nullable: false, maxLength: 200),
                        TelefonoDestinatario = c.String(nullable: false, maxLength: 10),
                        NombreDestinatario = c.String(nullable: false, maxLength: 200),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descripcion = c.String(maxLength: 50),
                        FechaDeRegistro = c.DateTime(nullable: false),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSinpe)
                .ForeignKey("dbo.CajaSINPE", t => t.IdCaja, cascadeDelete: true)
                .Index(t => t.IdCaja);
            
            CreateTable(
                "dbo.CajaSINPE",
                c => new
                    {
                        IdCaja = c.Int(nullable: false, identity: true),
                        IdComercio = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(nullable: false, maxLength: 150),
                        TelefonoSINPE = c.String(nullable: false, maxLength: 10),
                        FechaDeRegistro = c.DateTime(nullable: false),
                        FechaDeModificacion = c.DateTime(),
                        Estado = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCaja)
                .ForeignKey("dbo.Comercio", t => t.IdComercio, cascadeDelete: true)
                .Index(t => t.IdComercio);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BitacoraEventos", "IdSinpe", "dbo.PagoSINPE");
            DropForeignKey("dbo.PagoSINPE", "IdCaja", "dbo.CajaSINPE");
            DropForeignKey("dbo.CajaSINPE", "IdComercio", "dbo.Comercio");
            DropIndex("dbo.CajaSINPE", new[] { "IdComercio" });
            DropIndex("dbo.PagoSINPE", new[] { "IdCaja" });
            DropIndex("dbo.BitacoraEventos", new[] { "IdSinpe" });
            DropTable("dbo.CajaSINPE");
            DropTable("dbo.PagoSINPE");
            DropTable("dbo.BitacoraEventos");
        }
    }
}
