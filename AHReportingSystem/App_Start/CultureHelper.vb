Option Strict On
Option Explicit On

Imports System.Globalization
Imports System.Threading
Imports System.Web

''' <summary>
''' Helper to read / write the user's preferred UI culture.
''' Reads cookie "_culture" (values: "en" or "es"); defaults to "en".
''' Call ApplyFromCookie() early in each request (Application_BeginRequest).
''' </summary>
Public Module CultureHelper

    Public Const CookieName As String = "_culture"
    Public Const DefaultCulture As String = "en"

    Public ReadOnly Property SupportedCultures As String() = {"en", "es"}

    ''' <summary>Read cookie and set Thread.CurrentThread.CurrentUICulture.</summary>
    Public Sub ApplyFromCookie(context As HttpContext)
        Dim code = ReadCookie(context.Request.Cookies)
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(code)
        ' Keep CurrentCulture (numbers/dates) stable for accounting — only UI strings change.
    End Sub

    ''' <summary>Persist the chosen culture in the cookie (1 year). Accepts MVC's HttpContextBase.</summary>
    Public Sub WriteCookie(context As HttpContextBase, code As String)
        If Array.IndexOf(SupportedCultures, code) < 0 Then code = DefaultCulture
        Dim cookie As New HttpCookie(CookieName, code) With {
            .Expires = DateTime.UtcNow.AddYears(1),
            .HttpOnly = False,
            .Path = "/"
        }
        context.Response.Cookies.Set(cookie)
    End Sub

    Private Function ReadCookie(cookies As HttpCookieCollection) As String
        Dim cookie = cookies(CookieName)
        If cookie IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cookie.Value) Then
            If Array.IndexOf(SupportedCultures, cookie.Value) >= 0 Then
                Return cookie.Value
            End If
        End If
        Return DefaultCulture
    End Function

    ''' <summary>Current two-letter code ("en" or "es").</summary>
    Public Function Current() As String
        Dim name = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName
        If Array.IndexOf(SupportedCultures, name) < 0 Then name = DefaultCulture
        Return name
    End Function

End Module
