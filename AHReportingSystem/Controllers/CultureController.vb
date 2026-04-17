Option Strict On
Option Explicit On

Namespace Controllers

    ''' <summary>
    ''' Lets any user (even anonymous) switch between supported UI cultures.
    ''' </summary>
    <AllowAnonymous>
    Public Class CultureController
        Inherits Controller

        ' GET: /Culture/Set?lang=es&returnUrl=/...
        Public Function [Set](lang As String, returnUrl As String) As ActionResult
            CultureHelper.WriteCookie(HttpContext, lang)
            If Not String.IsNullOrEmpty(returnUrl) AndAlso Url.IsLocalUrl(returnUrl) Then
                Return Redirect(returnUrl)
            End If
            Return RedirectToAction("Index", "Home")
        End Function

    End Class

End Namespace
