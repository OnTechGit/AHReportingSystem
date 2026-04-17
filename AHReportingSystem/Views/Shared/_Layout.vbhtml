@Imports System.Web.Optimization
@Imports System.Configuration
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>@If ViewBag.Title IsNot Nothing Then @ViewBag.Title Else @Html.Raw("Dashboard") End If — AH Reporting System</title>

    <!-- Google Font: Source Sans Pro -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback" />
    <!-- Font Awesome Icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    <!-- AdminLTE CSS -->
    @Styles.Render("~/Content/adminlte")
    <!-- DataTables CSS -->
    @Styles.Render("~/Content/datatables")
    <!-- Select2 CSS -->
    @Styles.Render("~/Content/select2")
    <!-- Custom AH styles (color override #008bcc) -->
    @Styles.Render("~/Content/ahcustom")

    @RenderSection("styles", required:=False)
</head>
<body class="hold-transition sidebar-mini layout-fixed">
<div class="wrapper">

    <!-- ===================================================
         NAVBAR (top bar)
    ==================================================== -->
    <nav class="main-header navbar navbar-expand navbar-white navbar-light">
        <!-- Left navbar links -->
        <ul class="navbar-nav">
            <li class="nav-item">
                <a class="nav-link" data-widget="pushmenu" href="#" role="button">
                    <i class="fas fa-bars"></i>
                </a>
            </li>
        </ul>

        <!-- Right navbar links -->
        <ul class="navbar-nav ml-auto">
            <!-- Current user -->
            <li class="nav-item dropdown">
                <a class="nav-link" data-toggle="dropdown" href="#">
                    <i class="far fa-user"></i>
                    <span class="d-none d-sm-inline ml-1">@User.Identity.Name</span>
                </a>
                <div class="dropdown-menu dropdown-menu-right">
                    <a href="@Url.Action("ChangePassword", "Account")" class="dropdown-item">
                        <i class="fas fa-key mr-2"></i> Change Password
                    </a>
                    <div class="dropdown-divider"></div>
                    @Using Html.BeginForm("LogOut", "Account", FormMethod.Post)
                        @Html.AntiForgeryToken()
                        @<button type="submit" class="dropdown-item text-danger">
                            <i class="fas fa-sign-out-alt mr-2"></i> Sign Out
                        </button>
                    End Using
                </div>
            </li>
        </ul>
    </nav>

    <!-- ===================================================
         SIDEBAR
    ==================================================== -->
    <aside class="main-sidebar sidebar-dark-primary elevation-4">
        <!-- Brand / Logo -->
        <a href="@Url.Action("Index", "Home")" class="brand-link">
            <img src="@Url.Content("~/Content/img/ah-logo.png")"
                 alt="Alternative Holdings"
                 class="brand-image img-circle elevation-3"
                 style="opacity: .8; max-height:33px; width:auto;" />
            <span class="brand-text font-weight-light">AH Reporting</span>
        </a>

        <!-- Sidebar wrapper -->
        <div class="sidebar">
            <!-- Sidebar user panel -->
            <div class="user-panel mt-3 pb-3 mb-3 d-flex">
                <div class="info">
                    <a href="#" class="d-block text-white">
                        <i class="fas fa-user-circle mr-1"></i>
                        @User.Identity.Name
                    </a>
                </div>
            </div>

            <!-- Sidebar menu -->
            <nav class="mt-2">
                <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">

                    <!-- Dashboard -->
                    <li class="nav-item">
                        <a href="@Url.Action("Index", "Home")"
                           class="nav-link @If ViewContext.RouteData.Values("controller").ToString() = "Home" Then "active" End If">
                            <i class="nav-icon fas fa-tachometer-alt"></i>
                            <p>Dashboard</p>
                        </a>
                    </li>

                    <!-- Accounts Catalog -->
                    <li class="nav-item has-treeview @If ViewContext.RouteData.Values("controller").ToString() = "Accounts" Then "menu-open" End If">
                        <a href="#" class="nav-link @If ViewContext.RouteData.Values("controller").ToString() = "Accounts" Then "active" End If">
                            <i class="nav-icon fas fa-book"></i>
                            <p>
                                Chart of Accounts
                                <i class="right fas fa-angle-left"></i>
                            </p>
                        </a>
                        <ul class="nav nav-treeview">
                            <li class="nav-item">
                                <a href="@Url.Action("Index", "Accounts")" class="nav-link">
                                    <i class="far fa-circle nav-icon"></i>
                                    <p>System Accounts</p>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a href="@Url.Action("Index", "Categories")" class="nav-link">
                                    <i class="far fa-circle nav-icon"></i>
                                    <p>Categories</p>
                                </a>
                            </li>
                        </ul>
                    </li>

                    <!-- Admin Section (Admin role only) -->
                    @If User.IsInRole("Admin") Then
                        @<li class="nav-header">ADMINISTRATION</li>
                        @<li class="nav-item">
                            <a href="@Url.Action("Index", "Users")"
                               class="nav-link @If ViewContext.RouteData.Values("controller").ToString() = "Users" Then "active" End If">
                                <i class="nav-icon fas fa-users-cog"></i>
                                <p>User Management</p>
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
        <!-- Content Header (Page title) -->
        <div class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1 class="m-0">@ViewBag.Title</h1>
                    </div>
                    <div class="col-sm-6">
                        <ol class="breadcrumb float-sm-right">
                            <li class="breadcrumb-item">
                                <a href="@Url.Action("Index", "Home")">Home</a>
                            </li>
                            @If ViewBag.BreadcrumbParent IsNot Nothing Then
                                @<li class="breadcrumb-item">@ViewBag.BreadcrumbParent</li>
                            End If
                            <li class="breadcrumb-item active">@ViewBag.Title</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>

        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">

                <!-- Success message -->
                @If TempData("SuccessMessage") IsNot Nothing Then
                    @<div class="alert alert-success alert-dismissible fade show" role="alert">
                        <i class="fas fa-check-circle mr-2"></i>
                        @TempData("SuccessMessage")
                        <button type="button" class="close" data-dismiss="alert">
                            <span>&times;</span>
                        </button>
                    </div>
                End If

                <!-- Error message -->
                @If TempData("ErrorMessage") IsNot Nothing Then
                    @<div class="alert alert-danger alert-dismissible fade show" role="alert">
                        <i class="fas fa-exclamation-circle mr-2"></i>
                        @TempData("ErrorMessage")
                        <button type="button" class="close" data-dismiss="alert">
                            <span>&times;</span>
                        </button>
                    </div>
                End If

                <!-- Warning message -->
                @If TempData("WarningMessage") IsNot Nothing Then
                    @<div class="alert alert-warning alert-dismissible fade show" role="alert">
                        <i class="fas fa-exclamation-triangle mr-2"></i>
                        @TempData("WarningMessage")
                        <button type="button" class="close" data-dismiss="alert">
                            <span>&times;</span>
                        </button>
                    </div>
                End If

                @RenderBody()

            </div>
        </section>
    </div>

    <!-- ===================================================
         FOOTER
    ==================================================== -->
    <footer class="main-footer">
        <strong>AH Reporting System</strong> &copy; @DateTime.Now.Year Alternative Holdings.
        <div class="float-right d-none d-sm-inline-block">
            <b>Version</b> 1.0.0
        </div>
    </footer>

</div>

<!-- ===================================================
     SCRIPTS
==================================================== -->
<!-- jQuery (must load first) -->
@Scripts.Render("~/bundles/jquery")
<!-- Bootstrap 4 bundle (includes Popper.js) -->
@Scripts.Render("~/bundles/adminlte")
<!-- DataTables -->
@Scripts.Render("~/bundles/datatables")
<!-- Select2 -->
@Scripts.Render("~/bundles/select2")
<!-- jQuery Validation -->
@Scripts.Render("~/bundles/jqueryval")

<!-- ===================================================
     LOCK SCREEN — Inactivity Timer
==================================================== -->
<script>
    (function () {
        // Read timeout from meta tag (set below) — in milliseconds
        var timeoutMinutes = parseInt('@ConfigurationManager.AppSettings("LockScreenTimeoutMinutes")', 10) || 15;
        var timeoutMs = timeoutMinutes * 60 * 1000;
        var lockUrl = '@Url.Action("LockScreen", "Account")';
        var timer;

        if (timeoutMinutes <= 0) return; // 0 = disabled

        function resetTimer() {
            clearTimeout(timer);
            timer = setTimeout(function () {
                window.location.href = lockUrl;
            }, timeoutMs);
        }

        // Events that reset inactivity timer
        ['mousemove', 'keypress', 'click', 'scroll', 'touchstart'].forEach(function (evt) {
            document.addEventListener(evt, resetTimer, true);
        });

        // Start the timer on page load
        resetTimer();
    })();
</script>

@RenderSection("scripts", required:=False)

</body>
</html>
