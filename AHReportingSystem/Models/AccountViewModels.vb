Option Strict On
Option Explicit On

Imports System.ComponentModel.DataAnnotations

Namespace AHReportingSystem.Models

    ' =========================================================
    ' LoginViewModel
    ' Used by Account/Login view
    ' =========================================================
    Public Class LoginViewModel

        <Required(ErrorMessage:="Email is required.")>
        <EmailAddress(ErrorMessage:="Invalid email address.")>
        <Display(Name:="Email")>
        Public Property Email As String

        <Required(ErrorMessage:="Password is required.")>
        <DataType(DataType.Password)>
        <Display(Name:="Password")>
        Public Property Password As String

        <Display(Name:="Remember me")>
        Public Property RememberMe As Boolean

    End Class

    ' =========================================================
    ' LockScreenViewModel
    ' Used by Account/LockScreen view
    ' =========================================================
    Public Class LockScreenViewModel

        <Required(ErrorMessage:="Password is required to unlock.")>
        <DataType(DataType.Password)>
        <Display(Name:="Password")>
        Public Property Password As String

        Public Property ReturnUrl As String

        Public Property UserFullName As String

        Public Property UserEmail As String

    End Class

    ' =========================================================
    ' ChangePasswordViewModel
    ' Used by Account/ChangePassword view
    ' =========================================================
    Public Class ChangePasswordViewModel

        <Required>
        <DataType(DataType.Password)>
        <Display(Name:="Current password")>
        Public Property OldPassword As String

        <Required>
        <StringLength(100, ErrorMessage:="The {0} must be at least {2} characters long.", MinimumLength:=8)>
        <DataType(DataType.Password)>
        <Display(Name:="New password")>
        Public Property NewPassword As String

        <DataType(DataType.Password)>
        <Display(Name:="Confirm new password")>
        <Compare("NewPassword", ErrorMessage:="The new password and confirmation do not match.")>
        Public Property ConfirmPassword As String

    End Class

End Namespace
