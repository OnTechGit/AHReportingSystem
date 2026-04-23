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
    Public Class GroupingsController
        Inherits BaseController

        Private ReadOnly _db As New ApplicationDbContext()

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then _db.Dispose()
            MyBase.Dispose(disposing)
        End Sub

        Public Async Function Index() As Task(Of ActionResult)
            Dim list = Await _db.AccountGroupings _
                .OrderBy(Function(g) g.Name) _
                .Select(Function(g) New GroupingListItemViewModel With {
                    .GroupingId = g.GroupingId,
                    .Name = g.Name,
                    .Active = g.Active,
                    .AccountCount = g.Accounts.Count
                }).ToListAsync()
            Return View(list)
        End Function

        Public Function Create() As ActionResult
            Return View(New GroupingFormViewModel() With {.Active = True})
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Create(model As GroupingFormViewModel) As Task(Of ActionResult)
            If Not ModelState.IsValid Then Return View(model)

            If Await _db.AccountGroupings.AnyAsync(Function(g) g.Name = model.Name) Then
                ModelState.AddModelError("Name", Strings.Groupings_NameTaken)
                Return View(model)
            End If

            _db.AccountGroupings.Add(New AccountGrouping With {
                .Name = model.Name,
                .Active = model.Active,
                .CreatedBy = CurrentUserName,
                .CreatedAt = DateTime.UtcNow
            })
            Await _db.SaveChangesAsync()
            SetSuccessMessage(Strings.Common_Created)
            Return RedirectToAction("Index")
        End Function

        Public Async Function Edit(id As Integer?) As Task(Of ActionResult)
            If Not id.HasValue Then Return HttpNotFound()
            Dim entity = Await _db.AccountGroupings.FindAsync(id.Value)
            If entity Is Nothing Then Return HttpNotFound()
            Return View(New GroupingFormViewModel With {
                .GroupingId = entity.GroupingId,
                .Name = entity.Name,
                .Active = entity.Active
            })
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(model As GroupingFormViewModel) As Task(Of ActionResult)
            If Not ModelState.IsValid Then Return View(model)

            Dim entity = Await _db.AccountGroupings.FindAsync(model.GroupingId)
            If entity Is Nothing Then Return HttpNotFound()

            If Await _db.AccountGroupings.AnyAsync(Function(g) g.Name = model.Name AndAlso g.GroupingId <> model.GroupingId) Then
                ModelState.AddModelError("Name", Strings.Groupings_NameTaken)
                Return View(model)
            End If

            entity.Name = model.Name
            entity.Active = model.Active
            entity.ModifiedBy = CurrentUserName
            entity.ModifiedAt = DateTime.UtcNow
            Await _db.SaveChangesAsync()
            SetSuccessMessage(Strings.Common_Updated)
            Return RedirectToAction("Index")
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function ToggleActive(id As Integer) As Task(Of ActionResult)
            Dim entity = Await _db.AccountGroupings.FindAsync(id)
            If entity Is Nothing Then Return HttpNotFound()
            entity.Active = Not entity.Active
            entity.ModifiedBy = CurrentUserName
            entity.ModifiedAt = DateTime.UtcNow
            Await _db.SaveChangesAsync()
            SetSuccessMessage(If(entity.Active, Strings.Common_Activated, Strings.Common_Deactivated))
            Return RedirectToAction("Index")
        End Function

    End Class

End Namespace
