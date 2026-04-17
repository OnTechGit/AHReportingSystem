Option Strict On
Option Explicit On

Imports System.Data.Entity
Imports Microsoft.AspNet.Identity.EntityFramework

Namespace AHReportingSystem.Models

    ' =========================================================
    ' ApplicationDbContext
    ' Main EF Code-First DbContext for the application.
    ' Inherits from IdentityDbContext to include Identity tables.
    ' =========================================================
    Public Class ApplicationDbContext
        Inherits IdentityDbContext(Of ApplicationUser)

        Public Sub New()
            MyBase.New("AHReportingContext", throwIfV1Schema:=False)
        End Sub

        ' =====================================================
        ' DbSets — add entity tables here as phases progress
        ' =====================================================

        ' Phase 1: System Account Catalog
        ' Public Property SystemAccounts As DbSet(Of SystemAccount)
        ' Public Property AccountCategories As DbSet(Of AccountCategory)
        ' Public Property AccountSubCategories As DbSet(Of AccountSubCategory)
        ' Public Property AccountGroupings As DbSet(Of AccountGrouping)

        ' Phase 2: Companies & Account Mapping
        ' Public Property Companies As DbSet(Of Company)
        ' Public Property CompanyAccounts As DbSet(Of CompanyAccount)

        ' Phase 3: GL Entries
        ' Public Property GLEntries As DbSet(Of GLEntry)

        ''' <summary>
        ''' Factory method used by OWIN middleware.
        ''' </summary>
        Public Shared Function Create() As ApplicationDbContext
            Return New ApplicationDbContext()
        End Function

        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            MyBase.OnModelCreating(modelBuilder)

            ' =====================================================
            ' Identity table name customization (optional)
            ' =====================================================
            modelBuilder.Entity(Of ApplicationUser)().ToTable("AspNetUsers")

            ' =====================================================
            ' Fluent API configurations go here as models are added
            ' =====================================================

        End Sub

    End Class

End Namespace
