Imports System.Collections.ObjectModel
Imports WpfEfDAL

Public Class CustomerCollection
    Inherits MyBaseCollection(Of Customer)

    ''' <summary>
    ''' Вернуть true, если какой-либо клиент в этой коллекции содержит ошибку проверки
    ''' </summary>
    Public ReadOnly Property HasErrors() As Boolean
        Get
            Return (Aggregate cust In Me Where cust.HasErrors Into Count()) > 0
        End Get
    End Property

    Sub New(ByVal customers As IEnumerable(Of Customer), ByVal context As OMSEntities)
        MyBase.New(customers, context)
    End Sub

    Protected Overrides Sub InsertItem(ByVal index As Integer, ByVal item As Customer)
        If Not Me.IsLoading Then
            'Сообщает ObjectContext, что следует начать отслеживание данной сущности клиента
            Me.Context.AddToCustomers(item)
        End If
        MyBase.InsertItem(index, item)
    End Sub

    Protected Overrides Sub RemoveItem(ByVal index As Integer)
        'Сообщает ObjectContext, что следует удалить данную сущность клиента
        Me.Context.DeleteObject(Me(index))
        MyBase.RemoveItem(index)
    End Sub

End Class
