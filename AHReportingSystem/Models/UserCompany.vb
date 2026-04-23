Option Strict On
Option Explicit On

Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models

    ''' <summary>
    ''' Join table restricting a Supervisor / User to specific companies.
    ''' Admins always have universal access by role (they don't need rows here).
    ''' If a non-Admin user has ZERO rows, they are unrestricted (can see all).
    ''' </summary>
    Public Class UserCompany

        <Key, Column(Order:=0)>
        <StringLength(128)>
        Public Property UserId As String

        <Key, Column(Order:=1)>
        Public Property CompanyId As Integer

        Public Property GrantedAt As DateTime = DateTime.UtcNow

        <StringLength(256)>
        Public Property GrantedBy As String

        <ForeignKey("UserId")>
        Public Overridable Property User As ApplicationUser

        <ForeignKey("CompanyId")>
        Public Overridable Property Company As Company

    End Class

End Namespace
