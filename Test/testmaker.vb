Public Class testmaker
    Private Sub frmInicio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Actualizar_Info()
        For i = 0 To Trialmaker1.nivel_acceso.Length - 1
            cmb_registertype.Items.Add(Trialmaker1.nivel_acceso(i).level)
        Next i
    End Sub

    Private Sub cmb_registertype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_registertype.SelectedIndexChanged
        txt_identificador.Text = Trialmaker1.nivel_acceso(cmb_registertype.SelectedIndex).value
    End Sub
    Private Sub btn_process_Click(sender As Object, e As EventArgs) Handles btn_process.Click
        Trialmaker1.Identificador = txt_identificador.Text
        Actualizar_Info()
    End Sub
    Private Sub btn_register_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_register.Click
        Trialmaker1.dialogoRegistro()
        Actualizar_Info(False)
    End Sub
    Private Sub btn_borrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_borrar.Click
        Dim a As DialogResult
        a = MsgBox("Esta seguro que desea borrar los registros", MsgBoxStyle.OkCancel, "Borrar Registro")
        If a = DialogResult.OK Then Trialmaker1.borrarRegistros()
        Actualizar_Info()
    End Sub
    Private Sub btn_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_salir.Click
        Application.Exit()
    End Sub
    Private Sub Actualizar_Info(Optional ByVal procesar As Boolean = True)
        If procesar = True Then Trialmaker1.Procesar()
        lbl_result.Text = Trialmaker1.Estado.ToString
        lbl_daylefts.Text = Trialmaker1.DiasRestantes
        lbl_regfilepath.Text = Trialmaker1.RutaRegistro
        lbl_hidfilepath.Text = Trialmaker1.RutaRegOculto
    End Sub
End Class
