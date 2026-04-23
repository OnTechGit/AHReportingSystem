Option Strict On
Option Explicit On

Imports System.Data.Entity
Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports AHReportingSystem.Models
Imports AHReportingSystem.Models.ViewModels
Imports AHReportingSystem.Resources

Namespace Controllers

    ''' <summary>
    ''' CRUD for the AH consortium companies.
    ''' Code is case-insensitive unique; always stored upper-cased.
    ''' </summary>
    <Authorize(Roles:="Admin,Supervisor")>
    Public Class CompaniesController
        Inherits BaseController

        Private ReadOnly _db As New ApplicationDbContext()

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then _db.Dispose()
            MyBase.Dispose(disposing)
        End Sub

        Public Async Function Index() As Task(Of ActionResult)
            Dim allowed = GetAllowedCompanyIds()
            Dim query = _db.Companies.AsQueryable()
            If allowed IsNot Nothing Then
                query = query.Where(Function(c) allowed.Contains(c.CompanyId))
            End If

            Dim list = Await query _
                .OrderBy(Function(c) c.Name) _
                .Select(Function(c) New CompanyListItemViewModel With {
                    .CompanyId = c.CompanyId,
                    .Name = c.Name,
                    .Code = c.Code,
                    .Active = c.Active
                }).ToListAsync()
            Return View(list)
        End Function

        Public Function Create() As ActionResult
            Return View(New CompanyFormViewModel() With {.Active = True})
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Create(model As CompanyFormViewModel) As Task(Of ActionResult)
            model.Code = If(model.Code, "").Trim().ToUpperInvariant()
            If Not ModelState.IsValid Then Return View(model)

            If Await _db.Companies.AnyAsync(Function(c) c.Code = model.Code) Then
                ModelState.AddModelError("Code", Strings.Companies_CodeTaken)
                Return View(model)
            End If

            Dim entity As New Company With {
                .Name = model.Name.Trim(),
                .Code = model.Code,
                .Active = model.Active,
                .CreatedBy = CurrentUserName,
                .CreatedAt = DateTime.UtcNow
            }
            _db.Companies.Add(entity)
            Await _db.SaveChangesAsync()

            ' If the creator is a restricted user (has UserCompany rows),
            ' auto-grant access to the newly created company so they can work on it.
            Dim allowed = GetAllowedCompanyIds()
            If allowed IsNot Nothing Then
                _db.UserCompanies.Add(New UserCompany With {
                    .UserId = CurrentUserId,
                    .CompanyId = entity.CompanyId,
                    .GrantedBy = CurrentUserName,
                    .GrantedAt = DateTime.UtcNow
                })
                Await _db.SaveChangesAsync()
            End If

            SetSuccessMessage(Strings.Common_Created)
            Return RedirectToAction("Index")
        End Function

        Public Async Function Edit(id As Integer?) As Task(Of ActionResult)
            If Not id.HasValue Then Return HttpNotFound()
            If Not CanAccessCompany(id.Value) Then Return Forbidden()

            Dim entity = Await _db.Companies.FindAsync(id.Value)
            If entity Is Nothing Then Return HttpNotFound()
            Return View(New CompanyFormViewModel With {
                .CompanyId = entity.CompanyId,
                .Name = entity.Name,
                .Code = entity.Code,
                .Active = entity.Active
            })
        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Async Function Edit(model As CompanyFormViewModel) As Task(Of ActionResult)
            model.Code = If(model.Code, "").Trim().ToUpperInvariant()
            If Not CanAccessCompany(model.CompanyId) Then Return Forbidden()
            If Not ModelState.IsValid Then Return View(model)

            Dim entity = Await _db.Companies.FindAsync(model.CompanyId)
            If entity Is Nothing Then Return HttpNotFound()

            If Await _db.Companies.AnyAsync(Function(c) c.Code = model.Code AndAlso c.CompanyId <> model.CompanyId) Then
                ModelState.AddModelError("Code", Strings.Companies_CodeTaken)
                Return View(model)
            End If

            entity.Name = model.Name.Trim()
            entity.Code = model.Code
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
            If Not CanAccessCompany(id) Then Return Forbidden()

            Dim entity = Await _db.Companies.FindAsync(id)
            If entity Is Nothing Then Return HttpNotFound()
            entity.Active = Not entity.Active
            entity.ModifiedBy = CurrentUserName
            entity.ModifiedAt = DateTime.UtcNow
            Await _db.SaveChangesAsync()
            SetSuccessMessage(If(entity.Active, Strings.Common_Activated, Strings.Common_Deactivated))
            Return RedirectToAction("Index")
        End Function

        Private Function Forbidden() As ActionResult
            SetErrorMessage(Strings.Users_NoAccessToCompany)
            Return RedirectToAction("Index")
        End Function

    End Class

End Namespace
