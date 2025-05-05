-Example:
     ```vb6
     ' VB6 Code
     On Error Resume Next
     Dim x As Variant
     x = 10 / 0
     ```
     ```vb.net


     ' VB.NET Code
     Try
         Dim x As Integer
         x = 10 / 0
     Catch ex As Exception
         ' Handle error
     End Try
     ```
