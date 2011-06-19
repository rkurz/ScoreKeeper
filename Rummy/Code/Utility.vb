Public NotInheritable Class Utility
    Public Shared Function AddScript(ByVal page As Web.UI.Page, ByVal relativePath As String) As String
        Return String.Format("<script src='{0:s}' type='text/javascript'></script>", page.ResolveClientUrl(relativePath))
    End Function

    Public Shared Function AddStylesheet(ByVal page As Web.UI.Page, ByVal relativePath As String) As String
        Return String.Format("<style type='text/css'>@import '{0:s}';</style>", page.ResolveClientUrl(relativePath))
    End Function
End Class
