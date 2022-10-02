<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class testmaker
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
        Me.btn_register = New System.Windows.Forms.Button()
        Me.btn_borrar = New System.Windows.Forms.Button()
        Me.btn_salir = New System.Windows.Forms.Button()
        Me.txt_identificador = New System.Windows.Forms.TextBox()
        Me.lbl_identify = New System.Windows.Forms.Label()
        Me.btn_process = New System.Windows.Forms.Button()
        Me.lbl_result = New System.Windows.Forms.Label()
        Me.lbl_status = New System.Windows.Forms.Label()
        Me.cmb_registertype = New System.Windows.Forms.ComboBox()
        Me.Trialmaker1 = New TrialMaker.trialmaker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_daylefts = New System.Windows.Forms.Label()
        Me.lbl_regfilepath = New System.Windows.Forms.Label()
        Me.lbl_hidfilepath = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_register
        '
        Me.btn_register.Location = New System.Drawing.Point(15, 12)
        Me.btn_register.Name = "btn_register"
        Me.btn_register.Size = New System.Drawing.Size(143, 23)
        Me.btn_register.TabIndex = 1
        Me.btn_register.Text = "Formulario de Registro"
        Me.btn_register.UseVisualStyleBackColor = True
        '
        'btn_borrar
        '
        Me.btn_borrar.Location = New System.Drawing.Point(172, 12)
        Me.btn_borrar.Name = "btn_borrar"
        Me.btn_borrar.Size = New System.Drawing.Size(143, 23)
        Me.btn_borrar.TabIndex = 3
        Me.btn_borrar.Text = "Borrar Registros"
        Me.btn_borrar.UseVisualStyleBackColor = True
        '
        'btn_salir
        '
        Me.btn_salir.Location = New System.Drawing.Point(329, 12)
        Me.btn_salir.Name = "btn_salir"
        Me.btn_salir.Size = New System.Drawing.Size(143, 23)
        Me.btn_salir.TabIndex = 4
        Me.btn_salir.Text = "Salir"
        Me.btn_salir.UseVisualStyleBackColor = True
        '
        'txt_identificador
        '
        Me.txt_identificador.Location = New System.Drawing.Point(349, 49)
        Me.txt_identificador.Name = "txt_identificador"
        Me.txt_identificador.Size = New System.Drawing.Size(40, 20)
        Me.txt_identificador.TabIndex = 5
        Me.txt_identificador.Text = "121"
        '
        'lbl_identify
        '
        Me.lbl_identify.AutoSize = True
        Me.lbl_identify.Location = New System.Drawing.Point(12, 50)
        Me.lbl_identify.Name = "lbl_identify"
        Me.lbl_identify.Size = New System.Drawing.Size(139, 13)
        Me.lbl_identify.TabIndex = 6
        Me.lbl_identify.Text = "Identificador Clave 3 dígitos"
        '
        'btn_process
        '
        Me.btn_process.Location = New System.Drawing.Point(395, 47)
        Me.btn_process.Name = "btn_process"
        Me.btn_process.Size = New System.Drawing.Size(77, 23)
        Me.btn_process.TabIndex = 7
        Me.btn_process.Text = "Procesar"
        Me.btn_process.UseVisualStyleBackColor = True
        '
        'lbl_result
        '
        Me.lbl_result.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbl_result.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_result.ForeColor = System.Drawing.Color.SteelBlue
        Me.lbl_result.Location = New System.Drawing.Point(106, 105)
        Me.lbl_result.Name = "lbl_result"
        Me.lbl_result.Size = New System.Drawing.Size(353, 33)
        Me.lbl_result.TabIndex = 8
        Me.lbl_result.Text = "lbl_result"
        Me.lbl_result.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_status
        '
        Me.lbl_status.AutoSize = True
        Me.lbl_status.Location = New System.Drawing.Point(12, 114)
        Me.lbl_status.Name = "lbl_status"
        Me.lbl_status.Size = New System.Drawing.Size(73, 13)
        Me.lbl_status.TabIndex = 9
        Me.lbl_status.Text = "Estado Actual"
        '
        'cmb_registertype
        '
        Me.cmb_registertype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_registertype.FormattingEnabled = True
        Me.cmb_registertype.Location = New System.Drawing.Point(172, 48)
        Me.cmb_registertype.Name = "cmb_registertype"
        Me.cmb_registertype.Size = New System.Drawing.Size(165, 21)
        Me.cmb_registertype.TabIndex = 10
        '
        'Trialmaker1
        '
        Me.Trialmaker1.ClaveMaestra = "TrialMaker"
        Me.Trialmaker1.ClavePrograma = "Test1"
        Me.Trialmaker1.Identificador = "121"
        Me.Trialmaker1.Imagen = Nothing
        Me.Trialmaker1.TituloRegistro = "Registro de Programa"
        Me.Trialmaker1.TripleDESKey = New Byte() {CType(21, Byte), CType(10, Byte), CType(64, Byte), CType(10, Byte), CType(100, Byte), CType(40, Byte), CType(200, Byte), CType(4, Byte), CType(21, Byte), CType(54, Byte), CType(65, Byte), CType(246, Byte), CType(5, Byte), CType(62, Byte), CType(1, Byte), CType(54, Byte), CType(54, Byte), CType(6, Byte), CType(8, Byte), CType(9, Byte), CType(65, Byte), CType(4, Byte), CType(65, Byte), CType(9, Byte)}
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 146)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Dias Restantes"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 173)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Ubicacion Clave"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 201)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Ubicacion Oculta"
        '
        'lbl_daylefts
        '
        Me.lbl_daylefts.BackColor = System.Drawing.Color.White
        Me.lbl_daylefts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbl_daylefts.Location = New System.Drawing.Point(106, 145)
        Me.lbl_daylefts.Name = "lbl_daylefts"
        Me.lbl_daylefts.Size = New System.Drawing.Size(162, 22)
        Me.lbl_daylefts.TabIndex = 14
        '
        'lbl_regfilepath
        '
        Me.lbl_regfilepath.BackColor = System.Drawing.Color.White
        Me.lbl_regfilepath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbl_regfilepath.Location = New System.Drawing.Point(106, 172)
        Me.lbl_regfilepath.Name = "lbl_regfilepath"
        Me.lbl_regfilepath.Size = New System.Drawing.Size(353, 22)
        Me.lbl_regfilepath.TabIndex = 15
        '
        'lbl_hidfilepath
        '
        Me.lbl_hidfilepath.BackColor = System.Drawing.Color.White
        Me.lbl_hidfilepath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbl_hidfilepath.Location = New System.Drawing.Point(106, 200)
        Me.lbl_hidfilepath.Name = "lbl_hidfilepath"
        Me.lbl_hidfilepath.Size = New System.Drawing.Size(353, 22)
        Me.lbl_hidfilepath.TabIndex = 16
        '
        'testmaker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(488, 248)
        Me.Controls.Add(Me.lbl_hidfilepath)
        Me.Controls.Add(Me.lbl_regfilepath)
        Me.Controls.Add(Me.lbl_daylefts)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmb_registertype)
        Me.Controls.Add(Me.lbl_status)
        Me.Controls.Add(Me.lbl_result)
        Me.Controls.Add(Me.btn_process)
        Me.Controls.Add(Me.lbl_identify)
        Me.Controls.Add(Me.txt_identificador)
        Me.Controls.Add(Me.btn_salir)
        Me.Controls.Add(Me.btn_borrar)
        Me.Controls.Add(Me.btn_register)
        Me.Name = "testmaker"
        Me.Text = "Programa de demostración del control TrialMaker"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_register As System.Windows.Forms.Button
    Friend WithEvents btn_borrar As System.Windows.Forms.Button
    Friend WithEvents btn_salir As System.Windows.Forms.Button
    Friend WithEvents Trialmaker1 As TrialMaker.trialmaker
    Friend WithEvents txt_identificador As System.Windows.Forms.TextBox
    Friend WithEvents lbl_identify As System.Windows.Forms.Label
    Friend WithEvents btn_process As System.Windows.Forms.Button
    Friend WithEvents lbl_result As System.Windows.Forms.Label
    Friend WithEvents lbl_status As Label
    Friend WithEvents cmb_registertype As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lbl_daylefts As Label
    Friend WithEvents lbl_regfilepath As Label
    Friend WithEvents lbl_hidfilepath As Label
End Class
