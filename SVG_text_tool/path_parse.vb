Module path_parse

#Region "Constantes y Diccionarios"
    Dim paramCounts As New Dictionary(Of Char, Integer) From {{"a", 7}, {"c", 6}, {"h", 1}, {"l", 2}, {"m", 2}, {"r", 4}, {"q", 4}, {"s", 4}, {"t", 2}, {"v", 1}, {"z", 0}}
    Dim SPECIAL_SPACES() As Integer = {&H1680, &H180E, &H2000, &H2001, &H2002, &H2003, &H2004, &H2005, &H2006, &H2007, &H2008, &H2009, &H200A, &H202F, &H205F, &H3000, &HFEFF}
#End Region

#Region "Estructuras Publicas"
    Public Structure struct_result
        Dim cmd As Char
        Dim params() As Single
    End Structure
    Public Structure struct_pathParse
        Dim err As String
        Dim segments As List(Of struct_result)
    End Structure
#End Region

#Region "Clase State"
    Private Class State
        Public index As Integer
        Public path As String
        Public max As Integer
        Public result As New List(Of struct_result)
        Public param As Single
        Public err As String
        Public segmentStart As Long
        Public data As New List(Of Single)
        Sub New(ByVal myPath As String)
            index = 0
            path = myPath
            max = myPath.Length
            result.Clear()
            param = 0
            err = ""
            segmentStart = 0
            data.Clear()
        End Sub
    End Class
#End Region

#Region "Funciones Privadas"
    Private Function isSpace(ch As Integer) As Boolean
        Dim line_terminators As Boolean = (ch = &HA) Or (ch = &HD) Or (ch = &H2028) Or (ch = &H2029)
        Dim white_spaces As Boolean = (ch = &H20) Or (ch = &H9) Or (ch = &HB) Or (ch = &HC) Or (ch = &HA0)
        Dim spec_spaces As Boolean = ch >= &H1680 And (Array.IndexOf(SPECIAL_SPACES, ch) >= 0)
        Return line_terminators Or white_spaces Or spec_spaces
    End Function
    Private Function isCommand(code As Integer) As Boolean
        Dim fcode As Integer = (code Or &H20)
        Dim out As Boolean = False
        Select Case fcode
            Case &H6D : out = True'm
            Case &H7A : out = True'z
            Case &H6C : out = True'l
            Case &H68 : out = True'h
            Case &H76 : out = True'v
            Case &H63 : out = True'c
            Case &H73 : out = True's
            Case &H71 : out = True'q
            Case &H74 : out = True't
            Case &H61 : out = True'a
            Case &H72 : out = True 'r
        End Select
        Return out
    End Function
    Private Function isArc(code As Integer) As Boolean
        Return (code Or &H20) = &H61
    End Function
    Private Function isDigit(code As Integer) As Boolean
        Return (code >= 48 And code <= 57)
    End Function
    Private Function isDigitStart(code As Integer) As Boolean
        Dim digit09 As Boolean = (code >= &H30 And code <= &H39)
        Dim digitpm As Boolean = (code = &H2B) Or (code = &H2D) Or (code = &H2E)
        Return digit09 Or digitpm
    End Function
#End Region

