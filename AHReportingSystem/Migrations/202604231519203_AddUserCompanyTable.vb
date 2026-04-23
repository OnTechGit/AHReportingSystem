Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddUserCompanyTable
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.UserCompanies",
                Function(c) New With
                    {
                        .UserId = c.String(nullable := False, maxLength := 128),
                        .CompanyId = c.Int(nullable := False),
                        .GrantedAt = c.DateTime(nullable := False),
                        .GrantedBy = c.String(maxLength := 256)
                    }) _
                .PrimaryKey(Function(t) New With { t.UserId, t.CompanyId }) _
                .ForeignKey("dbo.Companies", Function(t) t.CompanyId, cascadeDelete := True) _
                .ForeignKey("dbo.AspNetUsers", Function(t) t.UserId, cascadeDelete := True) _
                .Index(Function(t) t.UserId) _
                .Index(Function(t) t.CompanyId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.UserCompanies", "UserId", "dbo.AspNetUsers")
            DropForeignKey("dbo.UserCompanies", "CompanyId", "dbo.Companies")
            DropIndex("dbo.UserCompanies", New String() { "CompanyId" })
            DropIndex("dbo.UserCompanies", New String() { "UserId" })
            DropTable("dbo.UserCompanies")
        End Sub
    End Class
End Namespace
