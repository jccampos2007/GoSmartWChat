Public Class StartingMode
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MySettings.Default.StartingMode = 0
        MySettings.Default.Save()
        MainForm.Show()
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MySettings.Default.StartingMode = 1
        MySettings.Default.Save()
        MainForm.Show()
        Me.Close()
    End Sub
End Class