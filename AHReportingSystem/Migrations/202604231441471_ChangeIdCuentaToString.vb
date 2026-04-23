Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ChangeIdCuentaToString
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropPrimaryKey("dbo.SystemAccounts")
            AlterColumn("dbo.SystemAccounts", "IdCuenta", Function(c) c.String(nullable := False, maxLength := 20))
            AddPrimaryKey("dbo.SystemAccounts", "IdCuenta")
        End Sub
        
        Public Overrides Sub Down()
            DropPrimaryKey("dbo.SystemAccounts")
            AlterColumn("dbo.SystemAccounts", "IdCuenta", Function(c) c.Int(nullable := False))
            AddPrimaryKey("dbo.SystemAccounts", "IdCuenta")
        End Sub
    End Class
End Namespace
