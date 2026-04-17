Option Strict On
Option Explicit On

Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports AHReportingSystem.Models

Namespace AHReportingSystem.Controllers

    ''' <summary>
    ''' Handles authentication: Login, Logout, LockScreen.
    ''' </summary>
    Public Class AccountController
        Inherits BaseController

        ' =====================================================
        ' GET: /Account/Login
        ' =====================================================
        <AllowAnonymous>
        Public Function Login(returnUrl As String) As ActionResult
            ViewBag.ReturnUrl = returnUrl
            Return View()
        End Function

        ' =====================================================
        ' POST: /Account/Login
        ' =====================================================
        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Async Function Login(model As LoginViewModel, returnUrl As String) As Task(Of ActionResult)
            If Not ModelState.IsValid Then
                Return View(model)
            End If

            ' Check if user exists and is active
            Dim user = Await UserManager.FindByEmailAsync(model.Email)
            If user Is Nothing OrElse Not user.IsActive Then
                ModelState.AddModelError(String.Empty, "Invalid credentials or account is inactive.")
                Return View(model)
            End If

            Dim result = Await SignInManager.PasswordSignInAsync(
                user.UserName, model.Password, model.RememberMe, shouldLockout:=True)

            Select Case result
                Case SignInStatus.Success
                    ' Update last login timestamp
                    user.LastLogin = DateTime.UtcNow
                    Await UserManager.UpdateAsync(user)
                    Return RedirectToLocal(returnUrl)

                Case SignInStatus.LockedOut
                    ModelState.AddModelError(String.Empty,
                        "Your account has been locked. Please try again in 5 minutes.")
                    Return View(model)

                Case Else
                    ModelState.AddModelError(String.Empty, "Invalid email or password.")
                    Return View(model)
            End Select
        End Function

        ' =====================================================
        ' POST: /Account/Logout
        ' =====================================================
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function LogOut() As ActionResult
            HttpContext.GetOwinContext().Authentication.SignOut(
                DefaultAuthenticationTypes.ApplicationCookie)
            Return RedirectToAction("Login", "Account")
        End Function

        ' =====================================================
        ' GET: /Account/LockScreen
        ' =====================================================
        Public Function LockScreen() As ActionResult
            Dim model = New LockScreenViewModel() With {
                .UserEmail = CurrentUserName,
                .ReturnUrl = Request.UrlReferrer?.PathAndQuery
            }

            ' Get user's full name for display
            Dim userId = CurrentUserId
            If Not String.IsNullOrEmpty(userId) Then
                Dim user = UserManager.FindById(userId)
                If user IsNot Nothing Then
                    model.UserFullName = If(user.FullName, user.UserName)
                    model.UserEmail = user.Email
                End If
            End If

            ' Store original URL in session
            If Not String.IsNullOrEmpty(model.ReturnUrl) Then
                Session("LockScreenReturnUrl") = model.ReturnUrl
            End If

            Return View(model)
        End Function

        ' =====================================================
        ' POST: /Account/LockScreen
        ' =====================================================
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function LockScreen(model As LockScreenViewModel) As Task(Of ActionResult)
            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Dim userId = CurrentUserId
            If String.IsNullOrEmpty(userId) Then
                Return RedirectToAction("Login")
            End If

            Dim user = Await UserManager.FindByIdAsync(userId)
            If user Is Nothing Then
                Return RedirectToAction("Login")
            End If

            Dim isValid = Await UserManager.CheckPasswordAsync(user, model.Password)
            If isValid Then
                ' Restore the original URL
                Dim returnUrl = TryCast(Session("LockScreenReturnUrl"), String)
                Session.Remove("LockScreenReturnUrl")
                Return RedirectToLocal(If(returnUrl, "~/"))
            Else
                ModelState.AddModelError(String.Empty, "Incorrect password. Please try again.")
                model.UserFullName = If(user.FullName, user.UserName)
                model.UserEmail = user.Email
                Return View(model)
            End If
        End Function

        ' =====================================================
        ' Private helpers
        ' =====================================================
        Private Function RedirectToLocal(returnUrl As String) As ActionResult
            If Url.IsLocalUrl(returnUrl) Then
                Return Redirect(returnUrl)
            End If
            Return RedirectToAction("Index", "Home")
        End Function

    End Class

End Namespace
