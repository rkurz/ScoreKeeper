Imports System.Data.Objects.DataClasses

''' <summary>
''' A generic container of 'EntityObject'.  It is intented to abstract the standard
''' ObjectContext class, by allowing it to be mock-able.
''' </summary>
Public Interface IObjectContext
    ''' <summary>
    ''' Add an object to the repository.  It is not expected that the object will persist
    ''' (if the repository supports persistence) until 'SaveChanges' is called.
    ''' 
    ''' As well, the given object will not appear in 'GetObjects' until 'SaveChanges' is called.
    ''' </summary>   
    Sub AddObject(Of T As EntityObject)(ByVal obj As T, ByVal entitySetName As String)

    ''' <summary>
    ''' Retrieves all objects in the context of a given type.
    ''' </summary>   
    Function GetObjects(Of T As EntityObject)() As IQueryable(Of T)

    ''' <summary>
    ''' Remove the object from the repository.  It is not expected that the object will
    ''' persist (if the repository supports persistence) until 'SaveChanges' is called.
    ''' 
    ''' As well, the given object will continue to appear in 'GetObjects' until 'SaveChanges'
    ''' is called.
    ''' </summary>  
    Sub DeleteObject(Of T)(ByVal obj As T)

    ''' <summary>
    ''' Update the object stored in the repository with the new values assigned to the obj parameter.
    ''' </summary>
    Sub UpdateObject(Of T As EntityObject)(ByVal obj As T, ByVal entitySetName As String)

    ''' <summary>
    ''' Persists changes made by calling 'AddObject' and 'DeleteObject'.
    ''' </summary>   
    Sub SaveChanges()

End Interface

