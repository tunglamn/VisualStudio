Imports WpfEfDAL

''' <summary>
''' В этом примере демонстрируется, как настроить привязку данных в XAML к коллекции 
'''  клиентов, запрашиваемой их модели EDM (Entity Data Model).
''' </summary>
''' <remarks></remarks>
Partial Public Class SimpleDataEntry

    Private db As New OMSEntities 'ObjectContext EF подключается к базе данных и отслеживает изменения
    Private CustomerData As CustomerCollection 'наследует от ObservableCollection
    Private View As ListCollectionView 'предоставляет данные об используемости для элементов управления (положение и перемещение в коллекции)

    Private Sub SimpleDataEntry_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

        Dim query = From c In db.Customers _
                    Where c.City = "Seattle" _
                    Order By c.LastName, c.FirstName _
                    Select c

        Me.CustomerData = New CustomerCollection(query, db)

        Dim customerSource = CType(Me.FindResource("CustomerSource"), CollectionViewSource)
        customerSource.Source = Me.CustomerData

        Me.View = CType(customerSource.View, ListCollectionView)
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnPrevious.Click
        If Me.View.CurrentPosition > 0 Then
            Me.View.MoveCurrentToPrevious()
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnNext.Click
        If Me.View.CurrentPosition < Me.View.Count - 1 Then
            Me.View.MoveCurrentToNext()
        End If
    End Sub

    Private Sub btnFirst_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnFirst.Click
        Me.View.MoveCurrentToFirst()
    End Sub

    Private Sub btnLast_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnLast.Click
        Me.View.MoveCurrentToLast()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnDelete.Click
        If Me.View.CurrentPosition > -1 Then
            Me.View.RemoveAt(Me.View.CurrentPosition)
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAdd.Click
        Me.View.AddNew()
        Me.View.CommitNew()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnSave.Click
        Try
            db.SaveChanges()
            MessageBox.Show("Customer data saved.", Me.Title, MessageBoxButton.OK, MessageBoxImage.Information)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

End Class
