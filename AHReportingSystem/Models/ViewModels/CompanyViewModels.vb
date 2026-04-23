Option Strict On
Option Explicit On

Imports System.ComponentModel.DataAnnotations

Namespace Models.ViewModels

    Public Class CompanyListItemViewModel
        Public Property CompanyId As Integer
        Public Property Name As String
        Public Property Code As String
        Public Property Active As Boolean
    End Class

    Public Class CompanyFormViewModel
        Public Property CompanyId As Integer

        <Required>
        <StringLength(200)>
        <Display(Name:="Name")>
        Public Property Name As String

        <Required>
        <StringLength(20)>
        <RegularExpression("^[A-Za-z0-9\-_. ]+$", ErrorMessage:="Only letters, digits, spaces, dots, '-' and '_' are allowed.")>
        <Display(Name:="Code")>
        Public Property Code As String

        <Display(Name:="Active")>
        Public Property Active As Boolean = True
    End Class

End Namespace
