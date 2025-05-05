' Interface file: IAnimal.cls (Class Module)
' Set "Instancing" property to "PublicNotCreatable"

Public Sub Speak()
End Sub

' Class: Dog.cls
Implements IAnimal

Private Sub IAnimal_Speak()
    MsgBox "Woof!"
End Sub

' Class: Cat.cls
Implements IAnimal

Private Sub IAnimal_Speak()
    MsgBox "Meow!"
End Sub

'Polymorphism:
Dim a As IAnimal

Set a = New Dog
a.Speak   ' Output: Woof!

Set a = New Cat
a.Speak   ' Output: Meow!
