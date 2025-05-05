Imports Microsoft.VisualBasic

' Customer.cls
Public CustomerId As Integer
Public Name As String

' OrderDetails.cls
Public OrderId As Integer
Public ProductName As String

' Module1.bas
Function GetOrderDetails(ByVal cust As Customer) As OrderDetails
    Dim order As New OrderDetails
    order.OrderId = 101
    order.ProductName = "Laptop"
    Set GetOrderDetails = order
End Function


' Customer class
Public Class Customer
    Public Property CustomerId As Integer
    Public Property Name As String
End Class

' OrderDetails class
Public Class OrderDetails
    Public Property OrderId As Integer
    Public Property ProductName As String
End Class

' Function to get order details
Public Function GetOrderDetails(cust As Customer) As OrderDetails
    Return New OrderDetails With {
        .OrderId = 101,
        .ProductName = "Laptop"
    }
End Function
