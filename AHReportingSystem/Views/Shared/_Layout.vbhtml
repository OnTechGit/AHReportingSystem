@Imports System.Configuration
@Imports System.Security.Claims
@Imports AHReportingSystem.Resources

@Code
    Dim currentController As String = CStr(ViewContext.RouteData.Values("controller"))
    Dim pageTitle As String = If(CStr(ViewBag.Title), "")
    If String.IsNullOrEmpty(pageTitle) Then pageTitle = Strings.Nav_Dashboard
    Dim breadcrumbParent As String = CStr(ViewBag.BreadcrumbParent)
    Dim currentLang As String = CultureHelper.Current()
    Dim returnUrl As String = Request.RawUrl

    ' Prefer the FullName claim (added by ApplicationUser.GenerateUserIdentityAsync)
    ' so the top navbar shows the person's name, not their email.
    Dim userDisplayName As String = User.Identity.Name
    Dim claimsIdent As ClaimsIdentity = TryCast(User.Identity, ClaimsIdentity)
    If claimsIdent IsNot Nothing Then
        Dim nameClaim = claimsIdent.FindFirst("FullName")
        If nameClaim IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(nameClaim.Value) Then
            userDisplayName = nameClaim.Value
        End If
    End If
End Code

<!DOCTYPE html>
<html lang="@currentLang">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>@pageTitle — @Strings.AppName</title>

    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback" />
    <link rel="stylesheet" href="@Url.Content("~/Content/fontawesome/css/all.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/bootstrap/css/bootstrap.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/AdminLTE/css/adminlte.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/datatables/dataTables.bootstrap4.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/datatables/buttons.bootstrap4.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/select2/select2.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/select2/select2-bootstrap4.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/sweetalert2/sweetalert2.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/custom.css")" />

    @RenderSection("styles", required:=False)
