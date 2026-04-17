Option Strict On
Option Explicit On

Imports Microsoft.Owin
Imports Owin

' This attribute tells OWIN to use this class as the startup configuration
<Assembly: OwinStartup(GetType(AHReportingSystem.Startup))>

Namespace AHReportingSystem

    Partial Public Class Startup

        Public Sub Configuration(app As IAppBuilder)
            ' Configure authentication (defined in App_Start/Startup.Auth.vb)
            ConfigureAuth(app)
        End Sub

    End Class

End Namespace
