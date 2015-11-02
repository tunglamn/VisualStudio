Imports WpfEfDAL
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Public Class OrdersCollection
    Inherits MyBaseCollection(Of Order)

    ''' <summary>
    ''' Вернуть true, если какой-либо заказ в этой коллекции содержит ошибку проверки
    ''' </summary>
    Public ReadOnly Property HasErrors() As Boolean
        Get
            Return (Aggregate order In Me Where order.HasErrors Into Count()) > 0
        End Get
    End Property

    Sub New(ByVal orders As IEnumerable(Of Order), ByVal context As OMSEntities)
        MyBase.New(orders, context)
    End Sub

    Protected Overrides Sub InsertItem(ByVal index As Integer, ByVal item As Order)
        AddHandler item.OrderDetails.AssociationChanged, AddressOf Details_CollectionChanged
        If Not Me.IsLoading Then
            Me.Context.AddToOrders(item)
        End If
        MyBase.InsertItem(index, item)
    End Sub

    Protected Overrides Sub RemoveItem(ByVal index As Integer)
        Dim order = Me(index)
        RemoveHandler order.OrderDetails.AssociationChanged, AddressOf Details_CollectionChanged
        For i = order.OrderDetails.Count - 1 To 0 Step -1
            'При удалении заказа, удалить все подробности, если они существуют
            Me.Context.DeleteObject(order.OrderDetails(i))
        Next
        Me.Context.DeleteObject(order)
        MyBase.RemoveItem(index)
    End Sub

    Private Sub Details_CollectionChanged(ByVal sender As Object, ByVal e As CollectionChangeEventArgs)
        If e.Action = CollectionChangeAction.Remove Then
            'Добавление подробности в заказ обрабатывается автоматически, но при этом необходимо сообщить ObjectContext, 
            ' что следует удалить подробность, если подробность удаляется из EntityCollection OrderDetails 
            Me.Context.DeleteObject(CType(e.Element, OrderDetail))
        End If
    End Sub

End Class
