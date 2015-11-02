Imports WpfEfDAL

''' <summary>
''' В этом примере демонстрируется использование конвертеров для форматирования значений и
'''  ограничения ввода данных пользователя допустимыми типами, а также для их проверки. 
''' В этой форме не допускается размещение данных, не являющихся датами, в полях дат и данных, не являющихся числами, в числовых полях
'''  с помощью простого ValueConverter (см. класс SimpleConverter и привязки XAML).
''' ''' </summary>
''' <remarks></remarks>
Partial Public Class FormattingAndValidation

    Private db As New OMSEntities 'ObjectContext EF подключается к базе данных и отслеживает изменения
    Private OrderData As OrdersCollection 'наследует от ObservableCollection

    Private MasterViewSource As CollectionViewSource
    Private DetailViewSource As CollectionViewSource

    'предоставляет данные об используемости для элементов управления (положение и перемещение в коллекциях)
    Private WithEvents MasterView As ListCollectionView
    Private DetailsView As BindingListCollectionView

    Private Sub FormattingAndValidation_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

        Dim query = From o In db.Orders.Include("OrderDetails") _
                    Where o.OrderDate >= #1/1/2009# _
                    Order By o.OrderDate Descending, o.Customer.LastName _
                    Select o

        Me.OrderData = New OrdersCollection(query, db)

        Dim customerList = From c In db.Customers _
                           Where c.Orders.Count > 0 _
                           Order By c.LastName, c.FirstName _
                           Select c

        Dim productList = From p In db.Products _
                          Order By p.Name _
                          Select p

        Me.MasterViewSource = CType(Me.FindResource("MasterViewSource"), CollectionViewSource)
        Me.DetailViewSource = CType(Me.FindResource("DetailsViewSource"), CollectionViewSource)
        Me.MasterViewSource.Source = Me.OrderData

        Dim customerSource = CType(Me.FindResource("CustomerLookup"), CollectionViewSource)
        customerSource.Source = customerList.ToList() 'Простой список подходит, так как здесь не редактируется Customers
        Dim productSource = CType(Me.FindResource("ProductLookup"), CollectionViewSource)
        productSource.Source = productList.ToList() 'Простой список подходит, так как здесь не редактируется Products

        Me.MasterView = CType(Me.MasterViewSource.View, ListCollectionView)
        Me.DetailsView = CType(Me.DetailViewSource.View, BindingListCollectionView)
    End Sub

    Private Sub MasterView_CurrentChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MasterView.CurrentChanged
        'Необходимо получить новое дочернее представление при изменении положения основного представления
        Me.DetailsView = CType(Me.DetailViewSource.View, BindingListCollectionView)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnSave.Click
        Try
            db.SaveChanges()
            MessageBox.Show("Order data saved.", Me.Title, MessageBoxButton.OK, MessageBoxImage.Information)
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnDelete.Click
        If Me.MasterView.CurrentPosition > -1 Then
            Me.MasterView.RemoveAt(Me.MasterView.CurrentPosition)
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAdd.Click
        Me.MasterView.AddNew()
        Me.MasterView.CommitNew()
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnPrevious.Click
        If Me.MasterView.CurrentPosition > 0 Then
            Me.MasterView.MoveCurrentToPrevious()
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnNext.Click
        If Me.MasterView.CurrentPosition < Me.MasterView.Count - 1 Then
            Me.MasterView.MoveCurrentToNext()
        End If
    End Sub

    Private Sub btnAddDetail_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAddDetail.Click
        Me.DetailsView.AddNew()
        Me.DetailsView.CommitNew()
    End Sub

    Private Sub btnDeleteDetail_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnDeleteDetail.Click
        If Me.DetailsView.CurrentPosition > -1 Then
            Me.DetailsView.RemoveAt(Me.DetailsView.CurrentPosition)
        End If
    End Sub

    Private Sub Item_GotFocus(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        'Будет обеспечивать синхронизацию положения View с выбранной 
        ' строкой, даже когда элемент управления редактируется в ListViewItem
        Dim item = CType(sender, ListViewItem)
        Me.ListView1.SelectedItem = item.DataContext
    End Sub

End Class
