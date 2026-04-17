@ModelType System.Web.Mvc.HandleErrorInfo

@Code
    ViewBag.Title = "Error"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div class="row">
    <div class="col-12">
        <div class="card card-danger">
            <div class="card-header">
                <h3 class="card-title">
                    <i class="fas fa-exclamation-triangle mr-2"></i>
                    An Error Occurred
                </h3>
            </div>
            <div class="card-body">
                <p class="text-muted">
                    An unexpected error occurred while processing your request.
                    Please try again or contact your system administrator if the problem persists.
                </p>
            </div>
            <div class="card-footer">
                <a href="@Url.Action("Index", "Home")" class="btn btn-primary">
                    <i class="fas fa-home mr-1"></i> Return to Dashboard
                </a>
            </div>
        </div>
    </div>
</div>
