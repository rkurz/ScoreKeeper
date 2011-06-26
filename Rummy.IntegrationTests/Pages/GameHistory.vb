Public Class GameHistoryPage
    Inherits MobilePage

    Public Sub New()
        MyBase.New("Home/GameHistory")
    End Sub

    Public Function FindFirstGame() As WatiN.Core.List
        Return Me.ActiveContent.List(Find.First)
    End Function
End Class
