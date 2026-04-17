Option Strict On
Option Explicit On

Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports Microsoft.AspNet.Identity
Imports AHReportingSystem.Models
Imports AHReportingSystem.Models.ViewModels
Imports AHReportingSystem.Resources

Namespace Controllers

    ''' <summary>
    ''' Admin-only CRUD for application users.
    ''' Single role per user; soft-delete via IsActive.
    ''' </summary>
    <Authorize(Roles:="Admin")>
    Public Class UsersController
        Inherits BaseController

        ' ========== GET: /Users =============================
        Public Function Index() As ActionResult
            Dim list As New List(Of UserListItemViewModel)
            Dim currentId = CurrentUserId

            For Each u In UserManager.Users.OrderBy(Function(x) x.FullName).ToList()
                Dim roles = UserManager.GetRoles(u.Id)
                list.Add(New UserListItemViewModel With {
                    .Id = u.Id,
                    .Email = u.Email,
                    .FullName = u.FullName,
                    .Role = If(roles.FirstOrDefault(), ""),
                    .IsActive = u.IsActive,
                    .LastLogin = u.LastLogin,
                    .IsSelf = (u.Id = currentId)
                })
            Next

            Return View(list)
        End Function

        ' ========== GET: /Users/Create ======================
        Public Function Create() As ActionResult
            PopulateRoles()
            Return View(New CreateUserViewModel() With {.Role = "User"})
        End Function

        ' ========== POST: /Users/Create =====================
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Create(model As CreateUserViewModel) As Task(Of ActionResult)
            If Not ModelState.IsValid Then
                PopulateRoles()
                Return View(model)
            End If

            ' Email uniqueness
            If Await UserManager.FindByEmailAsync(model.Email) IsNot Nothing Then
                ModelState.AddModelError("Email", Strings.Users_EmailTaken)
                PopulateRoles()
                Return View(model)
            End If

            Dim user As New ApplicationUser With {
                .UserName = model.Email,
                .Email = model.Email,
                .EmailConfirmed = True,
                .FullName = model.FullName,
                .IsActive = True,
                .MustChangePassword = True,
                .CreatedAt = DateTime.UtcNow
            }

            Dim result = Await UserManager.CreateAsync(user, model.InitialPassword)
            If Not result.Succeeded Then
                For Each msg In result.Errors
                    ModelState.AddModelError(String.Empty, msg)
                Next
                PopulateRoles()
                Return View(model)
            End If

            Await UserManager.AddToRoleAsync(user.Id, model.Role)
            SetSuccessMessage(Strings.Users_Created)
            Return RedirectToAction("Index")
        End Function

        ' ========== GET: /Users/Edit/{id} ===================
        Public Async Function Edit(id As String) As Task(Of ActionResult)
            If String.IsNullOrEmpty(id) Then Return HttpNotFound()
            Dim user = Await UserManager.FindByIdAsync(id)
            If user Is Nothing Then Return HttpNotFound()

            Dim roles = Await UserManager.GetRolesAsync(id)
            Dim model As New EditUserViewModel With {
                .Id = user.Id,
                .Email = user.Email,
                .FullName = user.FullName,
                .Role = If(roles.FirstOrDefault(), "User"),
                .IsActive = user.IsActive
            }
            PopulateRoles()
            Return View(model)
        End Function

        ' ========== POST: /Users/Edit =======================
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(model As EditUserViewModel) As Task(Of ActionResult)
            If Not ModelState.IsValid Then
                PopulateRoles()
                Return View(model)
            End If

            Dim user = Await UserManager.FindByIdAsync(model.Id)
            If user Is Nothing Then Return HttpNotFound()

            Dim isSelf = (user.Id = CurrentUserId)

            ' Email uniqueness (another user with same email)
            Dim existing = Await UserManager.FindByEmailAsync(model.Email)
            If existing IsNot Nothing AndAlso existing.Id <> user.Id Then
                ModelState.AddModelError("Email", Strings.Users_EmailTaken)
                PopulateRoles()
                Return View(model)
            End If

            ' Self-protection: cannot deactivate self or change own role
            If isSelf AndAlso Not model.IsActive Then
                ModelState.AddModelError(String.Empty, Strings.Users_CannotDeactivateSelf)
                PopulateRoles()
                Return View(model)
            End If
            Dim currentRoles = Await UserManager.GetRolesAsync(user.Id)
            Dim currentRole = If(currentRoles.FirstOrDefault(), "")
            If isSelf AndAlso model.Role <> currentRole Then
                ModelState.AddModelError(String.Empty, Strings.Users_CannotChangeSelfRole)
                PopulateRoles()
                Return View(model)
            End If

            user.FullName = model.FullName
            user.Email = model.Email
            user.UserName = model.Email
            user.IsActive = model.IsActive

            Dim updateResult = Await UserManager.UpdateAsync(user)
            If Not updateResult.Succeeded Then
                For Each em In updateResult.Errors
                    ModelState.AddModelError(String.Empty, em)
                Next
                PopulateRoles()
                Return View(model)
            End If

            ' Role sync (single-role model)
            If model.Role <> currentRole Then
                If Not String.IsNullOrEmpty(currentRole) Then
                    Await UserManager.RemoveFromRoleAsync(user.Id, currentRole)
                End If
                Await UserManager.AddToRoleAsync(user.Id, model.Role)
            End If

            SetSuccessMessage(Strings.Users_Updated)
            Return RedirectToAction("Index")
        End Function

        ' ========== POST: /Users/ToggleActive ===============
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function ToggleActive(id As String) As Task(Of ActionResult)
            If String.IsNullOrEmpty(id) Then Return HttpNotFound()
            Dim user = Await UserManager.FindByIdAsync(id)
            If user Is Nothing Then Return HttpNotFound()

            If user.Id = CurrentUserId AndAlso user.IsActive Then
                SetErrorMessage(Strings.Users_CannotDeactivateSelf)
                Return RedirectToAction("Index")
            End If

            user.IsActive = Not user.IsActive
            Await UserManager.UpdateAsync(user)
            SetSuccessMessage(If(user.IsActive, Strings.Users_Activated, Strings.Users_Deactivated))
            Return RedirectToAction("Index")
        End Function

        ' ========== POST: /Users/ResetPassword ==============
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function ResetPassword(model As ResetPasswordViewModel) As Task(Of ActionResult)
            Dim user = Await UserManager.FindByIdAsync(model.Id)
            If user Is Nothing Then Return HttpNotFound()

            If Not ModelState.IsValid Then
                SetErrorMessage(String.Join("; ", ModelState.Values.
                    SelectMany(Function(v) v.Errors).
                    Select(Function(e) e.ErrorMessage)))
                Return RedirectToAction("Edit", New With {.id = model.Id})
            End If

            Dim token = Await UserManager.GeneratePasswordResetTokenAsync(user.Id)
            Dim result = Await UserManager.ResetPasswordAsync(user.Id, token, model.NewPassword)
            If Not result.Succeeded Then
                SetErrorMessage(String.Join("; ", result.Errors))
                Return RedirectToAction("Edit", New With {.id = model.Id})
            End If

            user.MustChangePassword = True
            Await UserManager.UpdateAsync(user)

            SetSuccessMessage(Strings.Users_PasswordReset)
            Return RedirectToAction("Index")
        End Function

        ' ================================================
        ' Private helpers
        ' ================================================
        Private Sub PopulateRoles()
            ViewData("Roles") = New List(Of SelectListItem) From {
                New SelectListItem With {.Value = "Admin", .Text = Strings.Role_Admin},
                New SelectListItem With {.Value = "Supervisor", .Text = Strings.Role_Supervisor},
                New SelectListItem With {.Value = "User", .Text = Strings.Role_User}
            }
        End Sub

    End Class

End Namespace
