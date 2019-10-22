<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.btnSettings = New System.Windows.Forms.Button()
        Me.settingToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnChat = New System.Windows.Forms.Button()
        Me.btnLoadSimpleXS = New System.Windows.Forms.Button()
        Me.btnLoadImageXLS = New System.Windows.Forms.Button()
        Me.btnActivateCRM = New System.Windows.Forms.Button()
        Me.ChatPanel = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBoxOffline = New System.Windows.Forms.PictureBox()
        Me.PictureBoxOnline = New System.Windows.Forms.PictureBox()
        Me.Lbl_status = New System.Windows.Forms.Label()
        Me.PictureBoxCRMOff = New System.Windows.Forms.PictureBox()
        Me.PictureBoxCRMOn = New System.Windows.Forms.PictureBox()
        Me.lblCRM = New System.Windows.Forms.Label()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PictureBoxOffline, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxOnline, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxCRMOff, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxCRMOn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnSettings
        '
        Me.btnSettings.Image = CType(resources.GetObject("btnSettings.Image"), System.Drawing.Image)
        Me.btnSettings.Location = New System.Drawing.Point(0, 0)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(50, 50)
        Me.btnSettings.TabIndex = 2
        Me.settingToolTip.SetToolTip(Me.btnSettings, "My Settings")
        Me.btnSettings.UseVisualStyleBackColor = True
        '
        'settingToolTip
        '
        Me.settingToolTip.IsBalloon = True
        '
        'btnChat
        '
        Me.btnChat.Image = CType(resources.GetObject("btnChat.Image"), System.Drawing.Image)
        Me.btnChat.Location = New System.Drawing.Point(0, 49)
        Me.btnChat.Name = "btnChat"
        Me.btnChat.Size = New System.Drawing.Size(50, 50)
        Me.btnChat.TabIndex = 3
        Me.settingToolTip.SetToolTip(Me.btnChat, "WhatsApp")
        Me.btnChat.UseVisualStyleBackColor = True
        '
        'btnLoadSimpleXS
        '
        Me.btnLoadSimpleXS.Enabled = False
        Me.btnLoadSimpleXS.Image = CType(resources.GetObject("btnLoadSimpleXS.Image"), System.Drawing.Image)
        Me.btnLoadSimpleXS.Location = New System.Drawing.Point(0, 98)
        Me.btnLoadSimpleXS.Name = "btnLoadSimpleXS"
        Me.btnLoadSimpleXS.Size = New System.Drawing.Size(50, 50)
        Me.btnLoadSimpleXS.TabIndex = 4
        Me.settingToolTip.SetToolTip(Me.btnLoadSimpleXS, "Send Plain Messages")
        Me.btnLoadSimpleXS.UseVisualStyleBackColor = True
        Me.btnLoadSimpleXS.Visible = False
        '
        'btnLoadImageXLS
        '
        Me.btnLoadImageXLS.Enabled = False
        Me.btnLoadImageXLS.Image = CType(resources.GetObject("btnLoadImageXLS.Image"), System.Drawing.Image)
        Me.btnLoadImageXLS.Location = New System.Drawing.Point(0, 147)
        Me.btnLoadImageXLS.Name = "btnLoadImageXLS"
        Me.btnLoadImageXLS.Size = New System.Drawing.Size(50, 50)
        Me.btnLoadImageXLS.TabIndex = 5
        Me.settingToolTip.SetToolTip(Me.btnLoadImageXLS, "Send Image Messages")
        Me.btnLoadImageXLS.UseVisualStyleBackColor = True
        Me.btnLoadImageXLS.Visible = False
        '
        'btnActivateCRM
        '
        Me.btnActivateCRM.Enabled = False
        Me.btnActivateCRM.Image = CType(resources.GetObject("btnActivateCRM.Image"), System.Drawing.Image)
        Me.btnActivateCRM.Location = New System.Drawing.Point(0, 196)
        Me.btnActivateCRM.Name = "btnActivateCRM"
        Me.btnActivateCRM.Size = New System.Drawing.Size(50, 50)
        Me.btnActivateCRM.TabIndex = 13
        Me.settingToolTip.SetToolTip(Me.btnActivateCRM, "Start CRM WhatsApp")
        Me.btnActivateCRM.UseVisualStyleBackColor = True
        Me.btnActivateCRM.Visible = False
        '
        'ChatPanel
        '
        Me.ChatPanel.AutoSize = True
        Me.ChatPanel.Location = New System.Drawing.Point(50, 0)
        Me.ChatPanel.Name = "ChatPanel"
        Me.ChatPanel.Size = New System.Drawing.Size(667, 558)
        Me.ChatPanel.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1, 545)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "      "
        '
        'Timer1
        '
        Me.Timer1.Interval = 30000
        '
        'PictureBoxOffline
        '
        Me.PictureBoxOffline.Image = CType(resources.GetObject("PictureBoxOffline.Image"), System.Drawing.Image)
        Me.PictureBoxOffline.Location = New System.Drawing.Point(9, 510)
        Me.PictureBoxOffline.Name = "PictureBoxOffline"
        Me.PictureBoxOffline.Size = New System.Drawing.Size(28, 26)
        Me.PictureBoxOffline.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxOffline.TabIndex = 10
        Me.PictureBoxOffline.TabStop = False
        '
        'PictureBoxOnline
        '
        Me.PictureBoxOnline.Image = CType(resources.GetObject("PictureBoxOnline.Image"), System.Drawing.Image)
        Me.PictureBoxOnline.Location = New System.Drawing.Point(9, 510)
        Me.PictureBoxOnline.Name = "PictureBoxOnline"
        Me.PictureBoxOnline.Size = New System.Drawing.Size(28, 26)
        Me.PictureBoxOnline.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxOnline.TabIndex = 11
        Me.PictureBoxOnline.TabStop = False
        Me.PictureBoxOnline.Visible = False
        '
        'Lbl_status
        '
        Me.Lbl_status.AutoSize = True
        Me.Lbl_status.Location = New System.Drawing.Point(5, 541)
        Me.Lbl_status.Name = "Lbl_status"
        Me.Lbl_status.Size = New System.Drawing.Size(37, 13)
        Me.Lbl_status.TabIndex = 12
        Me.Lbl_status.Text = "Offline"
        '
        'PictureBoxCRMOff
        '
        Me.PictureBoxCRMOff.Image = CType(resources.GetObject("PictureBoxCRMOff.Image"), System.Drawing.Image)
        Me.PictureBoxCRMOff.Location = New System.Drawing.Point(9, 450)
        Me.PictureBoxCRMOff.Name = "PictureBoxCRMOff"
        Me.PictureBoxCRMOff.Size = New System.Drawing.Size(28, 26)
        Me.PictureBoxCRMOff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxCRMOff.TabIndex = 14
        Me.PictureBoxCRMOff.TabStop = False
        '
        'PictureBoxCRMOn
        '
        Me.PictureBoxCRMOn.Image = CType(resources.GetObject("PictureBoxCRMOn.Image"), System.Drawing.Image)
        Me.PictureBoxCRMOn.Location = New System.Drawing.Point(9, 450)
        Me.PictureBoxCRMOn.Name = "PictureBoxCRMOn"
        Me.PictureBoxCRMOn.Size = New System.Drawing.Size(28, 26)
        Me.PictureBoxCRMOn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxCRMOn.TabIndex = 15
        Me.PictureBoxCRMOn.TabStop = False
        Me.PictureBoxCRMOn.Visible = False
        '
        'lblCRM
        '
        Me.lblCRM.AutoSize = True
        Me.lblCRM.Location = New System.Drawing.Point(1, 480)
        Me.lblCRM.Name = "lblCRM"
        Me.lblCRM.Size = New System.Drawing.Size(48, 13)
        Me.lblCRM.TabIndex = 16
        Me.lblCRM.Text = "CRM Off"
        '
        'Timer2
        '
        Me.Timer2.Interval = 10000
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(719, 561)
        Me.Controls.Add(Me.lblCRM)
        Me.Controls.Add(Me.PictureBoxCRMOn)
        Me.Controls.Add(Me.PictureBoxCRMOff)
        Me.Controls.Add(Me.btnActivateCRM)
        Me.Controls.Add(Me.Lbl_status)
        Me.Controls.Add(Me.PictureBoxOnline)
        Me.Controls.Add(Me.PictureBoxOffline)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnLoadImageXLS)
        Me.Controls.Add(Me.btnLoadSimpleXS)
        Me.Controls.Add(Me.btnChat)
        Me.Controls.Add(Me.ChatPanel)
        Me.Controls.Add(Me.btnSettings)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.Text = "GoSmartchat                                                                      " &
    "                                       Ver. Oct 01 / 2019"
        CType(Me.PictureBoxOffline, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxOnline, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxCRMOff, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxCRMOn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSettings As Button
    Friend WithEvents settingToolTip As ToolTip
    Friend WithEvents ChatPanel As Panel
    Friend WithEvents btnChat As Button
    Friend WithEvents btnLoadSimpleXS As Button
    Friend WithEvents btnLoadImageXLS As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PictureBoxOffline As PictureBox
    Friend WithEvents PictureBoxOnline As PictureBox
    Friend WithEvents Lbl_status As Label
    Friend WithEvents btnActivateCRM As Button
    Friend WithEvents PictureBoxCRMOff As PictureBox
    Friend WithEvents PictureBoxCRMOn As PictureBox
    Friend WithEvents lblCRM As Label
    Friend WithEvents Timer2 As Timer
End Class
