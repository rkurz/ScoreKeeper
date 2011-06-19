''' <summary>
''' Represents an HTML 5 numeric textbox (watin can not find input elements with type=number).
''' </summary>
<ElementTag("input", Index:=0, InputType:="number")>
Public Class NumericTextField
    Inherits WatiN.Core.TextField

    Public Sub New(ByVal domContainer As DomContainer, ByVal finder As ElementFinder)
        MyBase.New(domContainer, finder)
    End Sub

    Public Sub New(ByVal domContainer As DomContainer, ByVal nativeElement As Native.INativeElement)
        MyBase.New(domContainer, nativeElement)
    End Sub
End Class
