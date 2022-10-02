Public Class svgpath
    Dim err As String
    Dim segments As New List(Of struct_result)
    Dim __stack As New List(Of Matrix)

#Region "Constructor"
    Sub New()
        err = ""
        segments.Clear()
        __stack.Clear()
    End Sub
    Sub New(path As String)
        Dim pstate As struct_pathParse = pathParse(path)
        segments = pstate.segments
        err = pstate.err
        __stack.Clear()
    End Sub
#End Region

#Region "Procedimientos Públicos"
    Public Sub clone(source As svgpath)
        Me.err = source.err
        Me.segments.AddRange(source.segments)
    End Sub
    'Private m As Matrix
    Private m As Matrix
    Public Sub __matrix(mat As Matrix)
        ' Abandonar para matrices vacias
        m = mat
        If m.queue.Count = 0 Then Return
        iterate("matrix", True)
    End Sub
    ' Apply stacked commands
    Public Sub __evaluateStack()
        'Dim m As Matrix
        Dim i As Integer

        If __stack.Count = 0 Then Return

        If __stack.Count = 1 Then
            __matrix(__stack(0))
            __stack.Clear()
            Return
        End If

        m = New Matrix
        i = __stack.Count - 1

        While i >= 0
            m.matrix(__stack(i).toArray())
            i -= 1
        End While

        __matrix(m)
        __stack.Clear()
    End Sub
    Public Sub translate(x As Single, Optional y As Single = 0)
        Dim svg_matrix As New Matrix
        svg_matrix.translate(x, y)
        __stack.Add(svg_matrix)
    End Sub
    Public Sub scale(sx)
        Dim svg_matrix As New Matrix
        svg_matrix.scale(sx, sx)
        __stack.Add(svg_matrix)
    End Sub
    Public Sub scale(sx, sy)
        Dim svg_matrix As New Matrix
        svg_matrix.scale(sx, sy)
        __stack.Add(svg_matrix)
    End Sub
    ' Rotate path around point (sx [, sy]) sy = sx if Not defined
    Public Sub rotate(angle As Integer, Optional rx As Single = 0, Optional ry As Single = 0)
        Dim svg_matrix As New Matrix
        svg_matrix.rotate(angle, rx, ry)
        __stack.Add(svg_matrix)
    End Sub
    ' Skew path along the X axis by `degrees` angle
    Public Sub skewX(degrees)
        Dim svg_matrix As New Matrix
        svg_matrix.skewX(degrees)
        __stack.Add(svg_matrix)
    End Sub

    ' Skew path along the Y axis by `degrees` angle
    Public Sub skewY(degrees)
        Dim svg_matrix As New Matrix
        svg_matrix.skewY(degrees)
        __stack.Add(svg_matrix)
    End Sub

    ' Apply matrix transform (array of 6 elements)
    Public Sub matrix(m)
        Dim svg_matrix As New Matrix
        svg_matrix.matrix(m)
        __stack.Add(svg_matrix)
    End Sub

    ' Transform path according to "transform" attr of SVG spec
    Public Sub transform(transformString)
        If Not transformString.trim() Then
            Return
        End If
        __stack.Add(transformParse(transformString))
        Return
    End Sub

    Public Sub round(d)
        Dim contourStartDeltaX = 0, contourStartDeltaY = 0, deltaX = 0, deltaY = 0, l

        d = d Or 0

        __evaluateStack()
        For Each s As struct_result In segments
            Dim isRelative = (LCase(s.cmd) = s.cmd)

            Select Case s.cmd
                Case "H", "h"
                    If (isRelative) Then s.params(0) += deltaX
                    deltaX = s.params(0) - Math.Round(s.params(0), d)
                    s.params(0) = +Math.Round(s.params(0), d)
                    Continue For
                Case "V", "v"
                    If (isRelative) Then s.params(0) += deltaY
                    deltaY = s.params(0) - Math.Round(s.params(0), d)
                    s.params(0) = +Math.Round(s.params(0), d)
                    Continue For
                Case "Z", "z"
                    deltaX = contourStartDeltaX
                    deltaY = contourStartDeltaY
                    Continue For
                Case "M", "m"
                    If isRelative Then
                        s.params(0) += deltaX
                        s.params(1) += deltaY
                    End If

                    deltaX = s.params(0) - Math.Round(s.params(0), d)
                    deltaY = s.params(1) - Math.Round(s.params(1), d)

                    contourStartDeltaX = deltaX
                    contourStartDeltaY = deltaY

                    s.params(0) = +Math.Round(s.params(0), d)
                    s.params(1) = +Math.Round(s.params(1), d)
                    Continue For
                Case "A", "a"
                    ' [cmd, rx, ry, x-axis-rotation, large-arc-flag, sweep-flag, x, y]
                    If isRelative Then
                        s.params(5) += deltaX
                        s.params(6) += deltaY
                    End If

                    deltaX = s.params(5) - Math.Round(s.params(5), d)
                    deltaY = s.params(6) - Math.Round(s.params(6), d)

                    s.params(0) = +Math.Round(s.params(0), d)
                    s.params(1) = +Math.Round(s.params(1), d)
                    s.params(2) = +Math.Round(s.params(2), d + 2) ' better precision For rotation
                    s.params(5) = +Math.Round(s.params(5), d)
                    s.params(6) = +Math.Round(s.params(6), d)
                    Continue For

                Case Else
                    ' a c l q s t
                    l = s.params.Count

                    If isRelative Then
                        s.params(l - 2) += deltaX
                        s.params(l - 1) += deltaY
                    End If

                    deltaX = s.params(l - 2) - Math.Round(s.params(l - 2), d)
                    deltaY = s.params(l - 1) - Math.Round(s.params(l - 1), d)

                    For i = 0 To l - 1
                        If Not i Then Continue For
                        s.params(i) = +Math.Round(s.params(i), d)
                    Next
                    Continue For
            End Select
        Next
        Return
    End Sub

    Public Sub iterate(iterator As String, Optional keepLazyStack As Boolean = True)
        Dim local_segments As List(Of struct_result) = segments
        Dim newSegments As New List(Of struct_result)
        Dim needReplace As Boolean = False
        Dim lastX As Single = 0
        Dim lastY As Single = 0
        Dim countourStartX As Single = 0
        Dim countourStartY As Single = 0

        If Not keepLazyStack Then __evaluateStack()

        Dim replacements(local_segments.Count - 1)
        For index = 0 To local_segments.Count - 1
            Dim s As struct_result = local_segments(index)
            Dim Res
            Select Case iterator
                Case "matrix"
                    iterador_matrix(s, index, lastX, lastY)
                Case "abs"
                    iterador_abs(s, index, lastX, lastY)
                Case "rel"
                    iterador_rel(s, index, lastX, lastY)
                Case "unarc"
                    Res = iterador_unarc(s, index, lastX, lastY)
                    ' verify
                    If IsArray(Res) Then
                        replacements(index) = Res
                        needReplace = True
                    End If
                Case "unshort"
                    iterador_unshort(s, index, lastX, lastY)
            End Select

            Dim isRelative As Boolean = Char.IsLower(s.cmd)

            ' calculate absolute X And Y
            Select Case s.cmd
                Case "m", "M"
                    lastX = s.params(0) + If(isRelative, lastX, 0)
                    lastY = s.params(1) + If(isRelative, lastY, 0)
                    countourStartX = lastX
                    countourStartY = lastY
                Case "h", "H"
                    lastX = s.params(0) + If(isRelative, lastX, 0)
                Case "v", "V"
                    lastY = s.params(0) + If(isRelative, lastY, 0)
                Case "z", "Z"
                    ' That make sence for multiple contours
                    lastX = countourStartX
                    lastY = countourStartY
                Case Else
                    lastX = s.params(s.params.Count - 2) + If(isRelative, lastX, 0)
                    lastY = s.params(s.params.Count - 1) + If(isRelative, lastY, 0)
            End Select
            local_segments(index) = s
        Next index

        ' Replace segments if iterator return results
        If Not needReplace Then Return

        For i = 0 To segments.Count - 1
            If (TypeOf replacements(i) IsNot IList) Then
                For j = 0 To replacements(i).length - 1
                    newSegments.Add(replacements(i)(j))
                Next j
            Else
                newSegments.Add(segments(i))
            End If
        Next i
        segments = newSegments
        Return
    End Sub
