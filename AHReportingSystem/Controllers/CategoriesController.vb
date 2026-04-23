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
    Public Class CategoriesController
        Inherits BaseController

        Private ReadOnly _db As New ApplicationDbContext()

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then _db.Dispose()
            MyBase.Dispose(disposing)
        End Sub

        ' GET: /Categories
        Public Async Function Index() As Task(Of ActionResult)
            Dim list = Await _db.AccountCategories _
                .OrderBy(Function(c) c.Name) _
                .Select(Function(c) New CategoryListItemViewModel With {
                    .CategoryId = c.CategoryId,
                    .Name = c.Name,
                    .Active = c.Active,
                    .SubCategoryCount = c.SubCategories.Count,
                    .AccountCount = c.Accounts.Count
                }).ToListAsync()
            Return View(list)
        End Function

        ' GET: /Categories/Create
        Public Function Create() As ActionResult
            Return View(New CategoryFormViewModel() With {.Active = True})
        End Function

        ' POST: /Categories/Create
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Create(model As CategoryFormViewModel) As Task(Of ActionResult)
            If Not ModelState.IsValid Then Return View(model)

            If Await _db.AccountCategories.AnyAsync(Function(c) c.Name = model.Name) Then
                ModelState.AddModelError("Name", Strings.Categories_NameTaken)
                Return View(model)
            End If

            _db.AccountCategories.Add(New AccountCategory With {
                .Name = model.Name,
                .Active = model.Active,
                .CreatedBy = CurrentUserName,
                .CreatedAt = DateTime.UtcNow
            })
            Await _db.SaveChangesAsync()
            SetSuccessMessage(Strings.Common_Created)
            Return RedirectToAction("Index")
        End Function

        ' GET: /Categories/Edit/{id}
        Public Async Function Edit(id As Integer?) As Task(Of ActionResult)
            If Not id.HasValue Then Return HttpNotFound()
            Dim entity = Await _db.AccountCategories.FindAsync(id.Value)
            If entity Is Nothing Then Return HttpNotFound()
            Return View(New CategoryFormViewModel With {
                .CategoryId = entity.CategoryId,
                .Name = entity.Name,
                .Active = entity.Active
            })
        End Function

        ' POST: /Categories/Edit
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(model As CategoryFormViewModel) As Task(Of ActionResult)
            If Not ModelState.IsValid Then Return View(model)

            Dim entity = Await _db.AccountCategories.FindAsync(model.CategoryId)
            If entity Is Nothing Then Return HttpNotFound()

            If Await _db.AccountCategories.AnyAsync(Function(c) c.Name = model.Name AndAlso c.CategoryId <> model.CategoryId) Then
                ModelState.AddModelError("Name", Strings.Categories_NameTaken)
                Return View(model)
            End If

            ' Prevent deactivation while accounts still reference it
            If entity.Active AndAlso Not model.Active Then
                Dim inUse = Await _db.SystemAccounts.AnyAsync(Function(a) a.CategoryId = entity.CategoryId AndAlso a.Active)
                If inUse Then
                    ModelState.AddModelError("Active", Strings.Categories_InUse)
                    Return View(model)
                End If
            End If

            entity.Name = model.Name
            entity.Active = model.Active
            entity.ModifiedBy = CurrentUserName
            entity.ModifiedAt = DateTime.UtcNow
            Await _db.SaveChangesAsync()
            SetSuccessMessage(Strings.Common_Updated)
            Return RedirectToAction("Index")
        End Function

        ' POST: /Categories/ToggleActive/{id}
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function ToggleActive(id As Integer) As Task(Of ActionResult)
            Dim entity = Await _db.AccountCategories.FindAsync(id)
            If entity Is Nothing Then Return HttpNotFound()

            If entity.Active Then
                Dim inUse = Await _db.SystemAccounts.AnyAsync(Function(a) a.CategoryId = id AndAlso a.Active)
                If inUse Then
                    SetErrorMessage(Strings.Categories_InUse)
                    Return RedirectToAction("Index")
                End If
            End If

            entity.Active = Not entity.Active
            entity.ModifiedBy = CurrentUserName
            entity.ModifiedAt = DateTime.UtcNow
            Await _db.SaveChangesAsync()
            SetSuccessMessage(If(entity.Active, Strings.Common_Activated, Strings.Common_Deactivated))
            Return RedirectToAction("Index")
        End Function

    End Class

End Namespace
