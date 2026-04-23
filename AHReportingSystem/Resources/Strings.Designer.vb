Option Strict On
Option Explicit On

Imports System.Globalization
Imports System.Resources

Namespace Resources

    ''' <summary>
    ''' Strongly-typed access to localized strings. Manually maintained.
    ''' Add a property here whenever you add a resx data entry to both
    ''' Strings.resx (English / neutral) and Strings.es.resx (Spanish).
    ''' </summary>
    Public Module Strings

        ' NOTE: logical resource name is "AHReportingSystem.Strings" (VB projects
        ' ignore the containing folder — it's RootNamespace + filename, not
        ' RootNamespace + folder + filename like C# projects do).
        Private ReadOnly _resourceMan As New ResourceManager(
            "AHReportingSystem.Strings",
            GetType(Strings).Assembly)

        ''' <summary>Current UI culture (honors Thread.CurrentThread.CurrentUICulture).</summary>
        Public Property Culture As CultureInfo

        Private Function L(key As String) As String
            Return _resourceMan.GetString(key, Culture)
        End Function

        ' ---------- App branding ----------
        Public ReadOnly Property AppName As String
            Get
                Return L("AppName")
            End Get
        End Property
        Public ReadOnly Property AppShortName As String
            Get
                Return L("AppShortName")
            End Get
        End Property

        ' ---------- Navigation ----------
        Public ReadOnly Property Nav_Dashboard As String
            Get
                Return L("Nav_Dashboard")
            End Get
        End Property
        Public ReadOnly Property Nav_ChartOfAccounts As String
            Get
                Return L("Nav_ChartOfAccounts")
            End Get
        End Property
        Public ReadOnly Property Nav_SystemAccounts As String
            Get
                Return L("Nav_SystemAccounts")
            End Get
        End Property
        Public ReadOnly Property Nav_Categories As String
            Get
                Return L("Nav_Categories")
            End Get
        End Property
        Public ReadOnly Property Nav_Administration As String
            Get
                Return L("Nav_Administration")
            End Get
        End Property
        Public ReadOnly Property Nav_UserManagement As String
            Get
                Return L("Nav_UserManagement")
            End Get
        End Property
        Public ReadOnly Property Nav_ChangePassword As String
            Get
                Return L("Nav_ChangePassword")
            End Get
        End Property
        Public ReadOnly Property Nav_SignOut As String
            Get
                Return L("Nav_SignOut")
            End Get
        End Property
        Public ReadOnly Property Nav_Language As String
            Get
                Return L("Nav_Language")
            End Get
        End Property

        ' ---------- Common ----------
        Public ReadOnly Property Common_Home As String
            Get
                Return L("Common_Home")
            End Get
        End Property
        Public ReadOnly Property Common_English As String
            Get
                Return L("Common_English")
            End Get
        End Property
        Public ReadOnly Property Common_Spanish As String
            Get
                Return L("Common_Spanish")
            End Get
        End Property
        Public ReadOnly Property Common_Version As String
            Get
                Return L("Common_Version")
            End Get
        End Property

        ' ---------- Sign-in ----------
        Public ReadOnly Property SignIn_Title As String
            Get
                Return L("SignIn_Title")
            End Get
        End Property
        Public ReadOnly Property SignIn_Prompt As String
            Get
                Return L("SignIn_Prompt")
            End Get
        End Property
        Public ReadOnly Property SignIn_Email As String
            Get
                Return L("SignIn_Email")
            End Get
        End Property
        Public ReadOnly Property SignIn_Password As String
            Get
                Return L("SignIn_Password")
            End Get
        End Property
        Public ReadOnly Property SignIn_RememberMe As String
            Get
                Return L("SignIn_RememberMe")
            End Get
        End Property
        Public ReadOnly Property SignIn_Button As String
            Get
                Return L("SignIn_Button")
            End Get
        End Property
        Public ReadOnly Property SignIn_InvalidCreds As String
            Get
                Return L("SignIn_InvalidCreds")
            End Get
        End Property
        Public ReadOnly Property SignIn_InvalidEmailOrPassword As String
            Get
                Return L("SignIn_InvalidEmailOrPassword")
            End Get
        End Property
        Public ReadOnly Property SignIn_LockedOut As String
            Get
                Return L("SignIn_LockedOut")
            End Get
        End Property

        ' ---------- Lock screen ----------
        Public ReadOnly Property LockScreen_Title As String
            Get
                Return L("LockScreen_Title")
            End Get
        End Property
        Public ReadOnly Property LockScreen_EnterPassword As String
            Get
                Return L("LockScreen_EnterPassword")
            End Get
        End Property
        Public ReadOnly Property LockScreen_SignOutInstead As String
            Get
                Return L("LockScreen_SignOutInstead")
            End Get
        End Property
        Public ReadOnly Property LockScreen_SignOutConfirm As String
            Get
                Return L("LockScreen_SignOutConfirm")
            End Get
        End Property
        Public ReadOnly Property LockScreen_InactivityMsg As String
            Get
                Return L("LockScreen_InactivityMsg")
            End Get
        End Property
        Public ReadOnly Property LockScreen_IncorrectPassword As String
            Get
                Return L("LockScreen_IncorrectPassword")
            End Get
        End Property

        ' ---------- Error ----------
        Public ReadOnly Property Error_Title As String
            Get
                Return L("Error_Title")
            End Get
        End Property
        Public ReadOnly Property Error_Description As String
            Get
                Return L("Error_Description")
            End Get
        End Property
        Public ReadOnly Property Error_ReturnToDashboard As String
            Get
                Return L("Error_ReturnToDashboard")
            End Get
        End Property

        ' ---------- Dashboard ----------
        Public ReadOnly Property Dashboard_Welcome As String
            Get
                Return L("Dashboard_Welcome")
            End Get
        End Property
        Public ReadOnly Property Dashboard_Intro As String
            Get
                Return L("Dashboard_Intro")
            End Get
        End Property
        Public ReadOnly Property Dashboard_GettingStarted As String
            Get
                Return L("Dashboard_GettingStarted")
            End Get
        End Property
        Public ReadOnly Property Dashboard_GettingStartedText As String
            Get
                Return L("Dashboard_GettingStartedText")
            End Get
        End Property
        Public ReadOnly Property Dashboard_InfoSystemAccounts As String
            Get
                Return L("Dashboard_InfoSystemAccounts")
            End Get
        End Property
        Public ReadOnly Property Dashboard_InfoCompanies As String
            Get
                Return L("Dashboard_InfoCompanies")
            End Get
        End Property
        Public ReadOnly Property Dashboard_InfoGLPeriods As String
            Get
                Return L("Dashboard_InfoGLPeriods")
            End Get
        End Property
        Public ReadOnly Property Dashboard_InfoUnmapped As String
            Get
                Return L("Dashboard_InfoUnmapped")
            End Get
        End Property

        ' ---------- Change password ----------
        Public ReadOnly Property ChangePassword_Title As String
            Get
                Return L("ChangePassword_Title")
            End Get
        End Property
        Public ReadOnly Property ChangePassword_Forced As String
            Get
                Return L("ChangePassword_Forced")
            End Get
        End Property
        Public ReadOnly Property ChangePassword_Current As String
            Get
                Return L("ChangePassword_Current")
            End Get
        End Property
        Public ReadOnly Property ChangePassword_New As String
            Get
                Return L("ChangePassword_New")
            End Get
        End Property
        Public ReadOnly Property ChangePassword_Confirm As String
            Get
                Return L("ChangePassword_Confirm")
            End Get
        End Property
        Public ReadOnly Property ChangePassword_Submit As String
            Get
                Return L("ChangePassword_Submit")
            End Get
        End Property
        Public ReadOnly Property ChangePassword_Success As String
            Get
                Return L("ChangePassword_Success")
            End Get
        End Property
        Public ReadOnly Property ChangePassword_Mismatch As String
            Get
                Return L("ChangePassword_Mismatch")
            End Get
        End Property
        Public ReadOnly Property ChangePassword_Requirements As String
            Get
                Return L("ChangePassword_Requirements")
            End Get
        End Property

        ' ---------- Users ----------
        Public ReadOnly Property Users_Title As String
            Get
                Return L("Users_Title")
            End Get
        End Property
        Public ReadOnly Property Users_New As String
            Get
                Return L("Users_New")
            End Get
        End Property
        Public ReadOnly Property Users_Edit As String
            Get
                Return L("Users_Edit")
            End Get
        End Property
        Public ReadOnly Property Users_Create As String
            Get
                Return L("Users_Create")
            End Get
        End Property
        Public ReadOnly Property Users_Col_FullName As String
            Get
                Return L("Users_Col_FullName")
            End Get
        End Property
        Public ReadOnly Property Users_Col_Email As String
            Get
                Return L("Users_Col_Email")
            End Get
        End Property
        Public ReadOnly Property Users_Col_Role As String
            Get
                Return L("Users_Col_Role")
            End Get
        End Property
        Public ReadOnly Property Users_Col_Active As String
            Get
                Return L("Users_Col_Active")
            End Get
        End Property
        Public ReadOnly Property Users_Col_LastLogin As String
            Get
                Return L("Users_Col_LastLogin")
            End Get
        End Property
        Public ReadOnly Property Users_Col_Actions As String
            Get
                Return L("Users_Col_Actions")
            End Get
        End Property
        Public ReadOnly Property Users_InitialPassword As String
            Get
                Return L("Users_InitialPassword")
            End Get
        End Property
        Public ReadOnly Property Users_InitialPassword_Hint As String
            Get
                Return L("Users_InitialPassword_Hint")
            End Get
        End Property
        Public ReadOnly Property Users_ResetPassword As String
            Get
                Return L("Users_ResetPassword")
            End Get
        End Property
        Public ReadOnly Property Users_Activate As String
            Get
                Return L("Users_Activate")
            End Get
        End Property
        Public ReadOnly Property Users_Deactivate As String
            Get
                Return L("Users_Deactivate")
            End Get
        End Property
        Public ReadOnly Property Users_Created As String
            Get
                Return L("Users_Created")
            End Get
        End Property
        Public ReadOnly Property Users_Updated As String
            Get
                Return L("Users_Updated")
            End Get
        End Property
        Public ReadOnly Property Users_Activated As String
            Get
                Return L("Users_Activated")
            End Get
        End Property
        Public ReadOnly Property Users_Deactivated As String
            Get
                Return L("Users_Deactivated")
            End Get
        End Property
        Public ReadOnly Property Users_PasswordReset As String
            Get
                Return L("Users_PasswordReset")
            End Get
        End Property
        Public ReadOnly Property Users_CannotDeactivateSelf As String
            Get
                Return L("Users_CannotDeactivateSelf")
            End Get
        End Property
        Public ReadOnly Property Users_CannotChangeSelfRole As String
            Get
                Return L("Users_CannotChangeSelfRole")
            End Get
        End Property
        Public ReadOnly Property Users_EmailTaken As String
            Get
                Return L("Users_EmailTaken")
            End Get
        End Property
        Public ReadOnly Property Users_NotFound As String
            Get
                Return L("Users_NotFound")
            End Get
        End Property

        ' ---------- Roles (localized labels; DB identifiers stay English) ----------
        Public ReadOnly Property Role_Admin As String
            Get
                Return L("Role_Admin")
            End Get
        End Property
        Public ReadOnly Property Role_Supervisor As String
            Get
                Return L("Role_Supervisor")
            End Get
        End Property
        Public ReadOnly Property Role_User As String
            Get
                Return L("Role_User")
            End Get
        End Property
        Public ReadOnly Property Role_Admin_Desc As String
            Get
                Return L("Role_Admin_Desc")
            End Get
        End Property
        Public ReadOnly Property Role_Supervisor_Desc As String
            Get
                Return L("Role_Supervisor_Desc")
            End Get
        End Property
        Public ReadOnly Property Role_User_Desc As String
            Get
                Return L("Role_User_Desc")
            End Get
        End Property

        ' ---------- Additional common labels ----------
        Public ReadOnly Property Common_Save As String
            Get
                Return L("Common_Save")
            End Get
        End Property
        Public ReadOnly Property Common_Cancel As String
            Get
                Return L("Common_Cancel")
            End Get
        End Property
        Public ReadOnly Property Common_Edit As String
            Get
                Return L("Common_Edit")
            End Get
        End Property
        Public ReadOnly Property Common_Yes As String
            Get
                Return L("Common_Yes")
            End Get
        End Property
        Public ReadOnly Property Common_No As String
            Get
                Return L("Common_No")
            End Get
        End Property
        Public ReadOnly Property Common_Never As String
            Get
                Return L("Common_Never")
            End Get
        End Property
        Public ReadOnly Property Common_Search As String
            Get
                Return L("Common_Search")
            End Get
        End Property
        Public ReadOnly Property Common_ExportExcel As String
            Get
                Return L("Common_ExportExcel")
            End Get
        End Property
        Public ReadOnly Property Common_Required As String
            Get
                Return L("Common_Required")
            End Get
        End Property
        Public ReadOnly Property Common_SelectOne As String
            Get
                Return L("Common_SelectOne")
            End Get
        End Property
        Public ReadOnly Property Common_Status As String
            Get
                Return L("Common_Status")
            End Get
        End Property
        Public ReadOnly Property Common_Created As String
            Get
                Return L("Common_Created")
            End Get
        End Property
        Public ReadOnly Property Common_Updated As String
            Get
                Return L("Common_Updated")
            End Get
        End Property
        Public ReadOnly Property Common_Activated As String
            Get
                Return L("Common_Activated")
            End Get
        End Property
        Public ReadOnly Property Common_Deactivated As String
            Get
                Return L("Common_Deactivated")
            End Get
        End Property
        Public ReadOnly Property Common_NotFound As String
            Get
                Return L("Common_NotFound")
            End Get
        End Property

        ' ---------- System Accounts ----------
        Public ReadOnly Property Accounts_Title As String
            Get
                Return L("Accounts_Title")
            End Get
        End Property
        Public ReadOnly Property Accounts_New As String
            Get
                Return L("Accounts_New")
            End Get
        End Property
        Public ReadOnly Property Accounts_Create As String
            Get
                Return L("Accounts_Create")
            End Get
        End Property
        Public ReadOnly Property Accounts_Edit As String
            Get
                Return L("Accounts_Edit")
            End Get
        End Property
        Public ReadOnly Property Accounts_Col_Id As String
            Get
                Return L("Accounts_Col_Id")
            End Get
        End Property
        Public ReadOnly Property Accounts_Col_Name As String
            Get
                Return L("Accounts_Col_Name")
            End Get
        End Property
        Public ReadOnly Property Accounts_Col_Category As String
            Get
                Return L("Accounts_Col_Category")
            End Get
        End Property
        Public ReadOnly Property Accounts_Col_SubCategory As String
            Get
                Return L("Accounts_Col_SubCategory")
            End Get
        End Property
        Public ReadOnly Property Accounts_Col_Grouping As String
            Get
                Return L("Accounts_Col_Grouping")
            End Get
        End Property
        Public ReadOnly Property Accounts_Col_Active As String
            Get
                Return L("Accounts_Col_Active")
            End Get
        End Property
        Public ReadOnly Property Accounts_IdTaken As String
            Get
                Return L("Accounts_IdTaken")
            End Get
        End Property
        Public ReadOnly Property Accounts_SubCategoryMismatch As String
            Get
                Return L("Accounts_SubCategoryMismatch")
            End Get
        End Property
        Public ReadOnly Property Accounts_ExportFileName As String
            Get
                Return L("Accounts_ExportFileName")
            End Get
        End Property

        ' ---------- Categories ----------
        Public ReadOnly Property Categories_Title As String
            Get
                Return L("Categories_Title")
            End Get
        End Property
        Public ReadOnly Property Categories_New As String
            Get
                Return L("Categories_New")
            End Get
        End Property
        Public ReadOnly Property Categories_Create As String
            Get
                Return L("Categories_Create")
            End Get
        End Property
        Public ReadOnly Property Categories_Edit As String
            Get
                Return L("Categories_Edit")
            End Get
        End Property
        Public ReadOnly Property Categories_NameTaken As String
            Get
                Return L("Categories_NameTaken")
            End Get
        End Property
        Public ReadOnly Property Categories_InUse As String
            Get
                Return L("Categories_InUse")
            End Get
        End Property

        ' ---------- Sub-Categories ----------
        Public ReadOnly Property SubCategories_Title As String
            Get
                Return L("SubCategories_Title")
            End Get
        End Property
        Public ReadOnly Property SubCategories_New As String
            Get
                Return L("SubCategories_New")
            End Get
        End Property
        Public ReadOnly Property SubCategories_Create As String
            Get
                Return L("SubCategories_Create")
            End Get
        End Property
        Public ReadOnly Property SubCategories_Edit As String
            Get
                Return L("SubCategories_Edit")
            End Get
        End Property
        Public ReadOnly Property SubCategories_NameTaken As String
            Get
                Return L("SubCategories_NameTaken")
            End Get
        End Property

        ' ---------- Groupings ----------
        Public ReadOnly Property Groupings_Title As String
            Get
                Return L("Groupings_Title")
            End Get
        End Property
        Public ReadOnly Property Groupings_New As String
            Get
                Return L("Groupings_New")
            End Get
        End Property
        Public ReadOnly Property Groupings_Create As String
            Get
                Return L("Groupings_Create")
            End Get
        End Property
        Public ReadOnly Property Groupings_Edit As String
            Get
                Return L("Groupings_Edit")
            End Get
        End Property
        Public ReadOnly Property Groupings_NameTaken As String
            Get
                Return L("Groupings_NameTaken")
            End Get
        End Property

        ' ---------- Shared field labels ----------
        Public ReadOnly Property Field_Name As String
            Get
                Return L("Field_Name")
            End Get
        End Property
        Public ReadOnly Property Field_Active As String
            Get
                Return L("Field_Active")
            End Get
        End Property

        ' ---------- Companies ----------
        Public ReadOnly Property Companies_Title As String
            Get
                Return L("Companies_Title")
            End Get
        End Property
        Public ReadOnly Property Companies_New As String
            Get
                Return L("Companies_New")
            End Get
        End Property
        Public ReadOnly Property Companies_Create As String
            Get
                Return L("Companies_Create")
            End Get
        End Property
        Public ReadOnly Property Companies_Edit As String
            Get
                Return L("Companies_Edit")
            End Get
        End Property
        Public ReadOnly Property Companies_Col_Code As String
            Get
                Return L("Companies_Col_Code")
            End Get
        End Property
        Public ReadOnly Property Companies_Col_Name As String
            Get
                Return L("Companies_Col_Name")
            End Get
        End Property
        Public ReadOnly Property Companies_CodeTaken As String
            Get
                Return L("Companies_CodeTaken")
            End Get
        End Property
        Public ReadOnly Property Companies_CodeHint As String
            Get
                Return L("Companies_CodeHint")
            End Get
        End Property
        Public ReadOnly Property Nav_Companies As String
            Get
                Return L("Nav_Companies")
            End Get
        End Property

        ' ---------- User / company access ----------
        Public ReadOnly Property Users_CompanyAccess As String
            Get
                Return L("Users_CompanyAccess")
            End Get
        End Property
        Public ReadOnly Property Users_CompanyAccess_AdminHint As String
            Get
                Return L("Users_CompanyAccess_AdminHint")
            End Get
        End Property
        Public ReadOnly Property Users_CompanyAccess_AllIfEmpty As String
            Get
                Return L("Users_CompanyAccess_AllIfEmpty")
            End Get
        End Property
        Public ReadOnly Property Users_NoAccessToCompany As String
            Get
                Return L("Users_NoAccessToCompany")
            End Get
        End Property

        ''' <summary>Localized label for a role DB identifier ("Admin" | "Supervisor" | "User").</summary>
        Public Function RoleLabel(roleId As String) As String
            Select Case roleId
                Case "Admin" : Return Role_Admin
                Case "Supervisor" : Return Role_Supervisor
                Case "User" : Return Role_User
                Case Else : Return roleId
            End Select
        End Function

    End Module

End Namespace
