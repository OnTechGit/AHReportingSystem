Option Strict On
Option Explicit On

Imports System.Web.Mvc

Namespace AHReportingSystem.Controllers

    ''' <summary>
    ''' Home controller — Dashboard landing page after login.
    ''' </summary>
    Public Class HomeController
        Inherits BaseController

        ' GET: /Home/Index
        Public Function Index() As ActionResult
            Return View()
        End Function

    End Class

End Namespace
