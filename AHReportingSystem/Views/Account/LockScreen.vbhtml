@ModelType AHReportingSystem.Models.LockScreenViewModel

@Code
    ViewBag.Title = "Session Locked"
    Layout = Nothing  ' LockScreen has its own full-page layout
End Code

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Session Locked — AH Reporting System</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    @System.Web.Optimization.Styles.Render("~/Content/adminlte")
    <style>
        body {
            background: linear-gradient(135deg, #006fa3 0%, #008bcc 50%, #00a8e0 100%);
            min-height: 100vh;
        }
        .lockscreen-box {
            margin-top: 0;
        }
        .lockscreen-logo a {
            color: #ffffff;
            font-weight: 700;
            font-size: 2rem;
            text-shadow: 0 2px 4px rgba(0,0,0,0.3);
        }
        .lockscreen-name {
            color: #ffffff;
        }
        .btn-primary {
            background-color: #008bcc !important;
            border-color: #007aad !important;
        }
        .lockscreen-item {
            border-radius: 8px;
        }
        .ah-lock-icon {
            font-size: 3rem;
            color: #008bcc;
            margin-bottom: 10px;
        }
    </style>
</head>
<body class="hold-transition lockscreen">

<div class="lockscreen-wrapper">

    <!-- Logo -->
    <div class="lockscreen-logo">
        <a href="#">
            <img src="@Url.Content("~/Content/img/ah-logo.png")"
                 alt="AH"
                 style="max-height:50px; margin-right:10px; vertical-align:middle;" />
            AH <b>Reporting</b>
        </a>
    </div>

    <!-- User locked info -->
    <div class="lockscreen-name">@Model.UserFullName</div>

    <!-- Lock screen box -->
    <div class="lockscreen-item">

        <div class="lockscreen-image">
            <i class="fas fa-user-lock" style="font-size:2rem; color:#008bcc;"></i>
        </div>

        @Using Html.BeginForm("LockScreen", "Account", FormMethod.Post)
            @Html.AntiForgeryToken()
            @Html.HiddenFor(Function(m) m.ReturnUrl)
            @Html.HiddenFor(Function(m) m.UserEmail)
            @Html.HiddenFor(Function(m) m.UserFullName)

            @<div class="input-group" style="min-width:220px;">
                @Html.PasswordFor(Function(m) m.Password,
                    New With {.class = "form-control",
                              .placeholder = "Enter your password to unlock",
                              .autofocus = "autofocus"})
                <div class="input-group-append">
                    <button type="submit" class="btn btn-warning">
                        <i class="fas fa-arrow-right"></i>
                    </button>
                </div>
            </div>

            @If Not Html.ViewData.ModelState.IsValid Then
                @<div class="text-danger small mt-2">
                    @Html.ValidationSummary(True)
                </div>
            End If

        End Using

    </div>

    <!-- Footer links -->
    <div class="help-block text-center mt-3">
        <a href="@Url.Action("LogOut", "Account")" class="text-white" style="opacity:0.8;"
           onclick="return confirm('Are you sure you want to log out completely?')">
            <i class="fas fa-sign-out-alt mr-1"></i> Sign out instead
        </a>
    </div>

    <div class="text-center mt-2" style="color:rgba(255,255,255,0.6); font-size:0.8rem;">
        Your session was locked due to inactivity.
    </div>

</div>

@System.Web.Optimization.Scripts.Render("~/bundles/jquery")
@System.Web.Optimization.Scripts.Render("~/bundles/jqueryval")
@System.Web.Optimization.Scripts.Render("~/bundles/adminlte")
</body>
</html>
