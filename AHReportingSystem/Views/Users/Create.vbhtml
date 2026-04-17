@ModelType AHReportingSystem.Models.ViewModels.CreateUserViewModel
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.Users_Create
    ViewBag.BreadcrumbParent = Strings.Users_Title
    Dim roles As IEnumerable(Of SelectListItem) = DirectCast(ViewData("Roles"), IEnumerable(Of SelectListItem))
End Code

<div class="row justify-content-center">
    <div class="col-md-8">
        <div class="card card-primary card-outline">
            <div class="card-header">
                <h3 class="card-title">
                    <i class="fas fa-user-plus mr-2"></i> @Strings.Users_Create
                </h3>
            </div>

            <form action="@Url.Action("Create")" method="post" novalidate="novalidate">
                @Html.AntiForgeryToken()
                <div class="card-body">

                    @Html.ValidationSummary(True, "", New With {.class = "alert alert-danger"})

                    <div class="form-group">
                        <label>@Strings.Users_Col_FullName</label>
                        @Html.TextBoxFor(Function(m) m.FullName, New With {.class = "form-control", .autofocus = "autofocus"})
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
                        <label>@Strings.Users_InitialPassword</label>
                        @Html.PasswordFor(Function(m) m.InitialPassword, New With {.class = "form-control"})
                        <small class="form-text text-muted">@Strings.Users_InitialPassword_Hint @Strings.ChangePassword_Requirements</small>
                        @Html.ValidationMessageFor(Function(m) m.InitialPassword, "", New With {.class = "text-danger small"})
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
    </div>
</div>
