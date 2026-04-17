Option Strict On
Option Explicit On

Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports AHReportingSystem.Models

''' <summary>
''' Global filter: if the logged-in user has MustChangePassword = True,
''' redirect every request to /Account/ChangePassword until they change it.
''' Exempts the ChangePassword and LogOut actions themselves and the
''' Culture switcher (so the user can switch language before changing).
''' </summary>
Public Class ForcePasswordChangeFilter
    Implements IActionFilter

    Public Sub OnActionExecuting(filterContext As ActionExecutingContext) _
        Implements IActionFilter.OnActionExecuting

        Dim user = filterContext.HttpContext.User
        If user Is Nothing OrElse Not user.Identity.IsAuthenticated Then Return

        Dim controller = CStr(filterContext.RouteData.Values("controller"))
        Dim action = CStr(filterContext.RouteData.Values("action"))

        ' Never loop on the exempt endpoints.
        If controller = "Account" AndAlso
           (action = "ChangePassword" OrElse action = "LogOut" OrElse action = "LockScreen") Then Return
        If controller = "Culture" Then Return

        Dim userManager = filterContext.HttpContext _
            .GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        If userManager Is Nothing Then Return

        Dim appUser = userManager.FindById(user.Identity.GetUserId())
        If appUser IsNot Nothing AndAlso appUser.MustChangePassword Then
            filterContext.Result = New RedirectToRouteResult(
                New System.Web.Routing.RouteValueDictionary(
                    New With {.controller = "Account", .action = "ChangePassword", .forced = True}))
        End If
    End Sub

    Public Sub OnActionExecuted(filterContext As ActionExecutedContext) _
        Implements IActionFilter.OnActionExecuted
        ' no-op
    End Sub

End Class
