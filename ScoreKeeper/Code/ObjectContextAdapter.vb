Imports System.Data.Objects
Imports System.Data.Objects.DataClasses

''' <summary>
''' Wraps the standard 'ObjectContext' to implement the abstracted object context.
''' </summary>
Public Class ObjectContextAdapter
    Implements IObjectContext

    ''' <summary>
    ''' The standard ObjectContext being wrapped.
    ''' </summary>    
    Private _context As ObjectContext

    Public Sub New(ByVal context As ObjectContext)
        _context = context
    End Sub

    Public Sub AddObject(Of T As System.Data.Objects.DataClasses.EntityObject)(ByVal obj As T, ByVal entitySetName As String) Implements IObjectContext.AddObject
        _context.AddObject(entitySetName, obj)
    End Sub

    Public Sub DeleteObject(Of T)(ByVal obj As T) Implements IObjectContext.DeleteObject
        _context.DeleteObject(obj)
    End Sub

    Public Function GetObjects(Of T As System.Data.Objects.DataClasses.EntityObject)() As IQueryable(Of T) Implements IObjectContext.GetObjects
        Return _context.CreateObjectSet(Of T)()
    End Function

    Public Sub SaveChanges() Implements IObjectContext.SaveChanges
        _context.SaveChanges()
    End Sub

    Public Sub UpdateObject(Of T As System.Data.Objects.DataClasses.EntityObject)(ByVal obj As T, ByVal entitySetName As String) Implements IObjectContext.UpdateObject
        _context.ApplyCurrentValues(entitySetName, obj)
    End Sub
End Class
