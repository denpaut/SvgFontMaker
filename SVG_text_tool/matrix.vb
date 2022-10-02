Public Class Matrix
    Public queue As New List(Of Single())
    Public cache() As Single

    Private Shared Function combine(m1 As Single(), m2 As Single()) As Single()
        Dim matriz(5) As Single
        matriz(0) = m1(0) * m2(0) + m1(2) * m2(1)
        matriz(1) = m1(1) * m2(0) + m1(3) * m2(1)
        matriz(2) = m1(0) * m2(2) + m1(2) * m2(3)
        matriz(3) = m1(1) * m2(2) + m1(3) * m2(3)
        matriz(4) = m1(0) * m2(4) + m1(2) * m2(5) + m1(4)
        matriz(5) = m1(1) * m2(4) + m1(3) * m2(5) + m1(5)
        Return matriz
    End Function

    Public Sub New()
        queue.Clear()
        cache = Nothing
    End Sub

    Public Sub matrix(m)
        If m(0) = 1 And m(1) = 0 And m(2) = 0 And m(3) = 1 And m(4) = 0 And m(5) = 0 Then Return
        cache = Nothing
        queue.Add(m)
    End Sub

    Public Sub translate(tx, ty)
        If tx <> 0 Or ty <> 0 Then
            cache = Nothing
            queue.Add({1, 0, 0, 1, tx, ty})
        End If
    End Sub

    Public Sub scale(sx, sy)
        If sx <> 1 Or sy <> 1 Then
            cache = Nothing
            queue.Add({sx, 0, 0, sy, 0, 0})
        End If
    End Sub

    Public Sub rotate(angle, rx, ry)
        Dim rad, cos, sin
        If angle = 0 Then Return
        translate(rx, ry)
        rad = angle * Math.PI / 180
        cos = Math.Cos(rad)
        sin = Math.Sin(rad)
        queue.Add({cos, sin, -sin, cos, 0, 0})
        cache = Nothing
        translate(-rx, -ry)
    End Sub

    Public Sub skewX(angle)
        If (angle <> 0) Then
            cache = Nothing
            queue.Add({1, 0, Math.Tan(angle * Math.PI / 180), 1, 0, 0})
        End If
    End Sub

    Public Sub skewY(angle)
        If (angle <> 0) Then
            cache = Nothing
            queue.Add({1, Math.Tan(angle * Math.PI / 180), 0, 1, 0, 0})
        End If
    End Sub

    Public Function toArray() As Single()
        If cache IsNot Nothing Then Return cache

        If queue.Count = 0 Then
            cache = {1, 0, 0, 1, 0, 0}
            Return cache
        End If

        cache = queue(0)

        If queue.Count = 1 Then Return cache

        For i = 1 To queue.Count - 1
            cache = combine(cache, queue(i))
        Next

        Return cache
    End Function

    Public Function calc(ByVal x As Single, ByVal y As Single, ByVal isRelative As Boolean) As Single()
        If queue.Count = 0 Then Return {x, y}

        If cache Is Nothing Then cache = toArray()
        Dim m As Single() = cache

        Return {x * m(0) + y * m(2) + If(isRelative, 0, m(4)), x * m(1) + y * m(3) + If(isRelative, 0, m(5))}
    End Function
End Class