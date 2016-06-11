namespace Clinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        ID_END = c.Int(nullable: false, identity: true),
                        RUA = c.String(nullable: false, maxLength: 100, unicode: false),
                        CIDADE = c.String(nullable: false, maxLength: 100, unicode: false),
                        TELEFONE_PRIMARIO = c.String(nullable: false, maxLength: 100, unicode: false),
                        TELEFONE_SECUNDARIO = c.String(nullable: false, maxLength: 100, unicode: false),
                        ESTADO = c.String(nullable: false, maxLength: 100, unicode: false),
                        BAIRRO = c.String(nullable: false, maxLength: 100, unicode: false),
                        CEP = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ID_END);
            
            CreateTable(
                "dbo.Necessidade",
                c => new
                    {
                        ID_NECESSIDADE = c.Int(nullable: false, identity: true),
                        DESCRICAO = c.String(nullable: false, maxLength: 100, unicode: false),
                        TITULO = c.String(nullable: false, maxLength: 80, unicode: false),
                        PRIORIDADE = c.String(nullable: false, maxLength: 100, unicode: false),
                        DATA = c.DateTime(nullable: false),
                        ID_ONG = c.Int(nullable: false),                        
                    })
                .PrimaryKey(t => t.ID_NECESSIDADE)
                .ForeignKey("dbo.Ong", t => t.ID_ONG, cascadeDelete: true)
                .Index(t => t.ID_ONG);
            
            CreateTable(
                "dbo.Ong",
                c => new
                    {
                        ID_ONG = c.Int(nullable: false, identity: true),
                        CNPJ = c.String(nullable: false, maxLength: 100, unicode: false),
                        NOME = c.String(nullable: false, maxLength: 100, unicode: false),
                        SITE = c.String(nullable: false, maxLength: 100, unicode: false),
                        EMAIL = c.String(nullable: false, maxLength: 100),
                        ID_END = c.Int(nullable: false),
                        ID_USER = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID_ONG)
                .ForeignKey("dbo.Endereco", t => t.ID_END, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ID_USER, cascadeDelete: true)
                .Index(t => t.ID_END)
                .Index(t => t.ID_USER);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ong", "ID_USER", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Necessidade", "ID_ONG", "dbo.Ong");
            DropForeignKey("dbo.Ong", "ID_END", "dbo.Endereco");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Ong", new[] { "ID_USER" });
            DropIndex("dbo.Ong", new[] { "ID_END" });
            DropIndex("dbo.Necessidade", new[] { "ID_ONG" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Ong");
            DropTable("dbo.Necessidade");
            DropTable("dbo.Endereco");
        }
    }
}
