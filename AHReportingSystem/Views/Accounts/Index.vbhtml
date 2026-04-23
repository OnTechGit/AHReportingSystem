@ModelType IEnumerable(Of AHReportingSystem.Models.ViewModels.SystemAccountListItemViewModel)
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.Accounts_Title
    ViewBag.BreadcrumbParent = Strings.Nav_ChartOfAccounts
End Code

<div class="card">
    <div class="card-header">
        <h3 class="card-title"><i class="fas fa-book mr-2"></i> @Strings.Accounts_Title</h3>
        <div class="card-tools">
            <a href="@Url.Action("ExportExcel")" class="btn btn-outline-success btn-sm mr-1">
                <i class="fas fa-file-excel mr-1"></i> @Strings.Common_ExportExcel
            </a>
            <a href="@Url.Action("Create")" class="btn btn-primary btn-sm">
                <i class="fas fa-plus mr-1"></i> @Strings.Accounts_New
            </a>
        </div>
    </div>
    <div class="card-body">
        <table id="tbl" class="table table-hover table-striped" style="width:100%">
            <thead>
                <tr>
                    <th>@Strings.Accounts_Col_Id</th>
                    <th>@Strings.Accounts_Col_Name</th>
                    <th>@Strings.Accounts_Col_Category</th>
                    <th>@Strings.Accounts_Col_SubCategory</th>
                    <th>@Strings.Accounts_Col_Grouping</th>
                    <th class="text-center">@Strings.Field_Active</th>
                    <th class="text-right">@Strings.Users_Col_Actions</th>
                </tr>
            </thead>
            <tbody>
                @For Each a In Model
                    Dim badgeClass As String = If(a.Active, "badge-success", "badge-secondary")
                    Dim badgeText As String = If(a.Active, Strings.Common_Yes, Strings.Common_No)
                    Dim toggleClass As String = If(a.Active, "btn-outline-warning", "btn-outline-success")
                    Dim toggleTitle As String = If(a.Active, Strings.Users_Deactivate, Strings.Users_Activate)
                    Dim toggleIcon As String = If(a.Active, "fa-ban", "fa-check")
                    @<tr>
                        <td>@a.IdCuenta</td>
                        <td>@a.Cuenta</td>
                        <td>@a.CategoryName</td>
                        <td>@a.SubCategoryName</td>
                        <td>@a.GroupingName</td>
                        <td class="text-center"><span class="badge @badgeClass">@badgeText</span></td>
                        <td class="text-right">
                            <a href="@Url.Action("Edit", New With {.id = a.IdCuenta})" class="btn btn-sm btn-outline-primary" title="@Strings.Common_Edit">
                                <i class="fas fa-edit"></i>
                            </a>
                            <form action="@Url.Action("ToggleActive", New With {.id = a.IdCuenta})" method="post" class="d-inline">
                                @Html.AntiForgeryToken()
                                <button type="submit" class="btn btn-sm @toggleClass" title="@toggleTitle">
                                    <i class="fas @toggleIcon"></i>
                                </button>
                            </form>
                        </td>
                    </tr>
                Next
            </tbody>
        </table>
    </div>
</div>

@Section scripts
    <script>
        $(function () {
            $('#tbl').DataTable({
                pageLength: 25,
                order: [[0, 'asc']]
            });
        });
    </script>
End Section
