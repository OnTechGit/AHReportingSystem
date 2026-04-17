@Imports AHReportingSystem.Resources

@Code
    ViewBag.Title = Strings.Nav_Dashboard
End Code

<div class="row">
    <div class="col-12 col-sm-6 col-md-3">
        <div class="info-box">
            <span class="info-box-icon bg-primary elevation-1"><i class="fas fa-book"></i></span>
            <div class="info-box-content">
                <span class="info-box-text">@Strings.Dashboard_InfoSystemAccounts</span>
                <span class="info-box-number">—</span>
            </div>
        </div>
    </div>
    <div class="col-12 col-sm-6 col-md-3">
        <div class="info-box">
            <span class="info-box-icon bg-success elevation-1"><i class="fas fa-building"></i></span>
            <div class="info-box-content">
                <span class="info-box-text">@Strings.Dashboard_InfoCompanies</span>
                <span class="info-box-number">—</span>
            </div>
        </div>
    </div>
    <div class="col-12 col-sm-6 col-md-3">
        <div class="info-box">
            <span class="info-box-icon bg-warning elevation-1"><i class="fas fa-file-invoice-dollar"></i></span>
            <div class="info-box-content">
                <span class="info-box-text">@Strings.Dashboard_InfoGLPeriods</span>
                <span class="info-box-number">—</span>
            </div>
        </div>
    </div>
    <div class="col-12 col-sm-6 col-md-3">
        <div class="info-box">
            <span class="info-box-icon bg-danger elevation-1"><i class="fas fa-exclamation-triangle"></i></span>
            <div class="info-box-content">
                <span class="info-box-text">@Strings.Dashboard_InfoUnmapped</span>
                <span class="info-box-number">—</span>
            </div>
        </div>
    </div>
</div>

<div class="row">
    <div class="col-12">
        <div class="card card-primary card-outline">
            <div class="card-header">
                <h3 class="card-title">
                    <i class="fas fa-chart-bar mr-2"></i>
                    @Strings.Dashboard_Welcome
                </h3>
            </div>
            <div class="card-body">
                <p>@Strings.Dashboard_Intro</p>
                <p class="text-muted">
                    <strong>@Strings.Dashboard_GettingStarted</strong>
                    @Strings.Dashboard_GettingStartedText
                </p>
            </div>
        </div>
    </div>
</div>
