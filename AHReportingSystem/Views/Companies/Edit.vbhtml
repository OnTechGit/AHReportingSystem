@ModelType AHReportingSystem.Models.ViewModels.CompanyFormViewModel
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.Companies_Edit
    ViewBag.BreadcrumbParent = Strings.Companies_Title
End Code

<div class="row justify-content-center">
    <div class="col-md-6">
        <div class="card card-primary card-outline">
            <div class="card-header">
                <h3 class="card-title"><i class="fas fa-edit mr-2"></i> @Strings.Companies_Edit</h3>
            </div>
            <form action="@Url.Action("Edit")" method="post" novalidate="novalidate">
                @Html.AntiForgeryToken()
                @Html.HiddenFor(Function(m) m.CompanyId)
                <div class="card-body">
                    @Html.ValidationSummary(True, "", New With {.class = "alert alert-danger"})

                    <div class="form-group">
                        <label>@Strings.Companies_Col_Code</label>
                        @Html.TextBoxFor(Function(m) m.Code, New With {.class = "form-control text-uppercase", .maxlength = "20"})
                        <small class="form-text text-muted">@Strings.Companies_CodeHint</small>
                        @Html.ValidationMessageFor(Function(m) m.Code, "", New With {.class = "text-danger small"})
                    </div>

                    <div class="form-group">
                        <label>@Strings.Companies_Col_Name</label>
                        @Html.TextBoxFor(Function(m) m.Name, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(m) m.Name, "", New With {.class = "text-danger small"})
                    </div>

                    <div class="form-group">
                        <div class="custom-control custom-switch">
                            @Html.CheckBoxFor(Function(m) m.Active, New With {.class = "custom-control-input", .id = "Active"})
                            <label class="custom-control-label" for="Active">@Strings.Field_Active</label>
                        </div>
                    </div>
                </div>
                <div class="card-footer">
                    <button type="submit" class="btn btn-primary"><i class="fas fa-save mr-1"></i> @Strings.Common_Save</button>
                    <a href="@Url.Action("Index")" class="btn btn-default ml-2">@Strings.Common_Cancel</a>
                </div>
            </form>
        </div>
    </div>
</div>
