@ModelType AHReportingSystem.Models.ChangePasswordViewModel
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.ChangePassword_Title
    Dim forced As Boolean = CBool(ViewData("Forced"))
End Code

<div class="row justify-content-center">
    <div class="col-md-6">

        @If forced Then
            @<div class="alert alert-warning">
                <i class="fas fa-lock mr-2"></i> @Strings.ChangePassword_Forced
            </div>
        End If

        <div class="card card-primary card-outline">
            <div class="card-header">
                <h3 class="card-title">
                    <i class="fas fa-key mr-2"></i> @Strings.ChangePassword_Title
                </h3>
            </div>

            <form action="@Url.Action("ChangePassword", "Account")" method="post" novalidate="novalidate">
                @Html.AntiForgeryToken()
                <div class="card-body">

                    @Html.ValidationSummary(True, "", New With {.class = "alert alert-danger"})

                    <div class="form-group">
                        <label>@Strings.ChangePassword_Current</label>
                        @Html.PasswordFor(Function(m) m.OldPassword, New With {.class = "form-control", .autocomplete = "current-password", .autofocus = "autofocus"})
                        @Html.ValidationMessageFor(Function(m) m.OldPassword, "", New With {.class = "text-danger small"})
                    </div>

                    <div class="form-group">
                        <label>@Strings.ChangePassword_New</label>
                        @Html.PasswordFor(Function(m) m.NewPassword, New With {.class = "form-control", .autocomplete = "new-password"})
                        <small class="form-text text-muted">@Strings.ChangePassword_Requirements</small>
                        @Html.ValidationMessageFor(Function(m) m.NewPassword, "", New With {.class = "text-danger small"})
                    </div>

                    <div class="form-group">
                        <label>@Strings.ChangePassword_Confirm</label>
                        @Html.PasswordFor(Function(m) m.ConfirmPassword, New With {.class = "form-control", .autocomplete = "new-password"})
                        @Html.ValidationMessageFor(Function(m) m.ConfirmPassword, "", New With {.class = "text-danger small"})
                    </div>
                </div>

                <div class="card-footer">
                    <button type="submit" class="btn btn-primary">
                        <i class="fas fa-save mr-1"></i> @Strings.ChangePassword_Submit
                    </button>
                    @If Not forced Then
                        @<a href="@Url.Action("Index", "Home")" class="btn btn-default ml-2">@Strings.Common_Cancel</a>
                    End If
                </div>
            </form>

        </div>
    </div>
</div>
