Option Strict On
Option Explicit On

Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Models

    ''' <summary>
    ''' System chart of accounts. Master catalog shared by all AH companies.
    ''' IdCuenta is entered manually by the user (not identity).
    ''' </summary>
    Public Class SystemAccount

        ''' <summary>
        ''' Alphanumeric account ID (manually assigned, not identity).
        ''' Max 20 chars; letters, digits, dashes, underscores, spaces and dots allowed.
        ''' Stored upper-cased — see AccountsController normalization.
        ''' </summary>
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.None)>
        <StringLength(20)>
        <Display(Name:="Account ID")>
        Public Property IdCuenta As String

        <Required>
        <StringLength(200)>
        <Display(Name:="Account Name")>
        Public Property Cuenta As String

        <Required>
        <Display(Name:="Category")>
        Public Property CategoryId As Integer

        <Required>
        <Display(Name:="Sub-Category")>
        Public Property SubCategoryId As Integer

        <Required>
        <Display(Name:="Grouping")>
        Public Property GroupingId As Integer

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

        <ForeignKey("SubCategoryId")>
        Public Overridable Property SubCategory As AccountSubCategory

        <ForeignKey("GroupingId")>
        Public Overridable Property Grouping As AccountGrouping

    End Class

End Namespace
