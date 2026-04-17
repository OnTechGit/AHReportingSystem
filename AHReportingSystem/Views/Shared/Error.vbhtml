@ModelType System.Web.Mvc.HandleErrorInfo
@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.Error_Title
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="row">
    <div class="col-12">
        <div class="card card-danger">
            <div class="card-header">
                <h3 class="card-title">
                    <i class="fas fa-exclamation-triangle mr-2"></i>
                    @Strings.Error_Title
                </h3>
            </div>
            <div class="card-body">
                <p class="text-muted">@Strings.Error_Description</p>
            </div>
            <div class="card-footer">
                <a href="@Url.Action("Index", "Home")" class="btn btn-primary">
                    <i class="fas fa-home mr-1"></i> @Strings.Error_ReturnToDashboard
                </a>
            </div>
        </div>
    </div>
</div>
