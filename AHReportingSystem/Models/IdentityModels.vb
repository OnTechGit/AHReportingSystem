Option Strict On
Option Explicit On

Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

Namespace Models

    ' =========================================================
    ' ApplicationUser
    ' Extends the default IdentityUser with AH-specific fields
    ' =========================================================
    Public Class ApplicationUser
        Inherits IdentityUser

        ''' <summary>Full name of the user (First + Last)</summary>
        Public Property FullName As String

        ''' <summary>Date and time of last successful login</summary>
        Public Property LastLogin As DateTime?

        ''' <summary>Whether the user account is active (soft enable/disable)</summary>
        Public Property IsActive As Boolean = True

        ''' <summary>Date the user was created</summary>
        Public Property CreatedAt As DateTime = DateTime.UtcNow

        ''' <summary>
        ''' When True, the user is redirected to ChangePassword on next login
        ''' and cannot access the rest of the app until they change it.
        ''' Set by admin when creating / resetting a user's password.
        ''' </summary>
        Public Property MustChangePassword As Boolean = False

        ''' <summary>
        ''' Companies this user is restricted to (join table).
        ''' Admins ignore this — they always see all. Non-Admins with zero rows
        ''' are unrestricted (all companies).
        ''' </summary>
        Public Overridable Property UserCompanies As ICollection(Of UserCompany)

        ''' <summary>
        ''' Generate a ClaimsIdentity for this user.
        ''' This is used by OWIN cookie middleware.
        ''' </summary>
        Public Async Function GenerateUserIdentityAsync(manager As UserManager(Of ApplicationUser)) As Task(Of ClaimsIdentity)
            ' authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            Dim userIdentity = Await manager.CreateIdentityAsync(Me, DefaultAuthenticationTypes.ApplicationCookie)
            ' Add custom claims here if needed
            userIdentity.AddClaim(New Claim("FullName", If(FullName, String.Empty)))
            Return userIdentity
        End Function

    End Class

End Namespace
