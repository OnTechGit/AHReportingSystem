@ModelType AHReportingSystem.Models.LoginViewModel

@Code
    ViewBag.Title = "Sign In"
    Layout = Nothing  ' Login uses its own layout, not the AdminLTE sidebar layout
End Code

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Sign In — AH Reporting System</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" />
    @System.Web.Optimization.Styles.Render("~/Content/adminlte")
    <style>
        :root {
            --primary: #008bcc;
        }
        body {
            background: #f4f6f9;
        }
        .login-logo a {
            color: #008bcc;
            font-weight: 700;
        }
        .login-card-body {
            border-top: 3px solid #008bcc;
        }
        .btn-primary {
            background-color: #008bcc !important;
            border-color: #007aad !important;
        }
        .btn-primary:hover {
            background-color: #007aad !important;
        }
    </style>
</head>
<body class="hold-transition login-page">
<div class="login-box">

    <!-- Logo -->
    <div class="login-logo">
        <img src="@Url.Content("~/Content/img/ah-logo.png")"
             alt="Alternative Holdings"
             style="max-height:80px; margin-bottom:10px;" /><br />
        <a href="#">AH <b>Reporting</b></a>
    </div>

    <!-- Login card -->
    <div class="card">
        <div class="card-body login-card-body">
            <p class="login-box-msg">Sign in to start your session</p>

            @Using Html.BeginForm("Login", "Account", New With {.ReturnUrl = ViewBag.ReturnUrl}, FormMethod.Post)
                @Html.AntiForgeryToken()

                @<div class="@(If(Html.ValidationSummary(True, "", New With {.class = "text-danger"}), ""))">
                    @Html.ValidationSummary(True, "", New With {.class = "alert alert-danger"})
                </div>

                @* Email *@
                <div class="input-group mb-3">
                    @Html.TextBoxFor(Function(m) m.Email,
                        New With {.class = "form-control",
                                  .placeholder = "Email address",
                                  .autocomplete = "email",
                                  .autofocus = "autofocus"})
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-envelope"></span>
                        </div>
                    </div>
                    @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger small"})
                </div>

                @* Password *@
                <div class="input-group mb-3">
                    @Html.PasswordFor(Function(m) m.Password,
                        New With {.class = "form-control",
                                  .placeholder = "Password",
                                  .autocomplete = "current-password"})
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-lock"></span>
                        </div>
                    </div>
                    @Html.ValidationMessageFor(Function(m) m.Password, "", New With {.class = "text-danger small"})
                </div>

                @* Remember me + Submit *@
                <div class="row">
                    <div class="col-8">
                        <div class="icheck-primary">
                            @Html.CheckBoxFor(Function(m) m.RememberMe)
                            <label for="RememberMe">Remember Me</label>
                        </div>
                    </div>
                    <div class="col-4">
                        <button type="submit" class="btn btn-primary btn-block">
                            Sign In
                        </button>
                    </div>
                </div>

            End Using

        </div>
    </div>

</div>

@System.Web.Optimization.Scripts.Render("~/bundles/jquery")
@System.Web.Optimization.Scripts.Render("~/bundles/jqueryval")
@System.Web.Optimization.Scripts.Render("~/bundles/adminlte")
</body>
</html>
