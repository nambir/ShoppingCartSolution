' Class: Person.cls
Private mName As String

Public Property Get Name() As String
    Name = mName
End Property

Public Property Let Name(value As String)
    mName = value
End Property

'Usage
Dim p As New Person
p.Name = "John"
MsgBox p.Name
