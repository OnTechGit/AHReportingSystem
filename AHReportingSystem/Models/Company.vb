Option Strict On
Option Explicit On

Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models

    ''' <summary>
    ''' A company in the Alternative Holdings consortium. Each uploads its own
    ''' General Ledger and maps its external chart of accounts to the system one.
    ''' </summary>
    Public Class Company

        <Key>
        Public Property CompanyId As Integer

        <Required>
        <StringLength(200)>
        <Display(Name:="Name")>
        Public Property Name As String

        ''' <summary>Short code used in reports and file uploads (unique, case-insensitive).</summary>
        <Required>
        <StringLength(20)>
        <Display(Name:="Code")>
        Public Property Code As String

        <Display(Name:="Active")>
        Public Property Active As Boolean = True

        <StringLength(256)>
        Public Property CreatedBy As String
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        <StringLength(256)>
        Public Property ModifiedBy As String
        Public Property ModifiedAt As DateTime?

        ''' <summary>Users explicitly granted access to this company (non-Admin only).</summary>
        Public Overridable Property UserCompanies As ICollection(Of UserCompany)

        ' Future phase 2 navigation:
        ' Public Overridable Property Accounts As ICollection(Of CompanyAccount)

    End Class

End Namespace
