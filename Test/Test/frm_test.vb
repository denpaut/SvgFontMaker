Public Class frm_test
    Dim svg_File As String = ""
    Dim svg_texto As New SVG_text_tool.svgHershey

    Private Sub frm_test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmb_selectfont.Items.AddRange(svg_texto.getArrayOfFonts)
        cmb_selectfont.SelectedIndex = 0
        cmb_alignment.SelectedIndex = 0
    End Sub

#Region "Buttons Apply and Save"
    Private Sub btn_apply_Click(sender As Object, e As EventArgs) Handles btn_apply.Click
        svg_texto.textInput = txt_textinput.Text
        svg_texto.textPosX = num_posx.Value
        svg_texto.textPosY = num_posy.Value
        svg_texto.textRectangle = check_box.Checked
        svg_texto.textRotate = num_rotate.Value
        svg_texto.fontLineHeight = num_lineheight.Value
        svg_texto.fontCharSpacing = num_charspace.Value
        svg_texto.fontAlignment = cmb_alignment.SelectedIndex
        If svg_texto.fontSize <> num_fontsize.Value Or svg_texto.fontSelected <> cmb_selectfont.Text Then
            svg_texto.fontSelected = cmb_selectfont.Text
            svg_texto.fontSize = num_fontsize.Value
            svg_texto.loadFont(cmb_selectfont.Text, num_fontsize.Value)
        End If
        svg_texto.createTextPaths()
        svg_File = svg_texto.getStringFile()
        web_svg.DocumentText = svg_File
    End Sub

    Private Sub btn_save_Click_1(sender As Object, e As EventArgs) Handles btn_save.Click
        If svg_File = "" Then Return
        Dim sfd As New SaveFileDialog
        sfd.Filter = "SVG Files (*.svg*)|*.svg"
        If sfd.ShowDialog = DialogResult.OK Then
            My.Computer.FileSystem.WriteAllText(sfd.FileName, svg_File, False)
        End If
    End Sub

    Private Sub lbl_font_Click(sender As Object, e As EventArgs) Handles lbl_font.Click
        web_svg.Navigate(New Uri("https://gitlab.com/oskay/svg-fonts/-/raw/master/samples.png"))
    End Sub
#End Region
End Class