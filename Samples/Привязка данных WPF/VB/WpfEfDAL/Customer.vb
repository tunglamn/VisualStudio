﻿Imports System.ComponentModel

Partial Public Class Customer
    Implements IDataErrorInfo

    Public Sub New()
        'Задать значения по умолчанию
        Me.LastName = "[new]"

    End Sub

    ''' <summary>
    ''' Этот метод вызывается в методе задания свойства LastName клиента
    '''  разделяемый класс, сформированный конструктором EDM.
    ''' </summary>
    Private Sub OnLastNameChanged()
        'Выполнить проверку. 
        If _LastName Is Nothing OrElse _LastName.Trim() = "" OrElse _LastName.Trim() = "[new]" Then
            Me.AddError("LastName", "Please enter a last name.")
        Else
            Me.RemoveError("LastName")
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
