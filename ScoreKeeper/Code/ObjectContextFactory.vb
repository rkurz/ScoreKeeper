Public Class ObjectContextFactory
    Public Shared Function GetContext() As IObjectContext
        Dim db As RummyEntities

        db = New RummyEntities

        Return New ObjectContextAdapter(db)
    End Function
End Class
