Option Strict On
Option Explicit On

Imports System.Web.Mvc

Namespace AHReportingSystem

    Public Class FilterConfig

        Public Shared Sub RegisterGlobalFilters(filters As GlobalFilterCollection)
            filters.Add(New HandleErrorAttribute())
        End Sub

    End Class

End Namespace
