Imports System.Text.RegularExpressions

Module transform_parse
    'Dim Matrix As Matrix = New Matrix()
    Dim operations As Dictionary(Of String, Boolean) = New Dictionary(Of String, Boolean) From {{"matrix", True}, {"scale", True}, {"rotate", True}, {"translate", True}, {"skewX", True}, {"skewY", True}}

    Dim CMD_SPLIT_RE = "\ s * (Matrix|translate|scale|rotate|skewX|skewY)\s*\(\s*(.+?)\s*\)[\s,]*"
    Dim PARAMS_SPLIT_RE = "[\s,]+"

    Public Function transformParse(transformString)
        Dim matrix = New Matrix()
        Dim cmd As String = ""
        Dim i As Integer

        Dim transform_split() As String = Regex.Split(transformString, CMD_SPLIT_RE)
        For Each item As String In transform_split
            ' Saltar elementos vacios
            If Not item.Length Then Continue For
            ' Recordar operación
            If operations.ContainsKey(item) Then
                cmd = item
                Continue For
            End If
            ' Extraer parametros y atributos a la matriz
            Dim item_split() As String = Regex.Split(item, PARAMS_SPLIT_RE)
            Dim params(item_split.Count - 1)
            For i = 0 To item_split.Count - 1
                params(i) = +i Or 0
            Next i
            ' Si la cantidad de parámetros no es correcto - ignorar el comando
            Select Case cmd
                Case "matrix"
                    If params.Count = 6 Then matrix.matrix(params)
                    Continue For
                Case "scale"
                    If params.Count = 1 Then
                        matrix.scale(params(0), params(0))
                    ElseIf params.Count = 2 Then
                        matrix.scale(params(0), params(1))
                    End If
                    Continue For
                Case "rotate"
                    If params.Count = 1 Then
                        matrix.rotate(params(0), 0, 0)
                    ElseIf params.Count = 3 Then
                        matrix.rotate(params(0), params(1), params(2))
                    End If
                    Continue For
                Case "translate"
                    If params.Count = 1 Then
                        matrix.translate(params(0), 0)
                    ElseIf params.Count = 2 Then
                        matrix.translate(params(0), params(1))
                    End If
                    Continue For
                Case "skewX"
                    If params.Length = 1 Then matrix.skewX(params(0))
                    Continue For
                Case "skewY"
                    If params.Length = 1 Then matrix.skewY(params(0))
                    Continue For
            End Select
        Next item

        Return matrix
    End Function
End Module
