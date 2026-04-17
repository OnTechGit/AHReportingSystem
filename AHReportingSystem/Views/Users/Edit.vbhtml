@ModelType AHReportingSystem.Models.ViewModels.EditUserViewModel
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.Users_Edit
    ViewBag.BreadcrumbParent = Strings.Users_Title
    Dim roles As IEnumerable(Of SelectListItem) = DirectCast(ViewData("Roles"), IEnumerable(Of SelectListItem))
End Code

<div class="row justify-content-center">
    <div class="col-md-8">

        <div class="card card-primary card-outline">
            <div class="card-header">
                <h3 class="card-title">
                    <i class="fas fa-user-edit mr-2"></i> @Strings.Users_Edit
                </h3>
            </div>

            <form action="@Url.Action("Edit")" method="post" novalidate="novalidate">
                @Html.AntiForgeryToken()
                @Html.HiddenFor(Function(m) m.Id)
                <div class="card-body">

                    @Html.ValidationSummary(True, "", New With {.class = "alert alert-danger"})

                    <div class="form-group">
                        <label>@Strings.Users_Col_FullName</label>
                        @Html.TextBoxFor(Function(m) m.FullName, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(m) m.FullName, "", New With {.class = "text-danger small"})
                    </div>

                    <div class="form-group">
                        <label>@Strings.Users_Col_Email</label>
                        @Html.TextBoxFor(Function(m) m.Email, New With {.class = "form-control", .type = "email"})
                        @Html.ValidationMessageFor(Function(m) m.Email, "", New With {.class = "text-danger small"})
                    </div>

                    <div class="form-group">
                        <label>@Strings.Users_Col_Role</label>
                        @Html.DropDownListFor(Function(m) m.Role, roles, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(m) m.Role, "", New With {.class = "text-danger small"})
                    </div>

                    <div class="form-group">
                        <div class="custom-control custom-switch">
                            @Html.CheckBoxFor(Function(m) m.IsActive, New With {.class = "custom-control-input", .id = "IsActive"})
                            <label class="custom-control-label" for="IsActive">@Strings.Users_Col_Active</label>
                        </div>
                    </div>
                </div>

                <div class="card-footer">
                    <button type="submit" class="btn btn-primary">
                        <i class="fas fa-save mr-1"></i> @Strings.Common_Save
                    </button>
                    <a href="@Url.Action("Index")" class="btn btn-default ml-2">@Strings.Common_Cancel</a>
                </div>
            </form>

        </div>

        <div class="card card-warning card-outline mt-3">
            <div class="card-header">
                <h3 class="card-title">
                    <i class="fas fa-key mr-2"></i> @Strings.Users_ResetPassword
                </h3>
            </div>
            <form action="@Url.Action("ResetPassword")" method="post" novalidate="novalidate">
                @Html.AntiForgeryToken()
                <input type="hidden" name="Id" value="@Model.Id" />
                <div class="card-body">
                    <div class="form-group">
                        <label>@Strings.ChangePassword_New</label>
                        <input type="password" name="NewPassword" class="form-control" required="required" minlength="8" />
                        <small class="form-text text-muted">@Strings.Users_InitialPassword_Hint @Strings.ChangePassword_Requirements</small>
                    </div>
                </div>
                <div class="card-footer">
                    <button type="submit" class="btn btn-warning">
                        <i class="fas fa-key mr-1"></i> @Strings.Users_ResetPassword
                    </button>
                </div>
            </form>
        </div>

    </div>
</div>