#End Region

#Region "Funciones Publicas"
    Public Function to_String(Optional withSpaces As Boolean = False, Optional roundDecimal As Integer = -1) As String
        __evaluateStack()

        Dim result As String = ""
        Dim cmdSkipped As Boolean = False
        Dim cmd As Char, prevCmd As Char = ""
        Dim segment As struct_result

        For i = 0 To segments.Count - 1
            segment = segments(i)
            cmd = segment.cmd
            If withSpaces Then
                result += cmd & " "
                For pos = 0 To segment.params.Count - 1
                    If roundDecimal < 0 Then
                        result += CStr(segment.params(pos)) & " "
                    Else
                        result += CStr(Math.Round(segment.params(pos), roundDecimal)) & " "
                    End If
                Next pos
                result.Trim()
            Else
                ' Command Not repeating => store
                If cmd <> prevCmd Or cmd = "m" Or cmd = "M" Then
                    ' workaround for FontForge SVG importing bug, keep space between "z m".
                    If cmd = "m" And prevCmd = "z" Then result += " "
                    result += cmd
                    cmdSkipped = False
                Else
                    cmdSkipped = True
                End If

                ' Store segment params
                For pos = 0 To segment.params.Count - 1
                    Dim valor As Single = segment.params(pos)
                    ' Espaciado puede ser omitido
                    ' Siempre después de un comando y para valores negativos (con '-' al inicio)
                    If pos = 0 Then
                        If (cmdSkipped And valor >= 0) Then result += " "
                    ElseIf valor >= 0 Then
                        result += " "
                    End If
                    If roundDecimal < 0 Then
                        result += CStr(valor)
                    Else
                        result += CStr(Math.Round(valor, roundDecimal))
                    End If
                Next pos
                prevCmd = cmd
            End If
        Next
        result = result.Replace("E-", "e-")
        Return result
    End Function
