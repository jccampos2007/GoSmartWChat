Imports System.Data.OleDb

Public Class ConfigurationForm
    Public Function ImportExcellToDataGridView(ByRef path As String, ByVal Datagrid As DataGridView) As Boolean
        Try
            'Dim stConexion As String = ("Provider=Microsoft.ACE.OLEDB.15.0;" & ("Data Source=" & (path & ";Extended Properties=""Excel 15.0;Xml;HDR=YES;IMEX=2"";")))
            'Dim stConexion As String = ("Provider=Microsoft.Jet.OLEDB.4.0;" & ("Data Source=" & (path & ";Extended Properties=""Excel 15.0;Xml;HDR=YES;IMEX=2"";")))
            Dim stConexion As String = ("Provider=Microsoft.Jet.OLEDB.4.0;" & ("Data Source=" & (path & ";Extended Properties=""Excel 15.0;Xml;HDR=NO;IMEX=1"";")))
            Dim cnConex As New OleDbConnection(stConexion)
            Dim Cmd As New OleDbCommand("Select * From [Hoja1$]")
            Dim Ds As New DataSet
            Dim Da As New OleDbDataAdapter
            Dim Dt As New DataTable
            cnConex.Open()
            Cmd.Connection = cnConex
            Da.SelectCommand = Cmd
            Da.Fill(Ds)
            Dt = Ds.Tables(0)
            Datagrid.Columns.Clear()
            Datagrid.DataSource = Dt
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        Return True
    End Function

    Private Sub btnLoadExcel_Click(sender As Object, e As EventArgs) Handles btnLoadExcel.Click
        Dim openFD As New OpenFileDialog()
        With openFD
            .Title = “Seleccionar archivos”
            .Filter = “Archivos Excel(*.xls;*.xlsx)|*.xls;*xlsx|Todos los archivos(*.*)|*.*”
            .Multiselect = False
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                ImportExcellToDataGridView(.FileName, DataGridViewXLS)
            End If
        End With
    End Sub

    Private Sub btnSaveXLS_Click(sender As Object, e As EventArgs) Handles btnSaveXLS.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.OpenFileDialog1.ShowDialog()
        Me.txtXLSShortMessages.Text = Me.OpenFileDialog1.FileName
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.OpenFileDialog1.ShowDialog()
        Me.txtXLSImageMessages.Text = Me.OpenFileDialog1.FileName
    End Sub

    Private Sub ConfigurationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtXLSShortMessages.Text = MySettings.Default.TextXLSFile
        txtXLSImageMessages.Text = MySettings.Default.ImageXLSFile
        NumIntervalTxtMessages.Value = MySettings.Default.DelayForTextMessage
        NumericIntervalImageMessage.Value = MySettings.Default.DelayForImageMessage

        If (MySettings.Default.CRMServer = "gosmart_v3") Then
            ComboBox_server.SelectedIndex = 0
        ElseIf (MySettings.Default.CRMServer = "gosmart_dev") Then
            ComboBox_server.SelectedIndex = 1
        ElseIf (MySettings.Default.CRMServer = "gosmart_v103") Then
            ComboBox_server.SelectedIndex = 2
        ElseIf (MySettings.Default.CRMServer = "gosmart_v201") Then
            ComboBox_server.SelectedIndex = 0
        End If

    End Sub

    Private Sub btnSaveGeneralSettings_Click(sender As Object, e As EventArgs) Handles btnSaveGeneralSettings.Click

        MySettings.Default.TextXLSFile = txtXLSShortMessages.Text
        MySettings.Default.ImageXLSFile = txtXLSImageMessages.Text
        MySettings.Default.DelayForTextMessage = CInt(NumIntervalTxtMessages.Value)
        MySettings.Default.DelayForImageMessage = CInt(NumericIntervalImageMessage.Value)

        If (ComboBox_server.Text = "gosmart_v3 - Pruebas") Then
            MySettings.Default.CRMServer = "gosmart_v3"
        ElseIf (ComboBox_server.Text = "gosmart_dev - Desarrollo") Then
            MySettings.Default.CRMServer = "gosmart_dev"
        ElseIf (ComboBox_server.Text = "gosmart_v103 - Producción") Then
            MySettings.Default.CRMServer = "gosmart_v103"
        ElseIf (ComboBox_server.Text = "gosmart_v201 - Kike") Then
            MySettings.Default.CRMServer = "gosmart_v201"
        End If

        MySettings.Default.Save()



    End Sub

    Private Sub btnSendTxt_Click(sender As Object, e As EventArgs) Handles btnSendTxt.Click
        'MainForm.SendTxtMessage(TxtBox_number.Text, TxtBox_Msg.Text)
        MainForm.SendTxtMessageToID(TxtBox_number.Text, TxtBox_Msg.Text)
    End Sub

    Private Sub btnSendImage_Click(sender As Object, e As EventArgs) Handles btnSendImage.Click
        'MainForm.SendImageMessage(TxtBox_number.Text, TxtBox_Msg.Text, TxtBox_image.Text)
        MainForm.SendImageMessageToID(TxtBox_number.Text, TxtBox_Msg.Text, TxtBox_image.Text)

    End Sub

    Private Sub btnLoadImagePath_Click(sender As Object, e As EventArgs) Handles btnLoadImagePath.Click
        Me.OpenFileDialog1.ShowDialog()
        Me.TxtBox_image.Text = Me.OpenFileDialog1.FileName
        Me.PictureBox1.ImageLocation = Me.TxtBox_image.Text
    End Sub



    Private Sub Label12_DoubleClick(sender As Object, e As EventArgs) Handles Label12.DoubleClick

        MySettings.Default.CRMServer = "gosmart_v201"
        MySettings.Default.Save()
        MsgBox("localhost Kike")

    End Sub

    Private Sub ComboBox_server_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox_server.SelectedIndexChanged

    End Sub
End Class