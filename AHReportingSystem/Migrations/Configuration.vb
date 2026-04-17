Option Strict On
Option Explicit On

Imports System.Data.Entity.Migrations
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports AHReportingSystem.Models

Namespace Migrations

    ''' <summary>
    ''' EF Code-First migrations configuration + seed data.
    ''' Seeds the 3 system roles (Admin / Supervisor / User) and the initial
    ''' Admin user. Cleans up deprecated "Manager" / "Viewer" roles if they
    ''' exist and have no users assigned.
    ''' </summary>
    Friend NotInheritable Class Configuration
        Inherits DbMigrationsConfiguration(Of ApplicationDbContext)

        Public Sub New()
            AutomaticMigrationsEnabled = False
            ContextKey = "AHReportingSystem.Models.ApplicationDbContext"
        End Sub

        Protected Overrides Sub Seed(context As ApplicationDbContext)
            SeedRoles(context)
            CleanupDeprecatedRoles(context)
            SeedAdminUser(context)
            context.SaveChanges()
        End Sub

        Private Sub SeedRoles(context As ApplicationDbContext)
            Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(context))
            Dim roles = {"Admin", "Supervisor", "User"}
            For Each roleName In roles
                If Not roleManager.RoleExists(roleName) Then
                    roleManager.Create(New IdentityRole(roleName))
                End If
            Next
        End Sub

        Private Sub CleanupDeprecatedRoles(context As ApplicationDbContext)
            Dim roleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(context))
            For Each oldRole In {"Manager", "Viewer"}
                Dim role = roleManager.FindByName(oldRole)
                If role IsNot Nothing AndAlso role.Users.Count = 0 Then
                    roleManager.Delete(role)
                End If
            Next
        End Sub

        Private Sub SeedAdminUser(context As ApplicationDbContext)
            Const adminEmail As String = "amelo@ontech.la"
            Const adminPassword As String = "Nena3004*"
            Const adminFullName As String = "Alexander Melo"

            Dim userManager = New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(context))

            Dim user = userManager.FindByEmail(adminEmail)
            If user Is Nothing Then
                user = New ApplicationUser() With {
                    .UserName = adminEmail,
                    .Email = adminEmail,
                    .EmailConfirmed = True,
                    .FullName = adminFullName,
                    .IsActive = True,
                    .MustChangePassword = False,
                    .CreatedAt = DateTime.UtcNow
                }
                Dim result = userManager.Create(user, adminPassword)
                If result.Succeeded Then
                    userManager.AddToRole(user.Id, "Admin")
                End If
            Else
                If Not userManager.IsInRole(user.Id, "Admin") Then
                    userManager.AddToRole(user.Id, "Admin")
                End If
            End If
        End Sub

    End Class

End Namespace
