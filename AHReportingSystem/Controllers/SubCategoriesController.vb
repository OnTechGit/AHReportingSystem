Option Strict On
Option Explicit On

Imports System.Data.Entity
Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports AHReportingSystem.Models
Imports AHReportingSystem.Models.ViewModels
Imports AHReportingSystem.Resources

Namespace Controllers

    <Authorize(Roles:="Admin,Supervisor")>
    Public Class SubCategoriesController
        Inherits BaseController

        Private ReadOnly _db As New ApplicationDbContext()

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then _db.Dispose()
            MyBase.Dispose(disposing)
        End Sub

        ' GET: /SubCategories
        Public Async Function Index() As Task(Of ActionResult)
            Dim list = Await _db.AccountSubCategories _
                .OrderBy(Function(s) s.Category.Name).ThenBy(Function(s) s.Name) _
                .Select(Function(s) New SubCategoryListItemViewModel With {
                    .SubCategoryId = s.SubCategoryId,
                    .Name = s.Name,
                    .CategoryId = s.CategoryId,
                    .CategoryName = s.Category.Name,
                    .Active = s.Active,
                    .AccountCount = s.Accounts.Count
                }).ToListAsync()
            Return View(list)
        End Function

        ' GET: /SubCategories/Create
        Public Async Function Create() As Task(Of ActionResult)
            Await PopulateCategories(Nothing)
            Return View(New SubCategoryFormViewModel() With {.Active = True})
        End Function

        ' POST: /SubCategories/Create
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Create(model As SubCategoryFormViewModel) As Task(Of ActionResult)
            If Not ModelState.IsValid Then
                Await PopulateCategories(model.CategoryId)
                Return View(model)
            End If

            If Await _db.AccountSubCategories.AnyAsync(Function(s) s.Name = model.Name AndAlso s.CategoryId = model.CategoryId) Then
                ModelState.AddModelError("Name", Strings.SubCategories_NameTaken)
                Await PopulateCategories(model.CategoryId)
                Return View(model)
            End If

            _db.AccountSubCategories.Add(New AccountSubCategory With {
                .Name = model.Name,
                .CategoryId = model.CategoryId,
                .Active = model.Active,
                .CreatedBy = CurrentUserName,
                .CreatedAt = DateTime.UtcNow
            })
            Await _db.SaveChangesAsync()
            SetSuccessMessage(Strings.Common_Created)
            Return RedirectToAction("Index")
        End Function

        ' GET: /SubCategories/Edit/{id}
        Public Async Function Edit(id As Integer?) As Task(Of ActionResult)
            If Not id.HasValue Then Return HttpNotFound()
            Dim entity = Await _db.AccountSubCategories.FindAsync(id.Value)
            If entity Is Nothing Then Return HttpNotFound()
            Await PopulateCategories(entity.CategoryId)
            Return View(New SubCategoryFormViewModel With {
                .SubCategoryId = entity.SubCategoryId,
                .Name = entity.Name,
                .CategoryId = entity.CategoryId,
                .Active = entity.Active
            })
        End Function

        ' POST: /SubCategories/Edit
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(model As SubCategoryFormViewModel) As Task(Of ActionResult)
            If Not ModelState.IsValid Then
                Await PopulateCategories(model.CategoryId)
                Return View(model)
            End If

            Dim entity = Await _db.AccountSubCategories.FindAsync(model.SubCategoryId)
            If entity Is Nothing Then Return HttpNotFound()

            If Await _db.AccountSubCategories.AnyAsync(Function(s) s.Name = model.Name AndAlso s.CategoryId = model.CategoryId AndAlso s.SubCategoryId <> model.SubCategoryId) Then
                ModelState.AddModelError("Name", Strings.SubCategories_NameTaken)
                Await PopulateCategories(model.CategoryId)
                Return View(model)
            End If

            entity.Name = model.Name
            entity.CategoryId = model.CategoryId
            entity.Active = model.Active
            entity.ModifiedBy = CurrentUserName
            entity.ModifiedAt = DateTime.UtcNow
            Await _db.SaveChangesAsync()
            SetSuccessMessage(Strings.Common_Updated)
            Return RedirectToAction("Index")
        End Function

        ' POST: /SubCategories/ToggleActive/{id}
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function ToggleActive(id As Integer) As Task(Of ActionResult)
            Dim entity = Await _db.AccountSubCategories.FindAsync(id)
            If entity Is Nothing Then Return HttpNotFound()
            entity.Active = Not entity.Active
            entity.ModifiedBy = CurrentUserName
            entity.ModifiedAt = DateTime.UtcNow
            Await _db.SaveChangesAsync()
            SetSuccessMessage(If(entity.Active, Strings.Common_Activated, Strings.Common_Deactivated))
            Return RedirectToAction("Index")
        End Function

        ' GET: /SubCategories/ForCategory/{id} — JSON endpoint used by Accounts form to filter sub-categories
        <HttpGet>
        Public Async Function ForCategory(id As Integer) As Task(Of ActionResult)
            Dim items = Await _db.AccountSubCategories _
                .Where(Function(s) s.CategoryId = id AndAlso s.Active) _
                .OrderBy(Function(s) s.Name) _
                .Select(Function(s) New With {.id = s.SubCategoryId, .name = s.Name}) _
                .ToListAsync()
            Return Json(items, JsonRequestBehavior.AllowGet)
        End Function

        ' ---------- Helpers ----------
        Private Async Function PopulateCategories(selected As Integer?) As Task
            Dim cats = Await _db.AccountCategories _
                .Where(Function(c) c.Active) _
                .OrderBy(Function(c) c.Name) _
                .ToListAsync()
            ViewData("Categories") = New SelectList(cats, "CategoryId", "Name", selected)
        End Function

    End Class

End Namespace
