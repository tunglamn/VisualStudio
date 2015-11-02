﻿Imports System.ComponentModel

Public Class Order
    Implements IDataErrorInfo

    Public Sub New()
        'Обработать это событие так, что ИП может быть оповещен, если происходит смена клиента
        AddHandler Me.CustomerReference.AssociationChanged, AddressOf Customer_AssociationChanged

        'Задать значения по умолчанию
        Me.OrderDate = Date.Today()
        Me.AddError("Customer", "Please select a customer.")
    End Sub

    Private Sub Customer_AssociationChanged(ByVal sender As Object, _
                                            ByVal e As CollectionChangeEventArgs)
        If e.Action = CollectionChangeAction.Remove Then
            OnPropertyChanging("Customer")
        Else
            If e.Action = CollectionChangeAction.Add Then
                Me.RemoveError("Customer")
            End If
            OnPropertyChanged("Customer")
        End If
    End Sub

    ''' <summary>
    ''' Этот метод вызывается в методе задания свойства OrderDate объекта Order
    '''  разделяемый класс, сформированный конструктором EDM.
    ''' </summary>
    Private Sub OnOrderDateChanged()
        'Выполнить проверку. 
        Me.RemoveError("OrderDate")

        If Not _OrderDate.HasValue Then
            Me.AddError("OrderDate", "Please enter an order date.")
        ElseIf _ShipDate.HasValue AndAlso _ShipDate < _OrderDate Then
            Me.AddError("OrderDate", "Order date cannot be after ship date.")
        End If
    End Sub


#Region "Члены IDataErrorInfo"
    Private m_validationErrors As New Dictionary(Of String, String)

    Private Sub AddError(ByVal columnName As String, ByVal msg As String)
        If Not m_validationErrors.ContainsKey(columnName) Then
            m_validationErrors.Add(columnName, msg)
        End If
    End Sub

    Private Sub RemoveError(ByVal columnName As String)
        If m_validationErrors.ContainsKey(columnName) Then
            m_validationErrors.Remove(columnName)
        End If
    End Sub

    Public ReadOnly Property HasErrors() As Boolean
        Get
            Return m_validationErrors.Count > 0
        End Get
    End Property

    Public ReadOnly Property [Error]() As String Implements System.ComponentModel.IDataErrorInfo.Error
        Get
            If m_validationErrors.Count > 0 Then
                Return "Order data is invalid"
            Else
                Return Nothing
            End If
        End Get
    End Property

    Default Public ReadOnly Property Item(ByVal columnName As String) As String Implements System.ComponentModel.IDataErrorInfo.Item
        Get
            If m_validationErrors.ContainsKey(columnName) Then
                Return m_validationErrors(columnName).ToString
            Else
                Return Nothing
            End If
        End Get
    End Property
#End Region

End Class
