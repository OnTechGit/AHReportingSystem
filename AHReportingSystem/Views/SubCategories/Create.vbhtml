@ModelType AHReportingSystem.Models.ViewModels.SubCategoryFormViewModel
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.SubCategories_Create
    ViewBag.BreadcrumbParent = Strings.SubCategories_Title
    Dim cats As SelectList = DirectCast(ViewData("Categories"), SelectList)
End Code

<div class="row justify-content-center">
    <div class="col-md-6">
        <div class="card card-primary card-outline">
            <div class="card-header">
                <h3 class="card-title"><i class="fas fa-plus mr-2"></i> @Strings.SubCategories_Create</h3>
            </div>
            <form action="@Url.Action("Create")" method="post" novalidate="novalidate">
                @Html.AntiForgeryToken()
                <div class="card-body">
                    @Html.ValidationSummary(True, "", New With {.class = "alert alert-danger"})
                    <div class="form-group">
                        <label>@Strings.Accounts_Col_Category</label>
                        @Html.DropDownListFor(Function(m) m.CategoryId, cats, Strings.Common_SelectOne, New With {.class = "form-control select2"})
                        @Html.ValidationMessageFor(Function(m) m.CategoryId, "", New With {.class = "text-danger small"})
                    </div>
                    <div class="form-group">
                        <label>@Strings.Field_Name</label>
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

@Section scripts
    <script>$(function () { $('.select2').select2({ theme: 'bootstrap4' }); });</script>
End Section
