@ModelType IEnumerable(Of AHReportingSystem.Models.ViewModels.CategoryListItemViewModel)
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.Categories_Title
    ViewBag.BreadcrumbParent = Strings.Nav_ChartOfAccounts
End Code

<div class="card">
    <div class="card-header">
        <h3 class="card-title"><i class="fas fa-tags mr-2"></i> @Strings.Categories_Title</h3>
        <div class="card-tools">
            <a href="@Url.Action("Create")" class="btn btn-primary btn-sm">
                <i class="fas fa-plus mr-1"></i> @Strings.Categories_New
            </a>
        </div>
    </div>
    <div class="card-body">
        <table id="tbl" class="table table-hover table-striped" style="width:100%">
            <thead>
                <tr>
                    <th>@Strings.Field_Name</th>
                    <th class="text-center">@Strings.SubCategories_Title</th>
                    <th class="text-center">@Strings.Accounts_Title</th>
                    <th class="text-center">@Strings.Field_Active</th>
                    <th class="text-right">@Strings.Users_Col_Actions</th>
                </tr>
            </thead>
            <tbody>
                @For Each c In Model
                    Dim badgeClass As String = If(c.Active, "badge-success", "badge-secondary")
                    Dim badgeText As String = If(c.Active, Strings.Common_Yes, Strings.Common_No)
                    Dim toggleClass As String = If(c.Active, "btn-outline-warning", "btn-outline-success")
                    Dim toggleTitle As String = If(c.Active, Strings.Users_Deactivate, Strings.Users_Activate)
                    Dim toggleIcon As String = If(c.Active, "fa-ban", "fa-check")
                    @<tr>
                        <td>@c.Name</td>
                        <td class="text-center">@c.SubCategoryCount</td>
                        <td class="text-center">@c.AccountCount</td>
                        <td class="text-center"><span class="badge @badgeClass">@badgeText</span></td>
                        <td class="text-right">
                            <a href="@Url.Action("Edit", New With {.id = c.CategoryId})" class="btn btn-sm btn-outline-primary" title="@Strings.Common_Edit">
                                <i class="fas fa-edit"></i>
                            </a>
                            <form action="@Url.Action("ToggleActive", New With {.id = c.CategoryId})" method="post" class="d-inline">
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
        $(function () { $('#tbl').DataTable({ pageLength: 25, order: [[0, 'asc']] }); });
    </script>
End Section
