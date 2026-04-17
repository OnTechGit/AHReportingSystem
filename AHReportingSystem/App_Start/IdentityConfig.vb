Option Strict On
Option Explicit On

Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports AHReportingSystem.Models

    ' =========================================================
    ' ApplicationUserManager
    ' Manages user creation, password validation, etc.
    ' =========================================================
    Public Class ApplicationUserManager
        Inherits UserManager(Of ApplicationUser)

        Public Sub New(store As IUserStore(Of ApplicationUser))
            MyBase.New(store)
        End Sub

        Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationUserManager),
                                      context As IOwinContext) As ApplicationUserManager

            Dim manager = New ApplicationUserManager(
                New UserStore(Of ApplicationUser)(context.Get(Of ApplicationDbContext)()))

            ' Password strength requirements
            manager.PasswordValidator = New PasswordValidator With {
                .RequiredLength = 8,
                .RequireNonLetterOrDigit = False,
                .RequireDigit = True,
                .RequireLowercase = True,
                .RequireUppercase = True
            }

            ' User validation
            manager.UserValidator = New UserValidator(Of ApplicationUser)(manager) With {
                .AllowOnlyAlphanumericUserNames = False,
                .RequireUniqueEmail = True
            }

            ' Account lockout settings
            manager.UserLockoutEnabledByDefault = True
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5)
            manager.MaxFailedAccessAttemptsBeforeLockout = 5

            Return manager
        End Function

    End Class

    ' =========================================================
    ' ApplicationSignInManager
    ' Handles sign-in logic (password, 2FA, etc.)
    ' =========================================================
    Public Class ApplicationSignInManager
        Inherits SignInManager(Of ApplicationUser, String)

        Public Sub New(userManager As ApplicationUserManager,
                       authenticationManager As IAuthenticationManager)
            MyBase.New(userManager, authenticationManager)
        End Sub

        Public Overrides Function CreateUserIdentityAsync(user As ApplicationUser) As Task(Of ClaimsIdentity)
            Return user.GenerateUserIdentityAsync(DirectCast(UserManager, ApplicationUserManager))
        End Function

        Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationSignInManager),
                                      context As IOwinContext) As ApplicationSignInManager
            Return New ApplicationSignInManager(
                context.GetUserManager(Of ApplicationUserManager)(),
                context.Authentication)
        End Function

    End Class

    ' =========================================================
    ' ApplicationRoleManager
    ' Manages roles in the system
    ' =========================================================
    Public Class ApplicationRoleManager
        Inherits RoleManager(Of IdentityRole)

        Public Sub New(store As IRoleStore(Of IdentityRole, String))
            MyBase.New(store)
        End Sub

        Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationRoleManager),
                                      context As IOwinContext) As ApplicationRoleManager
            Return New ApplicationRoleManager(
                New RoleStore(Of IdentityRole)(context.Get(Of ApplicationDbContext)()))
        End Function

End Class
