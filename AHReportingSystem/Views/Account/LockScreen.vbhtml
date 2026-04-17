@ModelType AHReportingSystem.Models.LockScreenViewModel
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.LockScreen_Title
    Layout = Nothing
    Dim currentLang As String = CultureHelper.Current()
End Code

<!DOCTYPE html>
<html lang="@currentLang">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>@Strings.LockScreen_Title — @Strings.AppName</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback" />
    <link rel="stylesheet" href="@Url.Content("~/Content/fontawesome/css/all.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/bootstrap/css/bootstrap.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/AdminLTE/css/adminlte.min.css")" />
    <style>
        body { background: linear-gradient(135deg, #006fa3 0%, #008bcc 50%, #00a8e0 100%); min-height: 100vh; }
        .lockscreen-logo a { color: #ffffff; font-weight: 700; font-size: 2rem; text-shadow: 0 2px 4px rgba(0,0,0,0.3); }
        .lockscreen-name { color: #ffffff; }
        .btn-primary { background-color: #008bcc !important; border-color: #007aad !important; }
        .lockscreen-item { border-radius: 8px; }
    </style>
</head>
<body class="hold-transition lockscreen">

<div class="lockscreen-wrapper">

    <div class="lockscreen-logo">
        <a href="#">
            <img src="@Url.Content("~/Content/img/ah-logo.png")"
                 alt="AH"
                 style="max-height:50px; margin-right:10px; vertical-align:middle;" />
            @Strings.AppShortName
        </a>
    </div>

    <div class="lockscreen-name">@Model.UserFullName</div>

    <div class="lockscreen-item">

        <div class="lockscreen-image">
            <i class="fas fa-user-lock" style="font-size:2rem; color:#008bcc;"></i>
        </div>

        <form action="@Url.Action("LockScreen", "Account")" method="post">
            @Html.AntiForgeryToken()
            @Html.HiddenFor(Function(m) m.ReturnUrl)
            @Html.HiddenFor(Function(m) m.UserEmail)
            @Html.HiddenFor(Function(m) m.UserFullName)

            <div class="input-group" style="min-width:220px;">
                @Html.PasswordFor(Function(m) m.Password,
                    New With {.class = "form-control",
                              .placeholder = Strings.LockScreen_EnterPassword,
                              .autofocus = "autofocus"})
                <div class="input-group-append">
                    <button type="submit" class="btn btn-warning">
                        <i class="fas fa-arrow-right"></i>
                    </button>
                </div>
            </div>

            @If Not Html.ViewData.ModelState.IsValid Then
                @<div class="text-danger small mt-2">@Html.ValidationSummary(True)</div>
            End If
        </form>

    </div>

    <div class="help-block text-center mt-3">
        <a href="@Url.Action("LogOut", "Account")" class="text-white" style="opacity:0.8;"
           onclick="return confirm('@Strings.LockScreen_SignOutConfirm')">
            <i class="fas fa-sign-out-alt mr-1"></i> @Strings.LockScreen_SignOutInstead
        </a>
    </div>

    <div class="text-center mt-2" style="color:rgba(255,255,255,0.6); font-size:0.8rem;">
        @Strings.LockScreen_InactivityMsg
    </div>

</div>

<script src="@Url.Content("~/Scripts/jquery-3.7.1.min.js")"></script>
<script src="@Url.Content("~/Content/bootstrap/js/bootstrap.bundle.min.js")"></script>
<script src="@Url.Content("~/Content/AdminLTE/js/adminlte.min.js")"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.min.js")"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")"></script>
</body>
</html>
