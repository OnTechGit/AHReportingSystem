Option Strict On
Option Explicit On

Imports System.Web.Optimization

''' <summary>
''' Registers style / script bundles for the entire application.
''' AdminLTE 3.2 is built on Bootstrap 4, so Bootstrap 3 assets are NOT used.
''' </summary>
Public Class BundleConfig

        Public Shared Sub RegisterBundles(bundles As BundleCollection)

            ' =====================================================
            ' jQuery (required by everything below)
            ' =====================================================
            bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"))

            bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"))

            ' =====================================================
            ' Font Awesome 5 (local)
            ' =====================================================
            bundles.Add(New StyleBundle("~/Content/fontawesome").Include(
                "~/Content/fontawesome/css/all.min.css"))

            ' =====================================================
            ' AdminLTE 3.2 + Bootstrap 4 (AdminLTE's host framework)
            ' =====================================================
            bundles.Add(New StyleBundle("~/Content/adminlte").Include(
                "~/Content/bootstrap/css/bootstrap.min.css",
                "~/Content/AdminLTE/css/adminlte.min.css"))

            bundles.Add(New ScriptBundle("~/bundles/adminlte").Include(
                "~/Content/bootstrap/js/bootstrap.bundle.min.js",
                "~/Content/AdminLTE/js/adminlte.min.js"))

            ' =====================================================
            ' DataTables (Bootstrap 4 styling, with HTML5 buttons)
            ' =====================================================
            bundles.Add(New StyleBundle("~/Content/datatables").Include(
                "~/Content/datatables/dataTables.bootstrap4.min.css",
                "~/Content/datatables/buttons.bootstrap4.min.css"))

            bundles.Add(New ScriptBundle("~/bundles/datatables").Include(
                "~/Scripts/datatables/jquery.dataTables.min.js",
                "~/Scripts/datatables/dataTables.bootstrap4.min.js",
                "~/Scripts/datatables/dataTables.buttons.min.js",
                "~/Scripts/datatables/buttons.bootstrap4.min.js",
                "~/Scripts/datatables/jszip.min.js",
                "~/Scripts/datatables/buttons.html5.min.js"))

            ' =====================================================
            ' Select2 (+ Bootstrap 4 theme)
            ' =====================================================
            bundles.Add(New StyleBundle("~/Content/select2").Include(
                "~/Content/select2/select2.min.css",
                "~/Content/select2/select2-bootstrap4.min.css"))

            bundles.Add(New ScriptBundle("~/bundles/select2").Include(
                "~/Scripts/select2/select2.full.min.js"))

            ' =====================================================
            ' SweetAlert2
            ' =====================================================
            bundles.Add(New StyleBundle("~/Content/sweetalert2").Include(
                "~/Content/sweetalert2/sweetalert2.min.css"))

            bundles.Add(New ScriptBundle("~/bundles/sweetalert2").Include(
                "~/Scripts/sweetalert2.all.min.js"))

            ' =====================================================
            ' Custom AH styles (color override #008bcc, etc.)
            ' =====================================================
            bundles.Add(New StyleBundle("~/Content/ahcustom").Include(
                "~/Content/custom.css"))

#If Not DEBUG Then
            BundleTable.EnableOptimizations = True
#End If

    End Sub

End Class
