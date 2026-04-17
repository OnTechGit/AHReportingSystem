Option Strict On
Option Explicit On

Imports System.Web.Mvc
Imports System.Web.Optimization
Imports System.Web.Routing

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    ''' <summary>
    ''' Runs before every request — honours the user's culture cookie (en/es).
    ''' </summary>
    Protected Sub Application_BeginRequest()
        CultureHelper.ApplyFromCookie(HttpContext.Current)
    End Sub

    Protected Sub Application_Error(sender As Object, e As EventArgs)
        Dim ex As Exception = Server.GetLastError()
        If ex IsNot Nothing Then
            System.Diagnostics.Debug.WriteLine($"Application Error: {ex.Message}")
        End If
    End Sub

End Class
