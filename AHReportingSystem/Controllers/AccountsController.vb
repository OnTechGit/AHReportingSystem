Option Strict On
Option Explicit On

Imports System.Data.Entity
Imports System.IO
Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports AHReportingSystem.Models
Imports AHReportingSystem.Models.ViewModels
Imports AHReportingSystem.Resources

Namespace Controllers

    ''' <summary>
    ''' SystemAccount catalog (chart of accounts). Admin + Supervisor only.
    ''' Soft-delete via Active flag. IdCuenta is a manually assigned integer PK.
    ''' </summary>
    <Authorize(Roles:="Admin,Supervisor")>
    Public Class AccountsController
        Inherits BaseController

        Private ReadOnly _db As New ApplicationDbContext()

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then _db.Dispose()
            MyBase.Dispose(disposing)
        End Sub

        ' ========== GET: /Accounts =============================
        Public Async Function Index() As Task(Of ActionResult)
            Dim list = Await _db.SystemAccounts _
                .OrderBy(Function(a) a.IdCuenta) _
                .Select(Function(a) New SystemAccountListItemViewModel With {
                    .IdCuenta = a.IdCuenta,
                    .Cuenta = a.Cuenta,
                    .CategoryId = a.CategoryId,
                    .CategoryName = a.Category.Name,
                    .SubCategoryId = a.SubCategoryId,
                    .SubCategoryName = a.SubCategory.Name,
                    .GroupingId = a.GroupingId,
                    .GroupingName = a.Grouping.Name,
                    .Active = a.Active
                }).ToListAsync()
            Return View(list)
        End Function

        ' ========== GET: /Accounts/Create =====================
        Public Async Function Create() As Task(Of ActionResult)
            Await PopulateLookups(Nothing, Nothing)
            Return View(New SystemAccountFormViewModel() With {.Active = True, .IsEdit = False})
        End Function

        ' ========== POST: /Accounts/Create ====================
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Create(model As SystemAccountFormViewModel) As Task(Of ActionResult)
            model.IsEdit = False
            model.IdCuenta = If(model.IdCuenta, "").Trim().ToUpperInvariant()
            If Not ModelState.IsValid Then
                Await PopulateLookups(model.CategoryId, model.GroupingId)
                Return View(model)
            End If

            If Await _db.SystemAccounts.AnyAsync(Function(a) a.IdCuenta = model.IdCuenta) Then
                ModelState.AddModelError("IdCuenta", Strings.Accounts_IdTaken)
                Await PopulateLookups(model.CategoryId, model.GroupingId)
                Return View(model)
            End If

            If Not Await ValidateSubCategoryBelongsToCategory(model.CategoryId, model.SubCategoryId) Then
                ModelState.AddModelError("SubCategoryId", Strings.Accounts_SubCategoryMismatch)
                Await PopulateLookups(model.CategoryId, model.GroupingId)
                Return View(model)
            End If

            _db.SystemAccounts.Add(New SystemAccount With {
                .IdCuenta = model.IdCuenta,
                .Cuenta = model.Cuenta,
                .CategoryId = model.CategoryId,
                .SubCategoryId = model.SubCategoryId,
                .GroupingId = model.GroupingId,
                .Active = model.Active,
                .CreatedBy = CurrentUserName,
                .CreatedAt = DateTime.UtcNow
            })
            Await _db.SaveChangesAsync()
            SetSuccessMessage(Strings.Common_Created)
            Return RedirectToAction("Index")
        End Function

        ' ========== GET: /Accounts/Edit/{id} ==================
        Public Async Function Edit(id As String) As Task(Of ActionResult)
            If String.IsNullOrWhiteSpace(id) Then Return HttpNotFound()
            Dim entity = Await _db.SystemAccounts.FindAsync(id)
            If entity Is Nothing Then Return HttpNotFound()

            Await PopulateLookups(entity.CategoryId, entity.GroupingId)
            Return View(New SystemAccountFormViewModel With {
                .IdCuenta = entity.IdCuenta,
                .Cuenta = entity.Cuenta,
                .CategoryId = entity.CategoryId,
                .SubCategoryId = entity.SubCategoryId,
                .GroupingId = entity.GroupingId,
                .Active = entity.Active,
                .IsEdit = True
            })
        End Function

        ' ========== POST: /Accounts/Edit ======================
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(model As SystemAccountFormViewModel) As Task(Of ActionResult)
            model.IsEdit = True
            model.IdCuenta = If(model.IdCuenta, "").Trim().ToUpperInvariant()
            If Not ModelState.IsValid Then
                Await PopulateLookups(model.CategoryId, model.GroupingId)
                Return View(model)
            End If

            Dim entity = Await _db.SystemAccounts.FindAsync(model.IdCuenta)
            If entity Is Nothing Then Return HttpNotFound()

            If Not Await ValidateSubCategoryBelongsToCategory(model.CategoryId, model.SubCategoryId) Then
                ModelState.AddModelError("SubCategoryId", Strings.Accounts_SubCategoryMismatch)
                Await PopulateLookups(model.CategoryId, model.GroupingId)
                Return View(model)
            End If

            entity.Cuenta = model.Cuenta
            entity.CategoryId = model.CategoryId
            entity.SubCategoryId = model.SubCategoryId
            entity.GroupingId = model.GroupingId
            entity.Active = model.Active
            entity.ModifiedBy = CurrentUserName
            entity.ModifiedAt = DateTime.UtcNow
            Await _db.SaveChangesAsync()
            SetSuccessMessage(Strings.Common_Updated)
            Return RedirectToAction("Index")
        End Function

        ' ========== POST: /Accounts/ToggleActive ==============
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function ToggleActive(id As String) As Task(Of ActionResult)
            If String.IsNullOrWhiteSpace(id) Then Return HttpNotFound()
            Dim entity = Await _db.SystemAccounts.FindAsync(id)
            If entity Is Nothing Then Return HttpNotFound()
            entity.Active = Not entity.Active
            entity.ModifiedBy = CurrentUserName
            entity.ModifiedAt = DateTime.UtcNow
            Await _db.SaveChangesAsync()
            SetSuccessMessage(If(entity.Active, Strings.Common_Activated, Strings.Common_Deactivated))
            Return RedirectToAction("Index")
        End Function

        ' ========== GET: /Accounts/ExportExcel ================
        Public Async Function ExportExcel() As Task(Of ActionResult)
            Dim rows = Await _db.SystemAccounts _
                .OrderBy(Function(a) a.IdCuenta) _
                .Select(Function(a) New With {
                    a.IdCuenta,
                    a.Cuenta,
                    .Category = a.Category.Name,
                    .SubCategory = a.SubCategory.Name,
                    .Grouping = a.Grouping.Name,
                    a.Active
                }).ToListAsync()

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial
            Using pkg As New ExcelPackage()
                Dim ws = pkg.Workbook.Worksheets.Add(Strings.Accounts_Title)

                ' Header
                Dim headers = {
                    Strings.Accounts_Col_Id,
                    Strings.Accounts_Col_Name,
                    Strings.Accounts_Col_Category,
                    Strings.Accounts_Col_SubCategory,
                    Strings.Accounts_Col_Grouping,
                    Strings.Accounts_Col_Active
                }
                For i = 0 To headers.Length - 1
                    ws.Cells(1, i + 1).Value = headers(i)
                Next
                Using headerRange = ws.Cells(1, 1, 1, headers.Length)
                    headerRange.Style.Font.Bold = True
                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid
                    headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#008bcc"))
                    headerRange.Style.Font.Color.SetColor(System.Drawing.Color.White)
                End Using

                ' Rows
                Dim row = 2
                For Each r In rows
                    ws.Cells(row, 1).Value = r.IdCuenta
                    ws.Cells(row, 2).Value = r.Cuenta
                    ws.Cells(row, 3).Value = r.Category
                    ws.Cells(row, 4).Value = r.SubCategory
                    ws.Cells(row, 5).Value = r.Grouping
                    ws.Cells(row, 6).Value = If(r.Active, Strings.Common_Yes, Strings.Common_No)
                    row += 1
                Next

                ws.Cells.AutoFitColumns()

                Dim bytes = pkg.GetAsByteArray()
                Dim fileName = $"{Strings.Accounts_ExportFileName}_{DateTime.UtcNow:yyyyMMdd_HHmm}.xlsx"
                Return File(bytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileName)
            End Using
        End Function

        ' ========== Helpers ====================================
        Private Async Function PopulateLookups(categoryId As Integer?, groupingId As Integer?) As Task
            Dim cats = Await _db.AccountCategories _
                .Where(Function(c) c.Active) _
                .OrderBy(Function(c) c.Name).ToListAsync()
            Dim subs = Await _db.AccountSubCategories _
                .Where(Function(s) s.Active) _
                .OrderBy(Function(s) s.Category.Name).ThenBy(Function(s) s.Name) _
                .Select(Function(s) New With {s.SubCategoryId, s.Name, s.CategoryId}) _
                .ToListAsync()
            Dim groups = Await _db.AccountGroupings _
                .Where(Function(g) g.Active) _
                .OrderBy(Function(g) g.Name).ToListAsync()

            ViewData("Categories") = New SelectList(cats, "CategoryId", "Name", categoryId)
            ViewData("Groupings") = New SelectList(groups, "GroupingId", "Name", groupingId)
            ' Pass all sub-categories with their CategoryId so the view JS can filter.
            ViewData("AllSubCategories") = subs
        End Function

        Private Async Function ValidateSubCategoryBelongsToCategory(catId As Integer, subId As Integer) As Task(Of Boolean)
            Dim sc = Await _db.AccountSubCategories.FindAsync(subId)
            Return sc IsNot Nothing AndAlso sc.CategoryId = catId
        End Function

    End Class

End Namespace
