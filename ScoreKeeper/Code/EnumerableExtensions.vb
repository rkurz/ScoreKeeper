Imports System.Runtime.CompilerServices

Public Module EnumerableExtensions

    <Extension()>
    Public Sub ForEach(Of T)(ByVal collection As IEnumerable(Of T), ByVal action As Action(Of T))
        For Each item As T In collection
            action(item)
        Next
    End Sub

End Module
