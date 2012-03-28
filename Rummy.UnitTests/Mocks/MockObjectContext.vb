Imports System.Data.Objects.DataClasses

''' <summary>
''' An IObjectContext that holds its objects in memory; simulating an 'ObjectContext' but
''' without offering any persistence.
''' </summary>
Public Class MockObjectContext
    Implements IObjectContext

    ''' <summary>
    ''' List of committed objects
    ''' </summary> 
    Private _objectLists As New Dictionary(Of Type, IList)

    ''' <summary>
    ''' Lists of uncmomitted objects to be added
    ''' </summary>   
    Private _addedLists As New Dictionary(Of Type, IList)

    ''' <summary>
    ''' Lists of uncommitted objects to be deleted
    ''' </summary>   
    Private _deletedLists As New Dictionary(Of Type, IList)

    ''' <summary>
    ''' Lists of uncommitted updates to existing objects.
    ''' </summary>
    ''' <remarks>Had to use a dictionary instead of a list so that the original object remains available (ie/ so we know which object to change).</remarks>
    Private _updatedLists As New Dictionary(Of Type, IDictionary)

    Public Sub AddObject(Of T As System.Data.Objects.DataClasses.EntityObject)(ByVal obj As T, ByVal entitySetName As String) Implements IObjectContext.AddObject
        GetAddedList(Of T)().Add(obj)

        '' Verify that the object lists have a list of type T, otherwise we run 
        '' into type difficulties later in 'SaveChanges'.
        GetObjectList(Of T)()
    End Sub

    Public Sub DeleteObject(Of T)(ByVal obj As T) Implements IObjectContext.DeleteObject
        GetDeletedList(Of T)().Add(obj)
    End Sub

    Public Sub UpdateObject(Of T As System.Data.Objects.DataClasses.EntityObject)(ByVal obj As T, ByVal entitySetName As String) Implements IObjectContext.UpdateObject
        Dim updatedList As Dictionary(Of EntityKey, T)

        updatedList = GetUpdatedList(Of T)()
        If updatedList.ContainsKey(obj.EntityKey) Then
            'This object has already been scheduled for an update.  We will overwrite the original update with the new one.
            updatedList(obj.EntityKey) = obj
        Else
            'Add the object to the list of objects waiting to be updated.
            updatedList.Add(obj.EntityKey, obj)
        End If
    End Sub

    Public Function GetObjects(Of T As System.Data.Objects.DataClasses.EntityObject)() As IQueryable(Of T) Implements IObjectContext.GetObjects
        Return GetObjectList(Of T)().AsQueryable()
    End Function

    Public Sub SaveChanges() Implements IObjectContext.SaveChanges
        Dim updatedList As Dictionary(Of EntityKey, EntityObject)
        Dim index As Integer

        For Each t As Type In _addedLists.Keys
            For Each o As Object In _addedLists(t)
                _objectLists(t).Add(o)
            Next

            _addedLists(t).Clear()
        Next

        'Perform updates.
        For Each t As Type In _updatedLists.Keys
            updatedList = _updatedLists(t)
            For Each key As EntityKey In updatedList.Keys
                'Find the index of this key in the object list for this type.
                index = FindIndexOfKey(GetObjectList(Of EntityObject), key)
                If index >= 0 Then
                    _objectLists(t)(index) = updatedList(key)
                End If
            Next

            _updatedLists(t).Clear()
        Next

        For Each t As Type In _deletedLists.Keys
            If _objectLists.ContainsKey(t) Then
                For Each o As Object In _deletedLists(t)
                    _objectLists(t).Remove(o)
                Next
            End If

            _deletedLists(t).Clear()
        Next
    End Sub

    ''' <summary>
    ''' Retreive a list of committed objects of the given type.
    ''' </summary>   
    Private Function GetObjectList(Of T)() As List(Of T)
        Return GetList(Of T)(_objectLists)
    End Function

    Private Function FindIndexOfKey(ByVal list As List(Of System.Data.Objects.DataClasses.EntityObject), ByVal key As EntityKey) As Integer
        Dim counter As Integer

        For counter = 0 To list.Count - 1
            If list(counter).EntityKey = key Then
                Return counter
            End If
        Next

        'Requested key was not found in the list.
        Return -1
    End Function

    ''' <summary>
    ''' Retreive a list of uncommitted added objects of the given type.
    ''' </summary>  
    Private Function GetAddedList(Of T)() As List(Of T)
        Return GetList(Of T)(_addedLists)
    End Function

    ''' <summary>
    ''' Retreive a list of uncommitted deleted objects of the given type.
    ''' </summary>
    Private Function GetDeletedList(Of T)() As List(Of T)
        Return GetList(Of T)(_deletedLists)
    End Function

    ''' <summary>
    ''' Retrieve a list of uncommitted updates to objects of the given type.
    ''' </summary>
    Private Function GetUpdatedList(Of t)() As Dictionary(Of EntityKey, t)
        Return GetDictionary(Of t)(_updatedLists)
    End Function

    ''' <summary>
    ''' Retreive a list from the given dictionary, of the given type.
    ''' </summary>  
    Private Function GetList(Of T)(ByVal dict As Dictionary(Of Type, IList)) As List(Of T)
        If dict.ContainsKey(GetType(T)) Then
            Return DirectCast(dict(GetType(T)), List(Of T))
        Else
            Dim list = New List(Of T)
            dict.Add(GetType(T), list)
            Return list
        End If
    End Function

    Private Function GetDictionary(Of T)(ByVal dict As Dictionary(Of Type, IDictionary)) As Dictionary(Of EntityKey, T)
        If dict.ContainsKey(GetType(T)) Then
            Return DirectCast(dict(GetType(T)), Dictionary(Of EntityKey, T))
        Else
            Dim objectDict As Dictionary(Of EntityKey, T)
            objectDict = New Dictionary(Of EntityKey, T)
            dict.Add(GetType(T), objectDict)
            Return objectDict
        End If
    End Function

End Class
