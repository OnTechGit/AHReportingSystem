@ModelType AHReportingSystem.Models.LoginViewModel
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.SignIn_Title
    Layout = Nothing
    Dim returnUrl As String = CStr(ViewData("ReturnUrl"))
    Dim currentLang As String = CultureHelper.Current()
End Code

<!DOCTYPE html>
<html lang="@currentLang">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>@Strings.SignIn_Title — @Strings.AppName</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback" />
    <link rel="stylesheet" href="@Url.Content("~/Content/fontawesome/css/all.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/bootstrap/css/bootstrap.min.css")" />
    <link rel="stylesheet" href="@Url.Content("~/Content/AdminLTE/css/adminlte.min.css")" />
    <style>
        body { background: #f4f6f9; }
        .login-logo a { color: #008bcc; font-weight: 700; }
        .login-card-body { border-top: 3px solid #008bcc; }
        .btn-primary { background-color: #008bcc !important; border-color: #007aad !important; }
        .btn-primary:hover { background-color: #007aad !important; }
        .lang-switch { position: absolute; top: 15px; right: 20px; font-size: 0.9rem; }
        .lang-switch a { color: #666; text-decoration: none; margin-left: 8px; }
        .lang-switch a.active { color: #008bcc; font-weight: 600; }
    </style>
</head>
<body class="hold-transition login-page">

<div class="lang-switch">
    <i class="fas fa-globe"></i>
    <a href="@Url.Action("Set", "Culture", New With {.lang = "en", .returnUrl = Request.RawUrl})"
       class="@(If(currentLang = "en", "active", ""))">EN</a>
    <a href="@Url.Action("Set", "Culture", New With {.lang = "es", .returnUrl = Request.RawUrl})"
       class="@(If(currentLang = "es", "active", ""))">ES</a>
</div>

<div class="login-box">

    <div class="login-logo">
        <img src="@Url.Content("~/Content/img/ah-logo.png")"
             alt="Alternative Holdings"
             style="max-height:80px; margin-bottom:10px;" /><br />
        <a href="#">@Strings.AppShortName</a>
    </div>

    <div class="card">
        <div class="card-body login-card-body">
            <p class="login-box-msg">@Strings.SignIn_Prompt</p>

            <form action="@Url.Action("Login", "Account", New With {.ReturnUrl = returnUrl})" method="post" novalidate="novalidate">
                @Html.AntiForgeryToken()

                @Html.ValidationSummary(True, "", New With {.class = "alert alert-danger"})

                <div class="input-group mb-3">
                    @Html.TextBoxFor(Function(m) m.Email,
                        New With {.class = "form-control",
                                  .placeholder = Strings.SignIn_Email,
                                  .autocomplete = "email",
                                  .autofocus = "autofocus"})
                    <div class="input-group-append">
                        <div class="input-group-text"><span class="fas fa-envelope"></span></div>
                    </div>
                    @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger small"})
                </div>

                <div class="input-group mb-3">
                    @Html.PasswordFor(Function(m) m.Password,
                        New With {.class = "form-control",
                                  .placeholder = Strings.SignIn_Password,
                                  .autocomplete = "current-password"})
                    <div class="input-group-append">
                        <div class="input-group-text"><span class="fas fa-lock"></span></div>
                    </div>
                    @Html.ValidationMessageFor(Function(m) m.Password, "", New With {.class = "text-danger small"})
                </div>

                <div class="row">
                    <div class="col-8">
                        <div class="icheck-primary">
                            @Html.CheckBoxFor(Function(m) m.RememberMe)
                            <label for="RememberMe">@Strings.SignIn_RememberMe</label>
                        </div>
                    </div>
                    <div class="col-4">
                        <button type="submit" class="btn btn-primary btn-block">@Strings.SignIn_Button</button>
                    </div>
                </div>
            </form>

        </div>
    </div>

</div>

<script src="@Url.Content("~/Scripts/jquery-3.7.1.min.js")"></script>
<script src="@Url.Content("~/Content/bootstrap/js/bootstrap.bundle.min.js")"></script>
<script src="@Url.Content("~/Content/AdminLTE/js/adminlte.min.js")"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.min.js")"></script>
<script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")"></script>
</body>
</html>
