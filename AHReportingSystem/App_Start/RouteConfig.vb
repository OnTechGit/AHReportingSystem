Option Strict On
Option Explicit On

Imports System.Web.Mvc
Imports System.Web.Routing

Namespace AHReportingSystem

    Public Class RouteConfig

        Public Shared Sub RegisterRoutes(routes As RouteCollection)
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

            routes.MapRoute(
                name:="Default",
                url:="{controller}/{action}/{id}",
                defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional}
            )
        End Sub

    End Class

End Namespace
