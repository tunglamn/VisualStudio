Imports WpfEfDAL

''' <summary>
''' В этом примере показывается прием построения простой сетки данных с помощью ListView в WPF.
''' </summary>
''' <remarks></remarks>
Partial Public Class SimpleDataGrid

    Private db As New OMSEntities 'ObjectContext EF подключается к базе данных и отслеживает изменения
    Private CustomerData As CustomerCollection 'наследует от ObservableCollection
    Private View As ListCollectionView 'предоставляет данные об используемости для элементов управления (положение и перемещение в коллекции)

    Private Sub SimpleDataGrid_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Dim query = From c In db.Customers _
                   Where c.City = "Seattle" _
                   Order By c.LastName, c.FirstName _
                   Select c

        Me.CustomerData = New CustomerCollection(query, db)

        Dim customerSource = CType(Me.FindResource("CustomerSource"), CollectionViewSource)
        customerSource.Source = Me.CustomerData

        Me.View = CType(customerSource.View, ListCollectionView)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnDelete.Click
        If Me.View.CurrentPosition > -1 Then
            Me.View.RemoveAt(Me.View.CurrentPosition)
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAdd.Click
        Dim cust = CType(Me.View.AddNew, Customer)
        'Задать значения по умолчанию, относящиеся к форме, или другой логике ИП...
        'cust.LastName = "[new]" 'Значения свойств по умолчанию заданы в конструкторе разделяемого класса WpfEfDAL.Customer
        Me.View.CommitNew()
        Me.ListView1.ScrollIntoView(cust)
        Me.ListView1.Focus()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnSave.Click
        Try
            db.SaveChanges()
            MessageBox.Show("Customer data saved.", Me.Title, MessageBoxButton.OK, MessageBoxImage.Information)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub Item_GotFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        'Будет обеспечивать синхронизацию положения View с выбранной 
        ' строкой, даже когда элемент управления редактируется в ListViewItem
        Dim item = CType(sender, ListViewItem)
        Me.ListView1.SelectedItem = item.DataContext
    End Sub
End Class
