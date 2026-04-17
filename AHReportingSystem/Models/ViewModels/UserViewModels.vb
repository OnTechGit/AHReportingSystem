Option Strict On
Option Explicit On

Imports System.ComponentModel.DataAnnotations

Namespace Models.ViewModels

    ''' <summary>Row in the Users/Index DataTable.</summary>
    Public Class UserListItemViewModel
        Public Property Id As String
        Public Property Email As String
        Public Property FullName As String
        Public Property Role As String
        Public Property IsActive As Boolean
        Public Property LastLogin As DateTime?
        Public Property IsSelf As Boolean
    End Class

    ''' <summary>Shared fields for Create / Edit; used as part of CreateUserViewModel / EditUserViewModel.</summary>
    Public MustInherit Class UserFormViewModelBase

        <Required>
        <StringLength(200)>
        <Display(Name:="Full Name")>
        Public Property FullName As String

        <Required>
        <EmailAddress>
        <StringLength(256)>
        <Display(Name:="Email")>
        Public Property Email As String

        <Required>
        <Display(Name:="Role")>
        Public Property Role As String    ' "Admin" | "Supervisor" | "User"

    End Class

    ''' <summary>Create new user: admin supplies initial password.</summary>
    Public Class CreateUserViewModel
        Inherits UserFormViewModelBase

        <Required>
        <StringLength(100, MinimumLength:=8)>
        <DataType(DataType.Password)>
        <Display(Name:="Initial password")>
        Public Property InitialPassword As String

    End Class

    ''' <summary>Edit existing user — no password here, use reset action.</summary>
    Public Class EditUserViewModel
        Inherits UserFormViewModelBase

        Public Property Id As String

        <Display(Name:="Active")>
        Public Property IsActive As Boolean

    End Class

    ''' <summary>Admin resets a user's password — user will be forced to change on next login.</summary>
    Public Class ResetPasswordViewModel

        Public Property Id As String

        <Required>
        <StringLength(100, MinimumLength:=8)>
        <DataType(DataType.Password)>
        <Display(Name:="New password")>
        Public Property NewPassword As String

    End Class

End Namespace
