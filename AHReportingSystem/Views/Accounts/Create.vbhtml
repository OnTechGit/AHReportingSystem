@ModelType AHReportingSystem.Models.ViewModels.SystemAccountFormViewModel
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.Accounts_Create
    ViewBag.BreadcrumbParent = Strings.Accounts_Title
    Dim cats As SelectList = DirectCast(ViewData("Categories"), SelectList)
    Dim groups As SelectList = DirectCast(ViewData("Groupings"), SelectList)
    Dim subsJson As String = System.Web.Helpers.Json.Encode(ViewData("AllSubCategories"))
End Code

<div class="row justify-content-center">
    <div class="col-md-8">
        <div class="card card-primary card-outline">
            <div class="card-header">
                <h3 class="card-title"><i class="fas fa-plus mr-2"></i> @Strings.Accounts_Create</h3>
            </div>
            <form action="@Url.Action("Create")" method="post" novalidate="novalidate">
                @Html.AntiForgeryToken()
                <div class="card-body">
                    @Html.ValidationSummary(True, "", New With {.class = "alert alert-danger"})

                    <div class="form-group">
                        <label>@Strings.Accounts_Col_Id</label>
                        @Html.TextBoxFor(Function(m) m.IdCuenta, New With {.class = "form-control text-uppercase", .maxlength = "20", .autofocus = "autofocus"})
                        @Html.ValidationMessageFor(Function(m) m.IdCuenta, "", New With {.class = "text-danger small"})
                    </div>

                    <div class="form-group">
                        <label>@Strings.Accounts_Col_Name</label>
                        @Html.TextBoxFor(Function(m) m.Cuenta, New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(m) m.Cuenta, "", New With {.class = "text-danger small"})
                    </div>

                    <div class="form-group">
                        <label>@Strings.Accounts_Col_Category</label>
                        @Html.DropDownListFor(Function(m) m.CategoryId, cats, Strings.Common_SelectOne, New With {.class = "form-control select2", .id = "CategoryId"})
                        @Html.ValidationMessageFor(Function(m) m.CategoryId, "", New With {.class = "text-danger small"})
                    </div>

                    <div class="form-group">
                        <label>@Strings.Accounts_Col_SubCategory</label>
                        <select id="SubCategoryId" name="SubCategoryId" class="form-control select2" data-selected="@Model.SubCategoryId">
                            <option value="">@Strings.Common_SelectOne</option>
                        </select>
                        @Html.ValidationMessageFor(Function(m) m.SubCategoryId, "", New With {.class = "text-danger small"})
                    </div>

                    <div class="form-group">
                        <label>@Strings.Accounts_Col_Grouping</label>
                        @Html.DropDownListFor(Function(m) m.GroupingId, groups, Strings.Common_SelectOne, New With {.class = "form-control select2"})
                        @Html.ValidationMessageFor(Function(m) m.GroupingId, "", New With {.class = "text-danger small"})
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
    <script>
        (function () {
            var allSubs = @Html.Raw(subsJson);
            var placeholder = '@Strings.Common_SelectOne';

            function populateSubs(catId) {
                var $sc = $('#SubCategoryId');
                var prev = $sc.data('selected');
                $sc.empty().append('<option value="">' + placeholder + '</option>');
                allSubs.filter(function (x) { return String(x.CategoryId) === String(catId); })
                       .forEach(function (x) {
                    $sc.append('<option value="' + x.SubCategoryId + '">' + x.Name + '</option>');
                });
                if (prev) $sc.val(prev).trigger('change.select2');
            }

            $(function () {
                $('.select2').select2({ theme: 'bootstrap4' });
                $('#CategoryId').on('change', function () { populateSubs($(this).val()); });
                var initial = $('#CategoryId').val();
                if (initial) populateSubs(initial);
            });
        })();
    </script>
End Section
