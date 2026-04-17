Option Strict On
Option Explicit On

Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports AHReportingSystem.Models

Namespace AHReportingSystem.Controllers

    ''' <summary>
    ''' Base controller for all AH Reporting System controllers.
    ''' Provides shared functionality: user manager access, common helpers, etc.
    ''' All controllers should inherit from this class.
    ''' </summary>
    <Authorize>
    Public MustInherit Class BaseController
        Inherits Controller

        ' =====================================================
        ' Identity managers (lazy-loaded via OWIN)
        ' =====================================================
        Private _userManager As ApplicationUserManager
        Private _signInManager As ApplicationSignInManager
        Private _roleManager As ApplicationRoleManager

        Public ReadOnly Property UserManager As ApplicationUserManager
            Get
                If _userManager Is Nothing Then
                    _userManager = HttpContext.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
                End If
                Return _userManager
            End Get
        End Property

        Public ReadOnly Property SignInManager As ApplicationSignInManager
            Get
                If _signInManager Is Nothing Then
                    _signInManager = HttpContext.GetOwinContext().Get(Of ApplicationSignInManager)()
                End If
                Return _signInManager
            End Get
        End Property

        Public ReadOnly Property RoleManager As ApplicationRoleManager
            Get
                If _roleManager Is Nothing Then
                    _roleManager = HttpContext.GetOwinContext().Get(Of ApplicationRoleManager)()
                End If
                Return _roleManager
            End Get
        End Property

        ' =====================================================
        ' Current user helper
        ' =====================================================
        Public ReadOnly Property CurrentUserId As String
            Get
                Return User.Identity.GetUserId()
            End Get
        End Property

        Public ReadOnly Property CurrentUserName As String
            Get
                Return User.Identity.GetUserName()
            End Get
        End Property

        ' =====================================================
        ' TempData helpers for success/error messages
        ' =====================================================
        Protected Sub SetSuccessMessage(message As String)
            TempData("SuccessMessage") = message
        End Sub

        Protected Sub SetErrorMessage(message As String)
            TempData("ErrorMessage") = message
        End Sub

        Protected Sub SetWarningMessage(message As String)
            TempData("WarningMessage") = message
        End Sub

        ' =====================================================
        ' Disposal
        ' =====================================================
        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                If _userManager IsNot Nothing Then
                    _userManager.Dispose()
                    _userManager = Nothing
                End If
                If _signInManager IsNot Nothing Then
                    _signInManager.Dispose()
                    _signInManager = Nothing
                End If
                If _roleManager IsNot Nothing Then
                    _roleManager.Dispose()
                    _roleManager = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

    End Class

End Namespace
