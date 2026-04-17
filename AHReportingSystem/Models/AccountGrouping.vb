Option Strict On
Option Explicit On

Imports System.ComponentModel.DataAnnotations

Namespace Models

    ''' <summary>
    ''' Reporting grouping (e.g. "EMPLOYEE EXPENSES").
    ''' Independent from Category / Sub-Category hierarchy.
    ''' </summary>
    Public Class AccountGrouping

        <Key>
        Public Property GroupingId As Integer

        <Required>
        <StringLength(100)>
        <Display(Name:="Grouping")>
        Public Property Name As String

        <Display(Name:="Active")>
        Public Property Active As Boolean = True

        <StringLength(256)>
        Public Property CreatedBy As String
        Public Property CreatedAt As DateTime = DateTime.UtcNow
        <StringLength(256)>
        Public Property ModifiedBy As String
        Public Property ModifiedAt As DateTime?

        Public Overridable Property Accounts As ICollection(Of SystemAccount)

    End Class

End Namespace
