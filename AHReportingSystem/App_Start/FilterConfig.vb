Option Strict On
Option Explicit On

Imports System.Web.Mvc

Public Class FilterConfig

    Public Shared Sub RegisterGlobalFilters(filters As GlobalFilterCollection)
        filters.Add(New HandleErrorAttribute())
        filters.Add(New ForcePasswordChangeFilter())
    End Sub

End Class
