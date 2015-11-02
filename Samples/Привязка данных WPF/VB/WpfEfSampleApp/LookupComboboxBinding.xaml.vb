Imports WpfEfDAL

''' <summary>
''' В этом примере показывается, как правильно привязывать ссылки на сущности к полям со списками, используемым при подстановке,
'''  а также как сообщить ИП об изменении ссылки на сущность. 
''' 1. - Обработайте событие CustomerReference.AssociationChanged для Order (см. разделяемый класс WpfEfDAL.Order)
''' 2. - XAML для поля со списком привязывает свойство SelectedItem к сущности Customer
''' 3. - Список подстановки клиентов заполняется из того же ObjectContext (OMSEntities), который используется в запросе заказов 
''' 4. - Список подстановки заказчиков является полной сущностью Customer, а не проекцией всего лишь нескольких полей
''' </summary>
''' <remarks></remarks>
Partial Public Class LookupComboboxBinding

    Private db As New OMSEntities 'ObjectContext EF подключается к базе данных и отслеживает изменения
    Private OrderData As OrdersCollection 'наследует от ObservableCollection
    Private View As ListCollectionView 'предоставляет данные об используемости для элементов управления (положение и перемещение в коллекции)

    Private Sub LookupComboboxBinding_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

        Dim query = From o In db.Orders _
                    Where o.OrderDate >= #1/1/2009# _
                    Order By o.OrderDate Descending, o.Customer.LastName _
                    Select o

        Me.OrderData = New OrdersCollection(query, db)

        'Убедитесь, что список подстановки заполняется из того же ObjectContext 
        ' (OMSEntities), используемый выше в запросе заказов.
        'Кроме того, убедитесь в том, что возвращается список сущности Customer, а не 
        ' проекцией всего лишь нескольких полей, в противном случае привязка не будет работать.
        Dim customerList = From c In db.Customers _
                           Where c.Orders.Count > 0 _
                           Order By c.LastName, c.FirstName _
                           Select c

        Dim orderSource = CType(Me.FindResource("OrdersSource"), CollectionViewSource)
        orderSource.Source = Me.OrderData

        Dim customerSource = CType(Me.FindResource("CustomerLookup"), CollectionViewSource)
        customerSource.Source = customerList.ToList() 'Простой список подходит, так как здесь не редактируется Customers

        Me.View = CType(orderSource.View, ListCollectionView)
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
            MessageBox.Show("Order data saved.", Me.Title, MessageBoxButton.OK, MessageBoxImage.Information)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

End Class