#End Region

#Region "Funciones de Iteradores"
    Private Sub iterador_matrix(ByRef s As struct_result, index As Integer, x As Single, y As Single)
        Dim p As Single(), q As Single()
        Dim result As struct_result
        Dim isRelative As Boolean

        Select Case s.cmd
            Case "v"
                p = m.calc(0, s.params(0), True)
                result = If(p(0) = 0, addResult("v", {p(1)}), addResult("l", {p(0), p(1)}))
            Case "V"
                p = m.calc(x, s.params(0), False)
                q = m.calc(x, y, False)
                result = If(p(0) = q(0), addResult("V", {p(1)}), addResult("L", {p(0), p(1)}))
            Case "h"
                p = m.calc(s.params(0), 0, True)
                result = If(p(1) = 0, addResult("h", {p(0)}), addResult("l", {p(0), p(1)}))
            Case "H"
                p = m.calc(s.params(0), y, False)
                q = m.calc(x, y, False)
                result = If(p(1) = q(1), addResult("H", {p(0)}), addResult("L", {p(0), p(1)}))
            Case "a", "A"
                ' ARC Is: ['A', rx, ry, x-axis-rotation, large-arc-flag, sweep-flag, x, y]

                ' Transform rx, ry And the x-axis-rotation
                Dim ma = m.toArray()
                Dim e As New Ellipse(s.params(0), s.params(1), s.params(2))
                e.transform(ma)
                ' flip sweep-flag if matrix Is Not orientation-preserving
                If (ma(0) * ma(3) - ma(1) * ma(2) < 0) Then
                    s.params(4) = If(s.params(4), "0", "1")
                End If

                ' Transform end point as usual (without translation for relative notation)
                p = m.calc(s.params(5), s.params(6), s.cmd = "a")

                ' Empty arcs can be ignored by renderer, but should Not be dropped
                ' to avoid collisions with `S A S` And so on. Replace with empty line.
                If ((s.cmd = "A" And s.params(5) = x And s.params(6) = y) Or (s.cmd = "a" And s.params(5) = 0 And s.params(6) = 0)) Then
                    result = addResult(If(s.cmd = "a", "l", "L"), {p(0), p(1)})
                    Exit Select
                End If

                ' if the resulting ellipse Is (almost) a segment ...
                If (e.isDegenerate()) Then
                    ' replace the arc by a line
                    result = addResult(If(s.cmd = "a", "l", "L"), {p(0), p(1)})
                Else
                    ' if it Is a real ellipse
                    ' s[0], s[4] And s[5] are Not modified
                    result = addResult(s.cmd, {e.rx, e.ry, e.ax, s.params(3), s.params(4), p(0), p(1)})
                End If
            Case "m"
                isRelative = index > 0

                p = m.calc(s.params(0), s.params(1), isRelative)
                result = addResult("m", {p(0), p(1)})
            Case Else
                isRelative = Char.IsLower(s.cmd)
                Dim parametros As New List(Of Single)
                For i = 0 To s.params.Count - 1 Step 2
                    p = m.calc(s.params(i), s.params(i + 1), isRelative)
                    parametros.Add(p(0))
                    parametros.Add(p(1))
                Next
                result = addResult(s.cmd, parametros.ToArray)
        End Select

        'segments(index) = result
        s = result
    End Sub
    Private Sub iterador_abs(ByRef s As struct_result, index As Integer, x As Single, y As Single)
        Dim name As Char = s.cmd
        If Char.IsUpper(name) Then Return ' Saltar comandos absolutos
        s.cmd = UCase(name)
        Select Case name
            Case "v"
                s.params(0) += y
            Case "a"
                s.params(5) += x
                s.params(6) += y
            Case Else
                For i = 0 To s.params.Count - 1
                    s.params(i) += If(i Mod 2 = 0, x, y)
                Next i
        End Select
    End Sub
    Private Sub iterador_rel(ByRef s As struct_result, index As Integer, x As Single, y As Single)
        Dim name As Char = s.cmd
        If Char.IsLower(name) Then Return ' Saltar comandos relativos
        If index = 0 And name = "M" Then Return ' No tocar la primera M para evitar posibles equivocaciones
        s.cmd = LCase(name)
        Select Case name
            Case "V"
                s.params(0) -= y
            Case "A"
                s.params(5) -= x
                s.params(6) -= y
            Case Else
                For i = 0 To s.params.Count - 1
                    s.params(i) -= If(i Mod 2 = 0, x, y)
                Next i
        End Select
    End Sub
    Private Function iterador_unarc(s As struct_result, index As Integer, x As Single, y As Single)
        Dim new_segments As New List(Of struct_result)
        Dim nextX, nextY
        Dim result As New List(Of String())
        Dim name = s.cmd
        ' Skip anything except arcs
        If (name <> "A" And name <> "a") Then Return Nothing

        If name = "a" Then
            ' Convert relative arc coordinates to absolute
            nextX = x + s.params(5)
            nextY = y + s.params(6)
        Else
            nextX = s.params(5)
            nextY = s.params(6)
        End If

        'CHECKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
        'new_segments = a2c.a2c(x, y, nextX, nextY, s.params(3), s.params(4), s.params(0), s.params(1), s.params(2))

        ' Degenerated arcs can be ignored by renderer, but should Not be dropped
        ' to avoid collisions with `S A S` And so on. Replace with empty line.
        If (new_segments.Count = 0) Then
            Return {{If(s.cmd = "a", "l", "L"), s.params(5), s.params(6)}}
        End If
        For Each s In new_segments
            result.Add({"C", s.params(1), s.params(2), s.params(3), s.params(4), s.params(5), s.params(6)})
        Next

        Return result
    End Function
    Private Sub iterador_unshort(s As struct_result, idx As Integer, x As Single, y As Single)
        Dim local_segments = segments
        Dim prevControlX, prevControlY, prevSegment
        Dim curControlX, curControlY


        Dim name As Char = s.cmd
        Dim nameUC = UCase(name)
        Dim isRelative As Boolean

        ' First command MUST be M|m, it's safe to skip.
        ' Protect from access to [-1] for sure.
        If Not idx Then Return

        If (nameUC = "T") Then '// quadratic curve
            isRelative = (name = "t")
            prevSegment = local_segments(idx - 1)

            If prevSegment(0) = "Q" Then
                prevControlX = prevSegment(1) - x
                prevControlY = prevSegment(2) - y
            ElseIf prevSegment(0) = "q" Then
                prevControlX = prevSegment(1) - prevSegment(3)
                prevControlY = prevSegment(2) - prevSegment(4)
            Else
                prevControlX = 0
                prevControlY = 0
            End If
            curControlX = -prevControlX
            curControlY = -prevControlY

            If Not isRelative Then
                curControlX += x
                curControlY += y
            End If

            Dim temp_segment As struct_result
            temp_segment.cmd = If(isRelative, "q", "Q")
            temp_segment.params = {curControlX, curControlY, s.params(0), s.params(1)}
            local_segments(idx) = temp_segment

        ElseIf (nameUC = "S") Then
            isRelative = (name = "s")
            prevSegment = local_segments(idx - 1)

            If (prevSegment(0) = "C") Then
                prevControlX = prevSegment(3) - x
                prevControlY = prevSegment(4) - y
            ElseIf (prevSegment(0) = "c") Then
                prevControlX = prevSegment(3) - prevSegment(5)
                prevControlY = prevSegment(4) - prevSegment(6)
            Else
                prevControlX = 0
                prevControlY = 0
            End If

            curControlX = -prevControlX
            curControlY = -prevControlY

            If Not isRelative Then
                curControlX += x
                curControlY += y
            End If

            Dim temp_segment As struct_result
            temp_segment.cmd = If(isRelative, "c", "C")
            temp_segment.params = {curControlX, curControlY, s.params(0), s.params(1), s.params(2), s.params(3)}
            local_segments(idx) = temp_segment
        End If
    End Sub
#End Region
End Class