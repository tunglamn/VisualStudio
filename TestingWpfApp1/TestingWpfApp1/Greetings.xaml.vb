Class MainWindow 

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        If RadioButton1.IsChecked = True Then
            MessageBox.Show("Hello.")
        Else : RadioButton2.IsChecked = True
            MessageBox.Show("Goodbye.")
        End If

    End Sub
End Class
