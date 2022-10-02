Public Class Ellipse
    Const epsilon = 0.0000000001
    Const torad = Math.PI / 180

    Public rx As Long
    Public ry As Long
    Public ax As Long
    Public Sub New(rx, ry, ax)
        Me.rx = rx
        Me.ry = ry
        Me.ax = ax
    End Sub
    Public Sub transform(m)
        Dim c = Math.Cos(ax * torad), s = Math.Sin(Me.ax * torad)
        Dim ma() = {rx * (m(0) * c + m(2) * s), rx * (m(1) * c + m(3) * s), ry * (-m(0) * s + m(2) * c), ry * (-m(1) * s + m(3) * c)}
        Dim J = ma(0) * ma(0) + ma(2) * ma(2)
        Dim K = ma(1) * ma(1) + ma(3) * ma(3)
        Dim D = ((ma(0) - ma(3)) * (ma(0) - ma(3)) + (ma(2) + ma(1)) * (ma(2) + ma(1))) * ((ma(0) + ma(3)) * (ma(0) + ma(3)) + (ma(2) - ma(1)) * (ma(2) - ma(1)))
        Dim JK = (J + K) / 2

        If (D < epsilon * JK) Then
            rx = ry = Math.Sqrt(JK)
            ax = 0
            Return
        End If

        Dim L = ma(0) * ma(1) + ma(2) * ma(3)
        D = Math.Sqrt(D)

        Dim l1 = JK + D / 2
        Dim l2 = JK - D / 2

        If (Math.Abs(L) < epsilon) And (Math.Abs(l1 - K) < epsilon) Then
            ax = 90
        Else
            ax = Math.Atan(If(Math.Abs(L) > Math.Abs(l1 - K), (l1 - J) / L, L / (l1 - K)))
        End If
        ax = ax * 180 / Math.PI

        If ax >= 0 Then
            rx = Math.Sqrt(l1)
            ry = Math.Sqrt(l2)
        Else
            ax += 90
            rx = Math.Sqrt(l2)
            ry = Math.Sqrt(l1)
        End If

        Return
    End Sub

    Public Function isDegenerate() As Boolean
        Return (rx < epsilon * ry Or ry < epsilon * rx)
    End Function
End Class