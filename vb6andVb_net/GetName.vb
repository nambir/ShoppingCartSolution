' VB6 Example
Function GetFullName(ByVal salutation As String, ByVal firstName As String, ByVal lastName As String) As String
    GetFullName = salutation & " " & firstName & " " & lastName
End Function

' VB.NET Example
Function GetFullName(ByVal salutation As String, ByVal firstName As String, ByVal lastName As String) As String
    Return salutation & " " & firstName & " " & lastName
End Function


'// C# Example
String GetFullName(String salutation, String firstName, String lastName)
{
    Return salutation + " " + firstName + " " + lastName;
}
