Option Strict On
Option Explicit On

Imports System.Web.Optimization

Namespace AHReportingSystem

    Public Class BundleConfig

        Public Shared Sub RegisterBundles(bundles As BundleCollection)

            ' =====================================================
            ' jQuery
            ' =====================================================
            bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"))

            bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"))

            ' =====================================================
            ' Bootstrap 3
            ' =====================================================
            bundles.Add(New ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"))

            bundles.Add(New StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css"))

            ' =====================================================
            ' AdminLTE 3.2
            ' =====================================================
            bundles.Add(New StyleBundle("~/Content/adminlte").Include(
                "~/Content/AdminLTE/css/adminlte.min.css"))

            bundles.Add(New ScriptBundle("~/bundles/adminlte").Include(
                "~/Scripts/bootstrap.bundle.min.js",
                "~/Content/AdminLTE/js/adminlte.min.js"))

            ' =====================================================
            ' DataTables
            ' =====================================================
            bundles.Add(New StyleBundle("~/Content/datatables").Include(
                "~/Content/datatables/dataTables.bootstrap4.min.css",
                "~/Content/datatables/buttons.bootstrap4.min.css"))

            bundles.Add(New ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/datatables/jquery.dataTables.min.js",
                "~/Scripts/datatables/dataTables.bootstrap4.min.js",
                "~/Scripts/datatables/dataTables.buttons.min.js",
                "~/Scripts/datatables/buttons.html5.min.js",
                "~/Scripts/datatables/jszip.min.js"))

            ' =====================================================
            ' Select2
            ' =====================================================
            bundles.Add(New StyleBundle("~/Content/select2").Include(
                "~/Content/select2/select2.min.css",
                "~/Content/select2/select2-bootstrap4.min.css"))

            bundles.Add(New ScriptBundle("~/bundles/select2").Include(
                "~/Scripts/select2/select2.full.min.js"))

            ' =====================================================
            ' Custom AH styles
            ' =====================================================
            bundles.Add(New StyleBundle("~/Content/ahcustom").Include(
                "~/Content/custom.css"))

            ' =====================================================
            ' Modernizr (keep separate — loads in <head>)
            ' =====================================================
            bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"))

#If Not DEBUG Then
            BundleTable.EnableOptimizations = True
#End If

        End Sub

    End Class

End Namespace
