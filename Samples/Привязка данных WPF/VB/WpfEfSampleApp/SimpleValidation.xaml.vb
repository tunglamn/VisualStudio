Imports WpfEfDAL

''' <summary>
''' В этой форме демонстрируется простое отображение ошибок проверки данных.
''' - 1. В привязках данных XAML укажите ValidatesOnDataErrors=True
''' - 2. WpfEfDAL.Customer реализует IDataErrorInfo и задает правило проверки для свойства LastName
''' - 3. Application.xaml определяет ErrorTemplate для отображения ошибок проверки
''' </summary>
''' <remarks></remarks>
Partial Public Class SimpleValidation

    Private db As New OMSEntities 'ObjectContext EF подключается к базе данных и отслеживает изменения
    Private CustomerData As CustomerCollection 'наследует от ObservableCollection
    Private View As ListCollectionView 'предоставляет данные об используемости для элементов управления (положение и перемещение в коллекции)

    Private Sub Validation_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

        Dim query = From c In db.Customers _
                    Where c.City = "Seattle" _
                    Order By c.LastName, c.FirstName _
                    Select c

        Me.CustomerData = New CustomerCollection(query, db)

        Dim customerSource = CType(Me.FindResource("CustomerSource"), CollectionViewSource)
        customerSource.Source = Me.CustomerData

        Me.View = CType(customerSource.View, ListCollectionView)
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAdd.Click
        Me.View.AddNew()
        Me.View.CommitNew()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnSave.Click
        Try
            If Not Me.CustomerData.HasErrors Then
                db.SaveChanges()
                MessageBox.Show("Customer data saved.", Me.Title, MessageBoxButton.OK, MessageBoxImage.Information)
            Else
                MessageBox.Show("Please correct the errors on this form before saving.", Me.Title, MessageBoxButton.OK, MessageBoxImage.Stop)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub


End Class