#Region "Procedimientos Privados"
    Private Sub skipSpaces(ByVal state As State)
        If state.index = state.max Then Return
        While isSpace(Convert.ToInt32(state.path.Chars(state.index)))
            state.index += 1
            If state.index = state.max Then Exit While
        End While
    End Sub
    Private Sub scanFlag(ByVal state As State)
        Dim ch = Asc(state.path.Chars(state.index))
        If ch = &H30 Then
            state.param = 0
            state.index += 1
            Return
        End If
        If ch = &H31 Then
            state.param = 1
            state.index += 1
            Return
        End If
        state.err = "SvgPath: arc flag can be 0 or 1 only (at pos " + state.index + ")"
    End Sub
    Private Sub scanParam(ByVal state As State)
        Dim start As Integer = state.index, index As Integer = start, max As Integer = state.max
        Dim zeroFirst As Boolean = False, hasCeiling As Boolean = False
        Dim hasDecimal As Boolean = False, hasDot As Boolean = False

        If index >= max Then
            state.err = "SvgPath: missed param (at pos " + index + ")"
            Return
        End If

        Dim ch As Integer = Convert.ToUInt32(state.path.Chars(index))
        'si character is  or -
        If ch = &H2B Or ch = &H2D Then
            index += 1
            ch = If(index < max, Convert.ToUInt32(state.path.Chars(index)), 0)
        End If

        If Not isDigit(ch) And ch <> &H2E Then
            state.err = "SvgPath: param should start with 0..9 or `.` (at pos " + index + ")"
            Return
        End If
        'si characters is diferente de .
        If ch <> &H2E Then
            zeroFirst = (ch = &H30)
            index += 1
            ch = If(index < max, Convert.ToInt32(state.path.Chars(index)), 0)

            If (zeroFirst And index < max) Then
                If ch And isDigit(ch) Then
                    state.err = "SvgPath: numbers started with `0` such as `09` are illegal (at pos " + start + ")"
                    Return
                End If
            End If

            If index < max Then
                While isDigit(Convert.ToUInt32(state.path.Chars(index)))
                    index += 1
                    hasCeiling = True
                    If index = max Then Exit While
                End While
                ch = If(index < max, Convert.ToUInt32(state.path.Chars(index)), 0)
            End If
        End If

        If ch = &H2E Then
            hasDot = True
            index += 1
            While isDigit(Convert.ToUInt32(state.path.Chars(index)))
                index += 1
                hasDecimal = True
                If index = max Then Exit While
            End While
            ch = If(index < max, Convert.ToUInt32(state.path.Chars(index)), 0)
        End If

        If ch = &H65 Or ch = &H45 Then
            If hasDot And Not hasCeiling And Not hasDecimal Then
                state.err = "SvgPath: invalid float exponent (at pos " + index + ")"
                Return
            End If
            index += 1

            ch = If(index < max, Convert.ToUInt32(state.path.Chars(index)), 0)
            If (ch = &H2B Or ch = &H2D) Then
                index += 1
            End If
            If (index < max And isDigit(Convert.ToUInt32(state.path.Chars(index)))) Then
                While isDigit(Convert.ToUInt32(state.path.Chars(index)))
                    index += 1
                    If index = max Then Exit While
                End While
            Else
                state.err = "SvgPath: invalid float exponent (at pos " + index + ")"
                Return
            End If
        End If

        state.index = index
        state.param = Single.Parse(state.path.Substring(start, index - start))
    End Sub
    Private Sub finalizeSegment(ByVal state As State)
        Dim cmd As Char = state.path(state.segmentStart)
        Dim cmdLC As Char = LCase(cmd)
        Dim params() As Single = state.data.ToArray

        If cmdLC = "m" And params.Count > 2 Then
            params = params.Skip(2).ToArray 'slice(2) : copia con inicio = 2 hasta el final
            state.result.Add(addResult(cmd, params))
            cmdLC = "l"
            cmd = If(cmd = "m", "l", "L")
        End If

        If cmdLC = "r" Then
            state.result.Add(addResult(cmd, params))
        Else
            Dim pCount As Integer = paramCounts.Item(cmdLC)
            While params.Count >= pCount
                state.result.Add(addResult(cmd, params.Take(pCount).ToArray)) 'splice 0, paramCounts : remover n elementos desde la pos 0, devuelve un array con los elementos eliminados
                params = params.Skip(pCount).ToArray
                If Not (pCount > 0) Then Exit While
            End While
        End If
    End Sub
    Private Sub scanSegment(ByVal state As State)
        state.segmentStart = state.index
        Dim cmdCode As Integer = Convert.ToInt32(state.path.Chars(state.index))
        Dim is_arc As Boolean = isArc(cmdCode)

        If Not isCommand(cmdCode) Then
            state.err = "SvgPath: bad command " + state.path(state.index) + " (at pos " + state.index + ")"
            Return
        End If

        Dim need_params As Integer = paramCounts.Item(LCase(state.path(state.index)))

        state.index += 1
        skipSpaces(state)

        state.data.Clear()

        If Not (need_params > 0) Then
            finalizeSegment(state)
            Return
        End If

        Dim max As Integer = state.max
        Dim comma_found As Boolean = False

        For j = 0 To 1 Step 0
            For i = need_params To 1 Step -1
                If (is_arc And (i = 3 Or i = 4)) Then scanFlag(state) Else scanParam(state)
                If state.err.Length Then Return

                state.data.Add(state.param)
                skipSpaces(state)
                comma_found = False
                If state.index < max Then
                    If Convert.ToUInt32(state.path.Chars(state.index)) = &H2C Then
                        state.index += 1
                        skipSpaces(state)
                        comma_found = True
                    End If
                End If
            Next

            If comma_found Then Continue For
            If state.index >= state.max Then Exit For
            If state.index = max Then Exit For
            If Not isDigitStart(Convert.ToUInt32(state.path.Chars(state.index))) Then Exit For
        Next

        finalizeSegment(state)
    End Sub
#End Region

#Region "Funciones Públicas"
    Public Function addResult(comando As Char, parametros() As Single) As struct_result
        addResult.cmd = comando
        addResult.params = parametros
    End Function
    Public Function pathParse(ByVal svgPath As String) As struct_pathParse
        Dim State = New State(svgPath)
        Dim max = State.max

        skipSpaces(State)

        While (State.index < max And Not State.err.Length)
            scanSegment(State)
        End While

        If State.err.Length Then
            State.result.Clear()
        ElseIf State.result.Count Then
            If ("mM".IndexOf(State.result(0).cmd) < 0) Then
                State.err = "SvgPath: string should start with 'M' or 'm'"
                State.result.Clear()
            Else
                State.result(0) = addResult("M", State.result(0).params)
            End If
        End If
        pathParse.err = State.err
        pathParse.segments = State.result
    End Function
#End Region

End Module
