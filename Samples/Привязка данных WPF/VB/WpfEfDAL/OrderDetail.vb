Imports System.ComponentModel

Public Class OrderDetail
    Implements IDataErrorInfo

    Public Sub New()
        'Обработать это событие так, что ИП может быть оповещен, если происходит смена клиента
        AddHandler Me.ProductReference.AssociationChanged, AddressOf Product_AssociationChanged

        'задать значения по умолчанию
        Me.Quantity = 1
        Me.AddError("Product", "Please select a product.")
    End Sub


    Private Sub Product_AssociationChanged(ByVal sender As Object, _
                                          ByVal e As CollectionChangeEventArgs)
        If e.Action = CollectionChangeAction.Remove Then
            OnPropertyChanging("Product")
        Else
            If e.Action = CollectionChangeAction.Add Then
                Me.RemoveError("Product")
            End If
            OnPropertyChanged("Product")
        End If
    End Sub

    ''' <summary>
    ''' Этот метод вызывается в методе задания свойства Quantity объекта OrderDetail
    '''  разделяемый класс, сформированный конструктором EDM.
    ''' </summary>
    Private Sub OnQuantityChanged()
        'Выполнить проверку. 
        If _Quantity <= 0 Then
            Me.AddError("Quantity", "Please enter a quantity.")
        Else
            Me.RemoveError("Quantity")
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
                Return "Customer data is invalid"
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
