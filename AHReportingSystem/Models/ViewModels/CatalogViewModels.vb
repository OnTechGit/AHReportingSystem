Option Strict On
Option Explicit On

Imports System.ComponentModel.DataAnnotations

Namespace Models.ViewModels

    ' =========================================================
    ' Category
    ' =========================================================
    Public Class CategoryListItemViewModel
        Public Property CategoryId As Integer
        Public Property Name As String
        Public Property Active As Boolean
        Public Property SubCategoryCount As Integer
        Public Property AccountCount As Integer
    End Class

    Public Class CategoryFormViewModel
        Public Property CategoryId As Integer
        <Required>
        <StringLength(100)>
        <Display(Name:="Name")>
        Public Property Name As String
        <Display(Name:="Active")>
        Public Property Active As Boolean = True
    End Class

    ' =========================================================
    ' Sub-Category
    ' =========================================================
    Public Class SubCategoryListItemViewModel
        Public Property SubCategoryId As Integer
        Public Property Name As String
        Public Property CategoryId As Integer
        Public Property CategoryName As String
        Public Property Active As Boolean
        Public Property AccountCount As Integer
    End Class

    Public Class SubCategoryFormViewModel
        Public Property SubCategoryId As Integer
        <Required>
        <StringLength(100)>
        <Display(Name:="Name")>
        Public Property Name As String
        <Required>
        <Display(Name:="Category")>
        Public Property CategoryId As Integer
        <Display(Name:="Active")>
        Public Property Active As Boolean = True
    End Class

    ' =========================================================
    ' Grouping
    ' =========================================================
    Public Class GroupingListItemViewModel
        Public Property GroupingId As Integer
        Public Property Name As String
        Public Property Active As Boolean
        Public Property AccountCount As Integer
    End Class

    Public Class GroupingFormViewModel
        Public Property GroupingId As Integer
        <Required>
        <StringLength(100)>
        <Display(Name:="Name")>
        Public Property Name As String
        <Display(Name:="Active")>
        Public Property Active As Boolean = True
    End Class

End Namespace