</head>
<body class="hold-transition sidebar-mini layout-fixed">
<div class="wrapper">

    <!-- ===================================================
         NAVBAR (top bar)
    ==================================================== -->
    <nav class="main-header navbar navbar-expand navbar-white navbar-light">
        <ul class="navbar-nav">
            <li class="nav-item">
                <a class="nav-link" data-widget="pushmenu" href="#" role="button">
                    <i class="fas fa-bars"></i>
                </a>
            </li>
        </ul>

        <ul class="navbar-nav ml-auto">

            <!-- Language switcher -->
            <li class="nav-item dropdown">
                <a class="nav-link" data-toggle="dropdown" href="#" title="@Strings.Nav_Language">
                    <i class="fas fa-globe"></i>
                    <span class="d-none d-sm-inline ml-1">@currentLang.ToUpper()</span>
                </a>
                <div class="dropdown-menu dropdown-menu-right">
                    <a href="@Url.Action("Set", "Culture", New With {.lang = "en", .returnUrl = returnUrl})"
                       class="dropdown-item @(If(currentLang = "en", "active", ""))">
                        <i class="fas fa-check mr-2 @(If(currentLang = "en", "", "invisible"))"></i>
                        @Strings.Common_English
                    </a>
                    <a href="@Url.Action("Set", "Culture", New With {.lang = "es", .returnUrl = returnUrl})"
                       class="dropdown-item @(If(currentLang = "es", "active", ""))">
                        <i class="fas fa-check mr-2 @(If(currentLang = "es", "", "invisible"))"></i>
                        @Strings.Common_Spanish
                    </a>
                </div>
            </li>

            <!-- User menu -->
            <li class="nav-item dropdown">
                <a class="nav-link" data-toggle="dropdown" href="#">
                    <i class="far fa-user"></i>
                    <span class="d-none d-sm-inline ml-1">@userDisplayName</span>
                </a>
                <div class="dropdown-menu dropdown-menu-right">
                    <a href="@Url.Action("ChangePassword", "Account")" class="dropdown-item">
                        <i class="fas fa-key mr-2"></i> @Strings.Nav_ChangePassword
                    </a>
                    <div class="dropdown-divider"></div>
                    <form action="@Url.Action("LogOut", "Account")" method="post" class="m-0">
                        @Html.AntiForgeryToken()
                        <button type="submit" class="dropdown-item text-danger">
                            <i class="fas fa-sign-out-alt mr-2"></i> @Strings.Nav_SignOut
                        </button>
                    </form>
                </div>
            </li>
        </ul>
    </nav>

    <!-- ===================================================
         SIDEBAR
    ==================================================== -->
    <aside class="main-sidebar sidebar-dark-primary elevation-4">
        <a href="@Url.Action("Index", "Home")" class="brand-link">
            <img src="@Url.Content("~/Content/img/ah-logo.png")"
                 alt="Alternative Holdings"
                 class="brand-image elevation-3"
                 style="opacity:.9; max-height:33px; width:auto;" />
            <span class="brand-text font-weight-light">@Strings.AppShortName</span>
        </a>
        <div class="sidebar">
            <nav class="mt-2">
                <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">

                    <li class="nav-item">
                        <a href="@Url.Action("Index", "Home")"
                           class="nav-link @(If(currentController = "Home", "active", ""))">
                            <i class="nav-icon fas fa-tachometer-alt"></i>
                            <p>@Strings.Nav_Dashboard</p>
                        </a>
                    </li>

                    <li class="nav-item">
                        <a href="@Url.Action("Index", "Companies")"
                           class="nav-link @(If(currentController = "Companies", "active", ""))">
                            <i class="nav-icon fas fa-building"></i>
                            <p>@Strings.Nav_Companies</p>
                        </a>
                    </li>

                    @Code
                        Dim inChart As Boolean = currentController = "Accounts" OrElse currentController = "Categories" OrElse currentController = "SubCategories" OrElse currentController = "Groupings"
                    End Code
                    <li class="nav-item has-treeview @(If(inChart, "menu-open", ""))">
                        <a href="#" class="nav-link @(If(inChart, "active", ""))">
                            <i class="nav-icon fas fa-book"></i>
                            <p>@Strings.Nav_ChartOfAccounts <i class="right fas fa-angle-left"></i></p>
                        </a>
                        <ul class="nav nav-treeview">
                            <li class="nav-item">
                                <a href="@Url.Action("Index", "Accounts")" class="nav-link @(If(currentController = "Accounts", "active", ""))">
                                    <i class="far fa-circle nav-icon"></i>
                                    <p>@Strings.Accounts_Title</p>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a href="@Url.Action("Index", "Categories")" class="nav-link @(If(currentController = "Categories", "active", ""))">
                                    <i class="far fa-circle nav-icon"></i>
                                    <p>@Strings.Categories_Title</p>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a href="@Url.Action("Index", "SubCategories")" class="nav-link @(If(currentController = "SubCategories", "active", ""))">
                                    <i class="far fa-circle nav-icon"></i>
                                    <p>@Strings.SubCategories_Title</p>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a href="@Url.Action("Index", "Groupings")" class="nav-link @(If(currentController = "Groupings", "active", ""))">
                                    <i class="far fa-circle nav-icon"></i>
                                    <p>@Strings.Groupings_Title</p>
                                </a>
                            </li>
                        </ul>
                    </li>

                    @If User.IsInRole("Admin") Then
                        @<li class="nav-header">@Strings.Nav_Administration</li>
                        @<li class="nav-item">
                            <a href="@Url.Action("Index", "Users")"
                               class="nav-link @(If(currentController = "Users", "active", ""))">
                                <i class="nav-icon fas fa-users-cog"></i>
                                <p>@Strings.Nav_UserManagement</p>
                            </a>
                        </li>
                    End If

                </ul>
            </nav>
        </div>
    </aside>

    <!-- ===================================================
         CONTENT WRAPPER
    ==================================================== -->
    <div class="content-wrapper">
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0">@pageTitle</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item">
                                <a href="@Url.Action("Index", "Home")">@Strings.Common_Home</a>
                            </li>
                            @If Not String.IsNullOrEmpty(breadcrumbParent) Then
                                @<li class="breadcrumb-item">@breadcrumbParent</li>
                            End If
                            <li class="breadcrumb-item active">@pageTitle</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>

        <section class="content">
            <div class="container-fluid">

                @If TempData("SuccessMessage") IsNot Nothing Then
                    @<div class="alert alert-success alert-dismissible">
                        <button type="button" class="close" data-dismiss="alert">&times;</button>
                        <i class="fas fa-check-circle mr-2"></i> @TempData("SuccessMessage")
                    </div>
                End If
                @If TempData("ErrorMessage") IsNot Nothing Then
                    @<div class="alert alert-danger alert-dismissible">
                        <button type="button" class="close" data-dismiss="alert">&times;</button>
                        <i class="fas fa-exclamation-circle mr-2"></i> @TempData("ErrorMessage")
                    </div>
                End If
                @If TempData("WarningMessage") IsNot Nothing Then
                    @<div class="alert alert-warning alert-dismissible">
                        <button type="button" class="close" data-dismiss="alert">&times;</button>
                        <i class="fas fa-exclamation-triangle mr-2"></i> @TempData("WarningMessage")
                    </div>
                End If

                @RenderBody()

            </div>
        </section>
    </div>

    <footer class="main-footer">
        <strong>@Strings.AppName</strong> &copy; @DateTime.Now.Year Alternative Holdings.
        <div class="float-right d-none d-sm-inline-block"><b>@Strings.Common_Version</b> 1.0.0</div>
    </footer>

</div>

<script src="@Url.Content("~/Scripts/jquery-3.7.1.min.js")"></script>
<script src="@Url.Content("~/Content/bootstrap/js/bootstrap.bundle.min.js")"></script>
<script src="@Url.Content("~/Content/AdminLTE/js/adminlte.min.js")"></script>
<script src="@Url.Content("~/Scripts/datatables/jquery.dataTables.min.js")"></script>
<script src="@Url.Content("~/Scripts/datatables/dataTables.bootstrap4.min.js")"></script>
<script src="@Url.Content("~/Scripts/datatables/jszip.min.js")"></script>
<script src="@Url.Content("~/Scripts/datatables/dataTables.buttons.min.js")"></script>
<script src="@Url.Content("~/Scripts/datatables/buttons.bootstrap4.min.js")"></script>
<script src="@Url.Content("~/Scripts/datatables/buttons.html5.min.js")"></script>
<script src="@Url.Content("~/Scripts/select2/select2.full.min.js")"></script>
<script src="@Url.Content("~/Scripts/sweetalert2.all.min.js")"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.min.js")"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")"></script>

<script>
(function () {
    var mins = parseInt('@ConfigurationManager.AppSettings("LockScreenTimeoutMinutes")', 10) || 15;
    if (mins <= 0) return;
    var lockUrl = '@Url.Action("LockScreen", "Account")';
    var timer;
    function reset() {
        clearTimeout(timer);
        timer = setTimeout(function () { window.location.href = lockUrl; }, mins * 60000);
    }
    ['mousemove','keypress','click','scroll','touchstart'].forEach(function(e) {
        document.addEventListener(e, reset, true);
    });
    reset();
}());
</script>

@RenderSection("scripts", required:=False)

</body>
</html>
