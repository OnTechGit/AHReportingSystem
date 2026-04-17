Option Strict On
Option Explicit On

Imports System.Data.Entity
Imports Microsoft.AspNet.Identity.EntityFramework

Namespace Models

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
        ' DbSets — Phase 1: System Account Catalog
        ' =====================================================
        Public Property SystemAccounts As DbSet(Of SystemAccount)
        Public Property AccountCategories As DbSet(Of AccountCategory)
        Public Property AccountSubCategories As DbSet(Of AccountSubCategory)
        Public Property AccountGroupings As DbSet(Of AccountGrouping)

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
            ' Phase 1 — Chart of Accounts
            ' =====================================================

            ' Disable cascade delete from SystemAccount -> Category/SubCategory/Grouping
            ' so classification tables cannot be accidentally wiped when an
            ' account is removed. Soft-delete via Active flag is the preferred path.
            modelBuilder.Entity(Of SystemAccount)() _
                .HasRequired(Function(a) a.Category) _
                .WithMany(Function(c) c.Accounts) _
                .HasForeignKey(Function(a) a.CategoryId) _
                .WillCascadeOnDelete(False)

            modelBuilder.Entity(Of SystemAccount)() _
                .HasRequired(Function(a) a.SubCategory) _
                .WithMany(Function(s) s.Accounts) _
                .HasForeignKey(Function(a) a.SubCategoryId) _
                .WillCascadeOnDelete(False)

            modelBuilder.Entity(Of SystemAccount)() _
                .HasRequired(Function(a) a.Grouping) _
                .WithMany(Function(g) g.Accounts) _
                .HasForeignKey(Function(a) a.GroupingId) _
                .WillCascadeOnDelete(False)

            ' SubCategory belongs to a Category — prevent cascade so a category
            ' cannot delete all its children implicitly.
            modelBuilder.Entity(Of AccountSubCategory)() _
                .HasRequired(Function(s) s.Category) _
                .WithMany(Function(c) c.SubCategories) _
                .HasForeignKey(Function(s) s.CategoryId) _
                .WillCascadeOnDelete(False)

            ' Decimal precision for future financial columns — set here as the
            ' standard for this DbContext (18,2).
            ' modelBuilder.Properties(Of Decimal)().Configure(Sub(c) c.HasPrecision(18, 2))

        End Sub

    End Class

End Namespace
