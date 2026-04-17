Option Strict On
Option Explicit On

Imports System.Web.Http
Imports System.Web.Mvc
Imports System.Web.Optimization
Imports System.Web.Routing

Namespace AHReportingSystem

    Public Class MvcApplication
        Inherits System.Web.HttpApplication

        Protected Sub Application_Start()
            AreaRegistration.RegisterAllAreas()
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
            RouteConfig.RegisterRoutes(RouteTable.Routes)
            BundleConfig.RegisterBundles(BundleTable.Bundles)
        End Sub

        Protected Sub Application_Error(sender As Object, e As EventArgs)
            Dim ex As Exception = Server.GetLastError()
            If ex IsNot Nothing Then
                ' Log the error (logging framework can be added later)
                System.Diagnostics.Debug.WriteLine($"Application Error: {ex.Message}")
            End If
        End Sub

    End Class

End Namespace
