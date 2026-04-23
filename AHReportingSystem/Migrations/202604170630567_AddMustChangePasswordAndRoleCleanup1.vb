Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddMustChangePasswordAndRoleCleanup1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Companies",
                Function(c) New With
                    {
                        .CompanyId = c.Int(nullable := False, identity := True),
                        .Name = c.String(nullable := False, maxLength := 200),
                        .Code = c.String(nullable := False, maxLength := 20),
                        .Active = c.Boolean(nullable := False),
                        .CreatedBy = c.String(maxLength := 256),
                        .CreatedAt = c.DateTime(nullable := False),
                        .ModifiedBy = c.String(maxLength := 256),
                        .ModifiedAt = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.CompanyId) _
                .Index(Function(t) t.Code, unique := True, name := "IX_Company_Code")
            
        End Sub
        
        Public Overrides Sub Down()
            DropIndex("dbo.Companies", "IX_Company_Code")
            DropTable("dbo.Companies")
        End Sub
    End Class
End Namespace
