Option Strict On
Option Explicit On

Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports AHReportingSystem.Models
Imports AHReportingSystem.Resources

Namespace Controllers

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
            ViewData("ReturnUrl") = returnUrl
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
                ModelState.AddModelError(String.Empty, Strings.SignIn_InvalidCreds)
                Return View(model)
            End If

            Dim result = Await SignInManager.PasswordSignInAsync(
                user.UserName, model.Password, model.RememberMe, shouldLockout:=True)

            Select Case result
                Case SignInStatus.Success
                    user.LastLogin = DateTime.UtcNow
                    Await UserManager.UpdateAsync(user)
                    ' Forced password change on first login (or after admin reset)
                    If user.MustChangePassword Then
                        Return RedirectToAction("ChangePassword", New With {.forced = True})
                    End If
                    Return RedirectToLocal(returnUrl)

                Case SignInStatus.LockedOut
                    ModelState.AddModelError(String.Empty, Strings.SignIn_LockedOut)
                    Return View(model)

                Case Else
                    ModelState.AddModelError(String.Empty, Strings.SignIn_InvalidEmailOrPassword)
                    Return View(model)
            End Select
        End Function

        ' =====================================================
        ' GET: /Account/ChangePassword
        ' =====================================================
        Public Function ChangePassword(forced As Boolean?) As ActionResult
            ViewData("Forced") = forced.GetValueOrDefault(False)
            Return View(New ChangePasswordViewModel())
        End Function

        ' =====================================================
        ' POST: /Account/ChangePassword
        ' =====================================================
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function ChangePassword(model As ChangePasswordViewModel) As Task(Of ActionResult)
            Dim userId = CurrentUserId
            Dim user = Await UserManager.FindByIdAsync(userId)
            If user Is Nothing Then Return RedirectToAction("Login")

            ViewData("Forced") = user.MustChangePassword

            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Dim result = Await UserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword)
            If Not result.Succeeded Then
                For Each msg In result.Errors
                    ModelState.AddModelError(String.Empty, msg)
                Next
                Return View(model)
            End If

            user.MustChangePassword = False
            Await UserManager.UpdateAsync(user)

            SetSuccessMessage(Strings.ChangePassword_Success)
            Return RedirectToAction("Index", "Home")
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
                ModelState.AddModelError(String.Empty, Strings.LockScreen_IncorrectPassword)
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
