@Code
    ViewBag.Title = "Dashboard"
End Code

<!-- Info boxes row -->
<div class="row">
    <div class="col-12 col-sm-6 col-md-3">
        <div class="info-box">
            <span class="info-box-icon bg-primary elevation-1">
                <i class="fas fa-book"></i>
            </span>
            <div class="info-box-content">
                <span class="info-box-text">System Accounts</span>
                <span class="info-box-number">—</span>
            </div>
        </div>
    </div>
    <div class="col-12 col-sm-6 col-md-3">
        <div class="info-box">
            <span class="info-box-icon bg-success elevation-1">
                <i class="fas fa-building"></i>
            </span>
            <div class="info-box-content">
                <span class="info-box-text">Companies</span>
                <span class="info-box-number">—</span>
            </div>
        </div>
    </div>
    <div class="col-12 col-sm-6 col-md-3">
        <div class="info-box">
            <span class="info-box-icon bg-warning elevation-1">
                <i class="fas fa-file-invoice-dollar"></i>
            </span>
            <div class="info-box-content">
                <span class="info-box-text">GL Periods Loaded</span>
                <span class="info-box-number">—</span>
            </div>
        </div>
    </div>
    <div class="col-12 col-sm-6 col-md-3">
        <div class="info-box">
            <span class="info-box-icon bg-danger elevation-1">
                <i class="fas fa-exclamation-triangle"></i>
            </span>
            <div class="info-box-content">
                <span class="info-box-text">Unmapped Accounts</span>
                <span class="info-box-number">—</span>
            </div>
        </div>
    </div>
</div>

<!-- Welcome card -->
<div class="row">
    <div class="col-12">
        <div class="card card-primary card-outline">
            <div class="card-header">
                <h3 class="card-title">
                    <i class="fas fa-chart-bar mr-2"></i>
                    Welcome to AH Reporting System
                </h3>
            </div>
            <div class="card-body">
                <p>
                    This system consolidates financial data from all Alternative Holdings companies,
                    enabling unified reporting across the group.
                </p>
                <p class="text-muted">
                    <strong>Getting started:</strong>
                    Begin by setting up the <a href="@Url.Action("Index", "Accounts")">Chart of Accounts</a>
                    and account categories, then load General Ledger data for each company.
                </p>
            </div>
        </div>
    </div>
</div>
