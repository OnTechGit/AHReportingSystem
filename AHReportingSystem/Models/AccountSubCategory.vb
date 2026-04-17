Option Strict On
Option Explicit On

Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models

    ''' <summary>
    ''' Secondary account classification nested under an AccountCategory.
    ''' E.g. "ADMINEXPENSES" under "Expense".
    ''' </summary>
    Public Class AccountSubCategory

        <Key>
        Public Property SubCategoryId As Integer

        <Required>
        <StringLength(100)>
        <Display(Name:="Sub-Category")>
        Public Property Name As String

        <Required>
        <Display(Name:="Category")>
        Public Property CategoryId As Integer

        <Display(Name:="Active")>
        Public Property Active As Boolean = True

        <StringLength(256)>
        Public Property CreatedBy As String
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        <StringLength(256)>
        Public Property ModifiedBy As String
        Public Property ModifiedAt As DateTime?

        <ForeignKey("CategoryId")>
        Public Overridable Property Category As AccountCategory

        Public Overridable Property Accounts As ICollection(Of SystemAccount)

    End Class

End Namespace
