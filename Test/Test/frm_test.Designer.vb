<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frm_test
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_test))
        Me.lbl_textinput = New System.Windows.Forms.Label()
        Me.txt_textinput = New System.Windows.Forms.TextBox()
        Me.lbl_position = New System.Windows.Forms.Label()
        Me.lbl_posx = New System.Windows.Forms.Label()
        Me.lbl_posy = New System.Windows.Forms.Label()
        Me.lbl_font = New System.Windows.Forms.Label()
        Me.lbl_alignment = New System.Windows.Forms.Label()
        Me.lbl_fontsize = New System.Windows.Forms.Label()
        Me.num_posx = New System.Windows.Forms.NumericUpDown()
        Me.num_posy = New System.Windows.Forms.NumericUpDown()
        Me.cmb_selectfont = New System.Windows.Forms.ComboBox()
        Me.cmb_alignment = New System.Windows.Forms.ComboBox()
        Me.num_fontsize = New System.Windows.Forms.NumericUpDown()
        Me.num_charspace = New System.Windows.Forms.NumericUpDown()
        Me.lbl_charspace = New System.Windows.Forms.Label()
        Me.num_lineheight = New System.Windows.Forms.NumericUpDown()
        Me.lbl_lineheight = New System.Windows.Forms.Label()
        Me.num_rotate = New System.Windows.Forms.NumericUpDown()
        Me.lbl_rotate = New System.Windows.Forms.Label()
        Me.web_svg = New System.Windows.Forms.WebBrowser()
        Me.btn_apply = New System.Windows.Forms.Button()
        Me.check_box = New System.Windows.Forms.CheckBox()
        Me.btn_save = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.num_posx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.num_posy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.num_fontsize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.num_charspace, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.num_lineheight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.num_rotate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbl_textinput
        '
        Me.lbl_textinput.AutoSize = True
        Me.lbl_textinput.Location = New System.Drawing.Point(13, 10)
        Me.lbl_textinput.Name = "lbl_textinput"
        Me.lbl_textinput.Size = New System.Drawing.Size(55, 13)
        Me.lbl_textinput.TabIndex = 0
        Me.lbl_textinput.Text = "Input Text"
        '
        'txt_textinput
        '
        Me.txt_textinput.Location = New System.Drawing.Point(16, 26)
        Me.txt_textinput.Multiline = True
        Me.txt_textinput.Name = "txt_textinput"
        Me.txt_textinput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_textinput.Size = New System.Drawing.Size(233, 157)
        Me.txt_textinput.TabIndex = 1
        Me.txt_textinput.Text = resources.GetString("txt_textinput.Text")
        '
        'lbl_position
        '
        Me.lbl_position.AutoSize = True
        Me.lbl_position.Location = New System.Drawing.Point(13, 186)
        Me.lbl_position.Name = "lbl_position"
        Me.lbl_position.Size = New System.Drawing.Size(44, 13)
        Me.lbl_position.TabIndex = 2
        Me.lbl_position.Text = "Position"
        '
        'lbl_posx
        '
        Me.lbl_posx.AutoSize = True
        Me.lbl_posx.Location = New System.Drawing.Point(13, 204)
        Me.lbl_posx.Name = "lbl_posx"
        Me.lbl_posx.Size = New System.Drawing.Size(17, 13)
        Me.lbl_posx.TabIndex = 3
        Me.lbl_posx.Text = "X:"
        '
        'lbl_posy
        '
        Me.lbl_posy.AutoSize = True
        Me.lbl_posy.Location = New System.Drawing.Point(108, 202)
        Me.lbl_posy.Name = "lbl_posy"
        Me.lbl_posy.Size = New System.Drawing.Size(17, 13)
        Me.lbl_posy.TabIndex = 4
        Me.lbl_posy.Text = "Y:"
        '
        'lbl_font
        '
        Me.lbl_font.AutoSize = True
        Me.lbl_font.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lbl_font.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_font.ForeColor = System.Drawing.Color.Blue
        Me.lbl_font.Location = New System.Drawing.Point(13, 225)
        Me.lbl_font.Name = "lbl_font"
        Me.lbl_font.Size = New System.Drawing.Size(38, 13)
        Me.lbl_font.TabIndex = 5
        Me.lbl_font.Text = "Fonts"
        '
        'lbl_alignment
        '
        Me.lbl_alignment.AutoSize = True
        Me.lbl_alignment.Location = New System.Drawing.Point(13, 271)
        Me.lbl_alignment.Name = "lbl_alignment"
        Me.lbl_alignment.Size = New System.Drawing.Size(53, 13)
        Me.lbl_alignment.TabIndex = 6
        Me.lbl_alignment.Text = "Alignment"
        '
        'lbl_fontsize
        '
        Me.lbl_fontsize.AutoSize = True
        Me.lbl_fontsize.Location = New System.Drawing.Point(13, 320)
        Me.lbl_fontsize.Name = "lbl_fontsize"
        Me.lbl_fontsize.Size = New System.Drawing.Size(51, 13)
        Me.lbl_fontsize.TabIndex = 7
        Me.lbl_fontsize.Text = "Font Size"
        '
        'num_posx
        '
        Me.num_posx.DecimalPlaces = 2
        Me.num_posx.Location = New System.Drawing.Point(36, 202)
        Me.num_posx.Name = "num_posx"
        Me.num_posx.Size = New System.Drawing.Size(66, 20)
        Me.num_posx.TabIndex = 8
        '
        'num_posy
        '
        Me.num_posy.DecimalPlaces = 2
        Me.num_posy.Location = New System.Drawing.Point(131, 202)
        Me.num_posy.Name = "num_posy"
        Me.num_posy.Size = New System.Drawing.Size(66, 20)
        Me.num_posy.TabIndex = 9
        '
        'cmb_selectfont
        '
        Me.cmb_selectfont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_selectfont.FormattingEnabled = True
        Me.cmb_selectfont.Location = New System.Drawing.Point(13, 241)
        Me.cmb_selectfont.Name = "cmb_selectfont"
        Me.cmb_selectfont.Size = New System.Drawing.Size(236, 21)
        Me.cmb_selectfont.TabIndex = 10
        '
        'cmb_alignment
        '
        Me.cmb_alignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_alignment.FormattingEnabled = True
        Me.cmb_alignment.Items.AddRange(New Object() {"Izquierda", "Centro", "Derecha"})
        Me.cmb_alignment.Location = New System.Drawing.Point(13, 287)
        Me.cmb_alignment.Name = "cmb_alignment"
        Me.cmb_alignment.Size = New System.Drawing.Size(236, 21)
        Me.cmb_alignment.TabIndex = 11
        '
        'num_fontsize
        '
        Me.num_fontsize.Location = New System.Drawing.Point(183, 318)
        Me.num_fontsize.Maximum = New Decimal(New Integer() {150, 0, 0, 0})
        Me.num_fontsize.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.num_fontsize.Name = "num_fontsize"
        Me.num_fontsize.Size = New System.Drawing.Size(66, 20)
        Me.num_fontsize.TabIndex = 12
        Me.num_fontsize.Value = New Decimal(New Integer() {24, 0, 0, 0})
        '
        'num_charspace
        '
        Me.num_charspace.Location = New System.Drawing.Point(183, 342)
        Me.num_charspace.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.num_charspace.Name = "num_charspace"
        Me.num_charspace.Size = New System.Drawing.Size(66, 20)
        Me.num_charspace.TabIndex = 14
        '
        'lbl_charspace
        '
        Me.lbl_charspace.AutoSize = True
        Me.lbl_charspace.Location = New System.Drawing.Point(13, 344)
        Me.lbl_charspace.Name = "lbl_charspace"
        Me.lbl_charspace.Size = New System.Drawing.Size(95, 13)
        Me.lbl_charspace.TabIndex = 13
        Me.lbl_charspace.Text = "Character Spacing"
        '
        'num_lineheight
        '
        Me.num_lineheight.DecimalPlaces = 1
        Me.num_lineheight.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.num_lineheight.Location = New System.Drawing.Point(183, 367)
        Me.num_lineheight.Maximum = New Decimal(New Integer() {3, 0, 0, 0})
        Me.num_lineheight.Minimum = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.num_lineheight.Name = "num_lineheight"
        Me.num_lineheight.Size = New System.Drawing.Size(66, 20)
        Me.num_lineheight.TabIndex = 16
        Me.num_lineheight.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lbl_lineheight
        '
        Me.lbl_lineheight.AutoSize = True
        Me.lbl_lineheight.Location = New System.Drawing.Point(13, 369)
        Me.lbl_lineheight.Name = "lbl_lineheight"
        Me.lbl_lineheight.Size = New System.Drawing.Size(61, 13)
        Me.lbl_lineheight.TabIndex = 15
        Me.lbl_lineheight.Text = "Line Height"
        '
        'num_rotate
        '
        Me.num_rotate.Location = New System.Drawing.Point(183, 393)
        Me.num_rotate.Maximum = New Decimal(New Integer() {360, 0, 0, 0})
        Me.num_rotate.Minimum = New Decimal(New Integer() {360, 0, 0, -2147483648})
        Me.num_rotate.Name = "num_rotate"
        Me.num_rotate.Size = New System.Drawing.Size(66, 20)
        Me.num_rotate.TabIndex = 18
        '
        'lbl_rotate
        '
        Me.lbl_rotate.AutoSize = True
        Me.lbl_rotate.Location = New System.Drawing.Point(13, 395)
        Me.lbl_rotate.Name = "lbl_rotate"
        Me.lbl_rotate.Size = New System.Drawing.Size(47, 13)
        Me.lbl_rotate.TabIndex = 17
        Me.lbl_rotate.Text = "Rotation"
        '
        'web_svg
        '
        Me.web_svg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.web_svg.Location = New System.Drawing.Point(0, 0)
        Me.web_svg.MinimumSize = New System.Drawing.Size(20, 20)
        Me.web_svg.Name = "web_svg"
        Me.web_svg.Size = New System.Drawing.Size(519, 527)
        Me.web_svg.TabIndex = 19
        '
        'btn_apply
        '
        Me.btn_apply.Location = New System.Drawing.Point(16, 428)
        Me.btn_apply.Name = "btn_apply"
        Me.btn_apply.Size = New System.Drawing.Size(233, 36)
        Me.btn_apply.TabIndex = 20
        Me.btn_apply.Text = "Apply"
        Me.btn_apply.UseVisualStyleBackColor = True
        '
        'check_box
        '
        Me.check_box.AutoSize = True
        Me.check_box.Location = New System.Drawing.Point(212, 205)
        Me.check_box.Name = "check_box"
        Me.check_box.Size = New System.Drawing.Size(44, 17)
        Me.check_box.TabIndex = 22
        Me.check_box.Text = "Box"
        Me.check_box.UseVisualStyleBackColor = True
        '
        'btn_save
        '
        Me.btn_save.Location = New System.Drawing.Point(16, 470)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(233, 36)
        Me.btn_save.TabIndex = 23
        Me.btn_save.Text = "Download"
        Me.btn_save.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_textinput)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btn_save)
        Me.SplitContainer1.Panel1.Controls.Add(Me.txt_textinput)
        Me.SplitContainer1.Panel1.Controls.Add(Me.check_box)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_position)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btn_apply)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_posx)
        Me.SplitContainer1.Panel1.Controls.Add(Me.num_rotate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_posy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_rotate)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_font)
        Me.SplitContainer1.Panel1.Controls.Add(Me.num_lineheight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_alignment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_lineheight)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_fontsize)
        Me.SplitContainer1.Panel1.Controls.Add(Me.num_charspace)
        Me.SplitContainer1.Panel1.Controls.Add(Me.num_posx)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lbl_charspace)
        Me.SplitContainer1.Panel1.Controls.Add(Me.num_posy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.num_fontsize)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmb_selectfont)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmb_alignment)
        Me.SplitContainer1.Panel1MinSize = 260
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.web_svg)
        Me.SplitContainer1.Size = New System.Drawing.Size(790, 531)
        Me.SplitContainer1.SplitterDistance = 263
        Me.SplitContainer1.TabIndex = 24
        '
        'frm_test
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(790, 531)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MinimumSize = New System.Drawing.Size(800, 560)
        Me.Name = "frm_test"
        Me.Text = "CNC Text Tool"
        CType(Me.num_posx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.num_posy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.num_fontsize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.num_charspace, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.num_lineheight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.num_rotate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lbl_textinput As Label
    Friend WithEvents txt_textinput As TextBox
    Friend WithEvents lbl_position As Label
    Friend WithEvents lbl_posx As Label
    Friend WithEvents lbl_posy As Label
    Friend WithEvents lbl_font As Label
    Friend WithEvents lbl_alignment As Label
    Friend WithEvents lbl_fontsize As Label
    Friend WithEvents num_posx As NumericUpDown
    Friend WithEvents num_posy As NumericUpDown
    Friend WithEvents cmb_selectfont As ComboBox
    Friend WithEvents cmb_alignment As ComboBox
    Friend WithEvents num_fontsize As NumericUpDown
    Friend WithEvents num_charspace As NumericUpDown
    Friend WithEvents lbl_charspace As Label
    Friend WithEvents num_lineheight As NumericUpDown
    Friend WithEvents lbl_lineheight As Label
    Friend WithEvents num_rotate As NumericUpDown
    Friend WithEvents lbl_rotate As Label
    Friend WithEvents web_svg As WebBrowser
    Friend WithEvents btn_apply As Button
    Friend WithEvents check_box As CheckBox
    Friend WithEvents btn_save As Button
    Friend WithEvents SplitContainer1 As SplitContainer
End Class
