Option Strict On
Option Explicit On

Imports System.ComponentModel.DataAnnotations

Namespace Models

    ''' <summary>
    ''' Top-level account classification (e.g. Expense, Revenue, Asset).
    ''' </summary>
    Public Class AccountCategory

        <Key>
        Public Property CategoryId As Integer

        <Required>
        <StringLength(100)>
        <Display(Name:="Category")>
        Public Property Name As String

        <Display(Name:="Active")>
        Public Property Active As Boolean = True

        <StringLength(256)>
        Public Property CreatedBy As String
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        <StringLength(256)>
        Public Property ModifiedBy As String
        Public Property ModifiedAt As DateTime?

        Public Overridable Property SubCategories As ICollection(Of AccountSubCategory)
        Public Overridable Property Accounts As ICollection(Of SystemAccount)

    End Class

End Namespace
