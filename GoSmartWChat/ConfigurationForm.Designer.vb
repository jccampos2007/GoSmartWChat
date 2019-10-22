<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ConfigurationForm
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConfigurationForm))
        Me.TabSettings = New System.Windows.Forms.TabControl()
        Me.GeneralSettings = New System.Windows.Forms.TabPage()
        Me.ComboBox_server = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.txtXLSImageMessages = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtXLSShortMessages = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnSaveGeneralSettings = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumericIntervalImageMessage = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.NumIntervalTxtMessages = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LoadXLS = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSaveXLS = New System.Windows.Forms.Button()
        Me.DataGridViewXLS = New System.Windows.Forms.DataGridView()
        Me.btnLoadExcel = New System.Windows.Forms.Button()
        Me.TabTestMsg = New System.Windows.Forms.TabPage()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnSendImage = New System.Windows.Forms.Button()
        Me.btnSendTxt = New System.Windows.Forms.Button()
        Me.btnLoadImagePath = New System.Windows.Forms.Button()
        Me.TxtBox_image = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtBox_Msg = New System.Windows.Forms.TextBox()
        Me.TxtBox_number = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabSettings.SuspendLayout()
        Me.GeneralSettings.SuspendLayout()
        CType(Me.NumericIntervalImageMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumIntervalTxtMessages, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LoadXLS.SuspendLayout()
        CType(Me.DataGridViewXLS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabTestMsg.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabSettings
        '
        Me.TabSettings.Controls.Add(Me.GeneralSettings)
        Me.TabSettings.Controls.Add(Me.LoadXLS)
        Me.TabSettings.Controls.Add(Me.TabTestMsg)
        Me.TabSettings.Location = New System.Drawing.Point(0, 5)
        Me.TabSettings.Name = "TabSettings"
        Me.TabSettings.SelectedIndex = 0
        Me.TabSettings.Size = New System.Drawing.Size(591, 238)
        Me.TabSettings.TabIndex = 0
        '
        'GeneralSettings
        '
        Me.GeneralSettings.Controls.Add(Me.ComboBox_server)
        Me.GeneralSettings.Controls.Add(Me.Label11)
        Me.GeneralSettings.Controls.Add(Me.Button2)
        Me.GeneralSettings.Controls.Add(Me.txtXLSImageMessages)
        Me.GeneralSettings.Controls.Add(Me.Button1)
        Me.GeneralSettings.Controls.Add(Me.txtXLSShortMessages)
        Me.GeneralSettings.Controls.Add(Me.Label6)
        Me.GeneralSettings.Controls.Add(Me.Label7)
        Me.GeneralSettings.Controls.Add(Me.btnSaveGeneralSettings)
        Me.GeneralSettings.Controls.Add(Me.Label5)
        Me.GeneralSettings.Controls.Add(Me.NumericIntervalImageMessage)
        Me.GeneralSettings.Controls.Add(Me.Label4)
        Me.GeneralSettings.Controls.Add(Me.NumIntervalTxtMessages)
        Me.GeneralSettings.Controls.Add(Me.Label3)
        Me.GeneralSettings.Controls.Add(Me.Label2)
        Me.GeneralSettings.Location = New System.Drawing.Point(4, 22)
        Me.GeneralSettings.Name = "GeneralSettings"
        Me.GeneralSettings.Padding = New System.Windows.Forms.Padding(3)
        Me.GeneralSettings.Size = New System.Drawing.Size(583, 212)
        Me.GeneralSettings.TabIndex = 0
        Me.GeneralSettings.Text = "General"
        Me.GeneralSettings.UseVisualStyleBackColor = True
        '
        'ComboBox_server
        '
        Me.ComboBox_server.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_server.FormattingEnabled = True
        Me.ComboBox_server.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ComboBox_server.Items.AddRange(New Object() {"gosmart_v3 - Pruebas", "gosmart_dev - Desarrollo", "gosmart_v103 - Producción"})
        Me.ComboBox_server.Location = New System.Drawing.Point(108, 189)
        Me.ComboBox_server.Name = "ComboBox_server"
        Me.ComboBox_server.Size = New System.Drawing.Size(191, 21)
        Me.ComboBox_server.TabIndex = 14
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 192)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(96, 16)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "CRM Server:"
        '
        'Button2
        '
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(445, 103)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(22, 22)
        Me.Button2.TabIndex = 12
        Me.Button2.UseVisualStyleBackColor = True
        '
        'txtXLSImageMessages
        '
        Me.txtXLSImageMessages.Location = New System.Drawing.Point(278, 103)
        Me.txtXLSImageMessages.Name = "txtXLSImageMessages"
        Me.txtXLSImageMessages.Size = New System.Drawing.Size(160, 20)
        Me.txtXLSImageMessages.TabIndex = 11
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(445, 21)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 22)
        Me.Button1.TabIndex = 10
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtXLSShortMessages
        '
        Me.txtXLSShortMessages.Location = New System.Drawing.Point(278, 21)
        Me.txtXLSShortMessages.Name = "txtXLSShortMessages"
        Me.txtXLSShortMessages.Size = New System.Drawing.Size(161, 20)
        Me.txtXLSShortMessages.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 137)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(264, 16)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Interval between media messages:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 105)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(248, 16)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Excel File for Media Messages:"
        '
        'btnSaveGeneralSettings
        '
        Me.btnSaveGeneralSettings.Image = CType(resources.GetObject("btnSaveGeneralSettings.Image"), System.Drawing.Image)
        Me.btnSaveGeneralSettings.Location = New System.Drawing.Point(516, 22)
        Me.btnSaveGeneralSettings.Name = "btnSaveGeneralSettings"
        Me.btnSaveGeneralSettings.Size = New System.Drawing.Size(50, 50)
        Me.btnSaveGeneralSettings.TabIndex = 6
        Me.btnSaveGeneralSettings.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(334, 53)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 16)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "secs."
        '
        'NumericIntervalImageMessage
        '
        Me.NumericIntervalImageMessage.Location = New System.Drawing.Point(278, 137)
        Me.NumericIntervalImageMessage.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.NumericIntervalImageMessage.Name = "NumericIntervalImageMessage"
        Me.NumericIntervalImageMessage.Size = New System.Drawing.Size(50, 20)
        Me.NumericIntervalImageMessage.TabIndex = 4
        Me.NumericIntervalImageMessage.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(334, 137)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 16)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "secs."
        '
        'NumIntervalTxtMessages
        '
        Me.NumIntervalTxtMessages.Location = New System.Drawing.Point(278, 52)
        Me.NumIntervalTxtMessages.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NumIntervalTxtMessages.Name = "NumIntervalTxtMessages"
        Me.NumIntervalTxtMessages.Size = New System.Drawing.Size(50, 20)
        Me.NumIntervalTxtMessages.TabIndex = 2
        Me.NumIntervalTxtMessages.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(264, 16)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Interval between short messages:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(248, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Excel File for short Messages:"
        '
        'LoadXLS
        '
        Me.LoadXLS.Controls.Add(Me.Label1)
        Me.LoadXLS.Controls.Add(Me.btnSaveXLS)
        Me.LoadXLS.Controls.Add(Me.DataGridViewXLS)
        Me.LoadXLS.Controls.Add(Me.btnLoadExcel)
        Me.LoadXLS.Location = New System.Drawing.Point(4, 22)
        Me.LoadXLS.Name = "LoadXLS"
        Me.LoadXLS.Padding = New System.Windows.Forms.Padding(3)
        Me.LoadXLS.Size = New System.Drawing.Size(583, 212)
        Me.LoadXLS.TabIndex = 1
        Me.LoadXLS.Text = "Load XLS"
        Me.LoadXLS.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(213, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(329, 55)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Coming Soon!"
        '
        'btnSaveXLS
        '
        Me.btnSaveXLS.Location = New System.Drawing.Point(108, 7)
        Me.btnSaveXLS.Name = "btnSaveXLS"
        Me.btnSaveXLS.Size = New System.Drawing.Size(71, 40)
        Me.btnSaveXLS.TabIndex = 2
        Me.btnSaveXLS.Text = "Save XLS"
        Me.btnSaveXLS.UseVisualStyleBackColor = True
        '
        'DataGridViewXLS
        '
        Me.DataGridViewXLS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewXLS.Location = New System.Drawing.Point(8, 53)
        Me.DataGridViewXLS.Name = "DataGridViewXLS"
        Me.DataGridViewXLS.Size = New System.Drawing.Size(171, 149)
        Me.DataGridViewXLS.TabIndex = 1
        '
        'btnLoadExcel
        '
        Me.btnLoadExcel.Location = New System.Drawing.Point(8, 6)
        Me.btnLoadExcel.Name = "btnLoadExcel"
        Me.btnLoadExcel.Size = New System.Drawing.Size(72, 41)
        Me.btnLoadExcel.TabIndex = 0
        Me.btnLoadExcel.Text = "Load XLS"
        Me.btnLoadExcel.UseVisualStyleBackColor = True
        '
        'TabTestMsg
        '
        Me.TabTestMsg.Controls.Add(Me.Label12)
        Me.TabTestMsg.Controls.Add(Me.PictureBox1)
        Me.TabTestMsg.Controls.Add(Me.btnSendImage)
        Me.TabTestMsg.Controls.Add(Me.btnSendTxt)
        Me.TabTestMsg.Controls.Add(Me.btnLoadImagePath)
        Me.TabTestMsg.Controls.Add(Me.TxtBox_image)
        Me.TabTestMsg.Controls.Add(Me.Label10)
        Me.TabTestMsg.Controls.Add(Me.Label9)
        Me.TabTestMsg.Controls.Add(Me.Label8)
        Me.TabTestMsg.Controls.Add(Me.TxtBox_Msg)
        Me.TabTestMsg.Controls.Add(Me.TxtBox_number)
        Me.TabTestMsg.Location = New System.Drawing.Point(4, 22)
        Me.TabTestMsg.Name = "TabTestMsg"
        Me.TabTestMsg.Size = New System.Drawing.Size(583, 212)
        Me.TabTestMsg.TabIndex = 2
        Me.TabTestMsg.Text = "Test Msg"
        Me.TabTestMsg.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(512, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 19)
        Me.Label12.TabIndex = 10
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(35, 153)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(60, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'btnSendImage
        '
        Me.btnSendImage.Image = CType(resources.GetObject("btnSendImage.Image"), System.Drawing.Image)
        Me.btnSendImage.Location = New System.Drawing.Point(482, 153)
        Me.btnSendImage.Name = "btnSendImage"
        Me.btnSendImage.Size = New System.Drawing.Size(50, 50)
        Me.btnSendImage.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.btnSendImage, "Send Image + Text")
        Me.btnSendImage.UseVisualStyleBackColor = True
        '
        'btnSendTxt
        '
        Me.btnSendTxt.Image = CType(resources.GetObject("btnSendTxt.Image"), System.Drawing.Image)
        Me.btnSendTxt.Location = New System.Drawing.Point(398, 153)
        Me.btnSendTxt.Name = "btnSendTxt"
        Me.btnSendTxt.Size = New System.Drawing.Size(50, 50)
        Me.btnSendTxt.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.btnSendTxt, "Send Text Message")
        Me.btnSendTxt.UseVisualStyleBackColor = True
        '
        'btnLoadImagePath
        '
        Me.btnLoadImagePath.Image = CType(resources.GetObject("btnLoadImagePath.Image"), System.Drawing.Image)
        Me.btnLoadImagePath.Location = New System.Drawing.Point(512, 112)
        Me.btnLoadImagePath.Name = "btnLoadImagePath"
        Me.btnLoadImagePath.Size = New System.Drawing.Size(20, 20)
        Me.btnLoadImagePath.TabIndex = 6
        Me.btnLoadImagePath.UseVisualStyleBackColor = True
        '
        'TxtBox_image
        '
        Me.TxtBox_image.Location = New System.Drawing.Point(139, 112)
        Me.TxtBox_image.Name = "TxtBox_image"
        Me.TxtBox_image.Size = New System.Drawing.Size(367, 20)
        Me.TxtBox_image.TabIndex = 5
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(11, 112)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(120, 16)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Image to Send:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(12, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(112, 16)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Text Message:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(11, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(128, 16)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Contact Number:"
        '
        'TxtBox_Msg
        '
        Me.TxtBox_Msg.Location = New System.Drawing.Point(139, 70)
        Me.TxtBox_Msg.Name = "TxtBox_Msg"
        Me.TxtBox_Msg.Size = New System.Drawing.Size(367, 20)
        Me.TxtBox_Msg.TabIndex = 1
        '
        'TxtBox_number
        '
        Me.TxtBox_number.Location = New System.Drawing.Point(139, 30)
        Me.TxtBox_number.Name = "TxtBox_number"
        Me.TxtBox_number.Size = New System.Drawing.Size(150, 20)
        Me.TxtBox_number.TabIndex = 0
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'ConfigurationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(594, 247)
        Me.Controls.Add(Me.TabSettings)
        Me.Name = "ConfigurationForm"
        Me.Text = "User Settings"
        Me.TabSettings.ResumeLayout(False)
        Me.GeneralSettings.ResumeLayout(False)
        Me.GeneralSettings.PerformLayout()
        CType(Me.NumericIntervalImageMessage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumIntervalTxtMessages, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LoadXLS.ResumeLayout(False)
        Me.LoadXLS.PerformLayout()
        CType(Me.DataGridViewXLS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabTestMsg.ResumeLayout(False)
        Me.TabTestMsg.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabSettings As TabControl
    Friend WithEvents GeneralSettings As TabPage
    Friend WithEvents LoadXLS As TabPage
    Friend WithEvents btnLoadExcel As Button
    Friend WithEvents DataGridViewXLS As DataGridView
    Friend WithEvents btnSaveXLS As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents NumericIntervalImageMessage As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents NumIntervalTxtMessages As NumericUpDown
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnSaveGeneralSettings As Button
    Friend WithEvents txtXLSShortMessages As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button1 As Button
    Friend WithEvents txtXLSImageMessages As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents TabTestMsg As TabPage
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtBox_Msg As TextBox
    Friend WithEvents TxtBox_number As TextBox
    Friend WithEvents btnLoadImagePath As Button
    Friend WithEvents TxtBox_image As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents btnSendImage As Button
    Friend WithEvents btnSendTxt As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ComboBox_server As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
End Class
