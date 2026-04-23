Option Strict On
Option Explicit On

Imports System.ComponentModel.DataAnnotations

Namespace Models.ViewModels

    ''' <summary>Row in the Accounts/Index DataTable.</summary>
    Public Class SystemAccountListItemViewModel
        Public Property IdCuenta As String
        Public Property Cuenta As String
        Public Property CategoryId As Integer
        Public Property CategoryName As String
        Public Property SubCategoryId As Integer
        Public Property SubCategoryName As String
        Public Property GroupingId As Integer
        Public Property GroupingName As String
        Public Property Active As Boolean
    End Class

    ''' <summary>Shared form model for Create / Edit.</summary>
    Public Class SystemAccountFormViewModel

        <Required>
        <StringLength(20)>
        <RegularExpression("^[A-Za-z0-9\-_. ]+$", ErrorMessage:="Only letters, digits, spaces, dots, '-' and '_' are allowed.")>
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

        ''' <summary>True when editing an existing account — IdCuenta becomes read-only in the view.</summary>
        Public Property IsEdit As Boolean

    End Class

End Namespace
