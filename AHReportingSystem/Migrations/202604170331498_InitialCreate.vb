Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class InitialCreate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.AccountCategories",
                Function(c) New With
                    {
                        .CategoryId = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 100),
                        .Active = c.Boolean(nullable := False),
                        .CreatedBy = c.String(maxLength := 256),
                        .CreatedAt = c.DateTime(nullable := False),
                        .ModifiedBy = c.String(maxLength := 256),
                        .ModifiedAt = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.CategoryId)
            
            CreateTable(
                "dbo.SystemAccounts",
                Function(c) New With
                    {
                        .IdCuenta = c.Int(nullable := False),
                        .Cuenta = c.String(nullable := False, maxLength := 200),
                        .CategoryId = c.Int(nullable := False),
                        .SubCategoryId = c.Int(nullable := False),
                        .GroupingId = c.Int(nullable := False),
                        .Active = c.Boolean(nullable := False),
                        .CreatedBy = c.String(maxLength := 256),
                        .CreatedAt = c.DateTime(nullable := False),
                        .ModifiedBy = c.String(maxLength := 256),
                        .ModifiedAt = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.IdCuenta) _
                .ForeignKey("dbo.AccountCategories", Function(t) t.CategoryId) _
                .ForeignKey("dbo.AccountGroupings", Function(t) t.GroupingId) _
                .ForeignKey("dbo.AccountSubCategories", Function(t) t.SubCategoryId) _
                .Index(Function(t) t.CategoryId) _
                .Index(Function(t) t.SubCategoryId) _
                .Index(Function(t) t.GroupingId)
            
            CreateTable(
                "dbo.AccountGroupings",
                Function(c) New With
                    {
                        .GroupingId = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 100),
                        .Active = c.Boolean(nullable := False),
                        .CreatedBy = c.String(maxLength := 256),
                        .CreatedAt = c.DateTime(nullable := False),
                        .ModifiedBy = c.String(maxLength := 256),
                        .ModifiedAt = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.GroupingId)
            
            CreateTable(
                "dbo.AccountSubCategories",
                Function(c) New With
                    {
                        .SubCategoryId = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 100),
                        .CategoryId = c.Int(nullable := False),
                        .Active = c.Boolean(nullable := False),
                        .CreatedBy = c.String(maxLength := 256),
                        .CreatedAt = c.DateTime(nullable := False),
                        .ModifiedBy = c.String(maxLength := 256),
                        .ModifiedAt = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.SubCategoryId) _
                .ForeignKey("dbo.AccountCategories", Function(t) t.CategoryId) _
                .Index(Function(t) t.CategoryId)
            
            CreateTable(
                "dbo.AspNetRoles",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 128),
                        .Name = c.String(nullable := False, maxLength := 256)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .Index(Function(t) t.Name, unique := True, name := "RoleNameIndex")
            
            CreateTable(
                "dbo.AspNetUserRoles",
                Function(c) New With
                    {
                        .UserId = c.String(nullable := False, maxLength := 128),
                        .RoleId = c.String(nullable := False, maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) New With { t.UserId, t.RoleId }) _
                .ForeignKey("dbo.AspNetRoles", Function(t) t.RoleId, cascadeDelete := True) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId, cascadeDelete := True) _
                .Index(Function(t) t.UserId) _
                .Index(Function(t) t.RoleId)
            
            CreateTable(
                "dbo.AspNetUsers",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 128),
                        .FullName = c.String(),
                        .LastLogin = c.DateTime(),
                        .IsActive = c.Boolean(nullable := False),
                        .CreatedAt = c.DateTime(nullable := False),
                        .Email = c.String(maxLength := 256),
                        .EmailConfirmed = c.Boolean(nullable := False),
                        .PasswordHash = c.String(),
                        .SecurityStamp = c.String(),
                        .PhoneNumber = c.String(),
                        .PhoneNumberConfirmed = c.Boolean(nullable := False),
                        .TwoFactorEnabled = c.Boolean(nullable := False),
                        .LockoutEndDateUtc = c.DateTime(),
                        .LockoutEnabled = c.Boolean(nullable := False),
                        .AccessFailedCount = c.Int(nullable := False),
                        .UserName = c.String(nullable := False, maxLength := 256)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .Index(Function(t) t.UserName, unique := True, name := "UserNameIndex")
            
            CreateTable(
                "dbo.AspNetUserClaims",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .UserId = c.String(nullable := False, maxLength := 128),
                        .ClaimType = c.String(),
                        .ClaimValue = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId, cascadeDelete := True) _
                .Index(Function(t) t.UserId)
            
            CreateTable(
                "dbo.AspNetUserLogins",
                Function(c) New With
                    {
                        .LoginProvider = c.String(nullable := False, maxLength := 128),
                        .ProviderKey = c.String(nullable := False, maxLength := 128),
                        .UserId = c.String(nullable := False, maxLength := 128)
                    }) _
                .PrimaryKey(Function(t) New With { t.LoginProvider, t.ProviderKey, t.UserId }) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId, cascadeDelete := True) _
                .Index(Function(t) t.UserId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles")
            DropForeignKey("dbo.SystemAccounts", "SubCategoryId", "dbo.AccountSubCategories")
            DropForeignKey("dbo.AccountSubCategories", "CategoryId", "dbo.AccountCategories")
            DropForeignKey("dbo.SystemAccounts", "GroupingId", "dbo.AccountGroupings")
            DropForeignKey("dbo.SystemAccounts", "CategoryId", "dbo.AccountCategories")
            DropIndex("dbo.AspNetUserLogins", New String() { "UserId" })
            DropIndex("dbo.AspNetUserClaims", New String() { "UserId" })
            DropIndex("dbo.AspNetUsers", "UserNameIndex")
            DropIndex("dbo.AspNetUserRoles", New String() { "RoleId" })
            DropIndex("dbo.AspNetUserRoles", New String() { "UserId" })
            DropIndex("dbo.AspNetRoles", "RoleNameIndex")
            DropIndex("dbo.AccountSubCategories", New String() { "CategoryId" })
            DropIndex("dbo.SystemAccounts", New String() { "GroupingId" })
            DropIndex("dbo.SystemAccounts", New String() { "SubCategoryId" })
            DropIndex("dbo.SystemAccounts", New String() { "CategoryId" })
            DropTable("dbo.AspNetUserLogins")
            DropTable("dbo.AspNetUserClaims")
            DropTable("dbo.AspNetUsers")
            DropTable("dbo.AspNetUserRoles")
            DropTable("dbo.AspNetRoles")
            DropTable("dbo.AccountSubCategories")
            DropTable("dbo.AccountGroupings")
            DropTable("dbo.SystemAccounts")
            DropTable("dbo.AccountCategories")
        End Sub
    End Class
End Namespace
