Option Strict On
Option Explicit On

Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.Cookies
Imports Owin
Imports AHReportingSystem.Models

Namespace AHReportingSystem

    Partial Public Class Startup

        ' Configure cookie-based authentication for the application.
        ' Called from Startup.vb -> Configuration()
        Public Sub ConfigureAuth(app As IAppBuilder)

            ' Configure the db context, user manager and role manager
            ' to use a single instance per request
            app.CreatePerOwinContext(AddressOf ApplicationDbContext.Create)
            app.CreatePerOwinContext(Of ApplicationUserManager)(AddressOf ApplicationUserManager.Create)
            app.CreatePerOwinContext(Of ApplicationSignInManager)(AddressOf ApplicationSignInManager.Create)
            app.CreatePerOwinContext(Of ApplicationRoleManager)(AddressOf ApplicationRoleManager.Create)

            ' Enable cookie-based authentication
            app.UseCookieAuthentication(New CookieAuthenticationOptions() With {
                .AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                .LoginPath = New PathString("/Account/Login"),
                .Provider = New CookieAuthenticationProvider() With {
                    .OnValidateIdentity = SecurityStampValidator.OnValidateIdentity(Of ApplicationUserManager, ApplicationUser)(
                        validateInterval:=TimeSpan.FromMinutes(30),
                        regenerateIdentity:=Function(manager, user) user.GenerateUserIdentityAsync(manager))
                },
                .SlidingExpiration = True,
                .ExpireTimeSpan = TimeSpan.FromHours(8)
            })

        End Sub

    End Class

End Namespace
