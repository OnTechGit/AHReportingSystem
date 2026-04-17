@ModelType IEnumerable(Of AHReportingSystem.Models.ViewModels.UserListItemViewModel)
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.Users_Title
    ViewBag.BreadcrumbParent = Strings.Nav_Administration
End Code

<div class="card">
    <div class="card-header">
        <h3 class="card-title">
            <i class="fas fa-users-cog mr-2"></i> @Strings.Users_Title
        </h3>
        <div class="card-tools">
            <a href="@Url.Action("Create")" class="btn btn-primary btn-sm">
                <i class="fas fa-plus mr-1"></i> @Strings.Users_New
            </a>
        </div>
    </div>

    <div class="card-body">
        <table id="tblUsers" class="table table-hover table-striped" style="width:100%">
            <thead>
                <tr>
                    <th>@Strings.Users_Col_FullName</th>
                    <th>@Strings.Users_Col_Email</th>
                    <th>@Strings.Users_Col_Role</th>
                    <th class="text-center">@Strings.Users_Col_Active</th>
                    <th>@Strings.Users_Col_LastLogin</th>
                    <th class="text-right">@Strings.Users_Col_Actions</th>
                </tr>
            </thead>
            <tbody>
                @For Each u In Model
                    Dim activeBadge As String = If(u.IsActive, "badge-success", "badge-secondary")
                    Dim activeLabel As String = If(u.IsActive, Strings.Common_Yes, Strings.Common_No)
                    Dim lastLoginText As String = If(u.LastLogin.HasValue, u.LastLogin.Value.ToString("yyyy-MM-dd HH:mm"), Strings.Common_Never)
                    Dim toggleBtnClass As String = If(u.IsActive, "btn-outline-warning", "btn-outline-success")
                    Dim toggleTitle As String = If(u.IsActive, Strings.Users_Deactivate, Strings.Users_Activate)
                    Dim toggleIcon As String = If(u.IsActive, "fa-user-slash", "fa-user-check")
                    Dim toggleDisabled As Boolean = u.IsSelf AndAlso u.IsActive
                    @<tr>
                        <td>@u.FullName</td>
                        <td>@u.Email</td>
                        <td>@Strings.RoleLabel(u.Role)</td>
                        <td class="text-center"><span class="badge @activeBadge">@activeLabel</span></td>
                        <td>@lastLoginText</td>
                        <td class="text-right">
                            <a href="@Url.Action("Edit", New With {.id = u.Id})" class="btn btn-sm btn-outline-primary" title="@Strings.Common_Edit">
                                <i class="fas fa-edit"></i>
                            </a>
                            <form action="@Url.Action("ToggleActive", New With {.id = u.Id})" method="post" class="d-inline">
                                @Html.AntiForgeryToken()
                                <button type="submit" class="btn btn-sm @toggleBtnClass" title="@toggleTitle" disabled="@toggleDisabled">
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
            $('#tblUsers').DataTable({
                pageLength: 25,
                order: [[0, 'asc']],
                language: {
                    search: '@Strings.Common_Search:'
                }
            });
        });
    </script>
End Section
