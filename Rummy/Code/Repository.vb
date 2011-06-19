Imports System.Data.Objects.DataClasses

''' <summary>
''' A generic repository of 'EntityObject'
''' </summary>
Public Class Repository(Of T As EntityObject)

    ''' <summary>
    ''' The underlying object container
    ''' </summary>   
    Private _context As IObjectContext

    ''' <summary>
    ''' Identifies the type of object with the object container (requried by 'ObjectContext')
    ''' </summary>   
    Private _entitySetName As String

    Public Sub New(ByVal context As IObjectContext, ByVal entitySetName As String)
        _context = context
        _entitySetName = entitySetName
    End Sub

    ''' <summary>
    ''' Registers a new entity with the context.
    ''' 
    ''' Does not call 'SaveChanges' on the context (results are not persisted right away).
    ''' </summary> 
    Public Sub Create(ByVal entity As T)
        _context.AddObject(entity, _entitySetName)
    End Sub

    ''' <summary>
    ''' Registers zero or more entities with the context.
    ''' 
    ''' Does not call 'SaveChanges' on the context (results are not persisted right away).
    ''' </summary>   
    Public Sub Create(ByVal entities As IEnumerable(Of T))
        entities.ForEach(Sub(x) Create(x))
    End Sub

    ''' <summary>
    ''' Deletes an existing entity from the context.
    ''' 
    ''' Does not call 'SaveChanges' on the context (results are not persisted right away).
    ''' </summary>    
    Public Sub Delete(ByVal entity As T)
        _context.DeleteObject(entity)
    End Sub

    ''' <summary>
    ''' Delete a bunch of entities at once.
    ''' 
    ''' Does not call 'SaveChanges' on the context (results are not persisted right away).
    ''' </summary> 
    Public Sub Delete(ByVal entities As IEnumerable(Of T))
        entities.ForEach(Sub(x) Delete(x))
    End Sub

    ''' <summary>
    ''' Retrieve all objects from the context.
    ''' </summary>  
    Public Function FindAll() As IQueryable(Of T)
        Return _context.GetObjects(Of T)()
    End Function

    ''' <summary>
    ''' Saves Creates and Deletes with the context.
    ''' </summary>  
    Public Sub SaveChanges()
        _context.SaveChanges()
    End Sub

    ''' <summary>
    ''' Updates the object properties in the context to match that of the entity parameter.
    ''' </summary>
    Public Sub Update(ByVal entity As T)
        _context.UpdateObject(entity, _entitySetName)
    End Sub
End Class

