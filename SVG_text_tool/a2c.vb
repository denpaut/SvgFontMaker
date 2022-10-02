Module a2c
    Const TAU = Math.PI * 2
    Private Function unit_vector_angle(ux As Single, uy As Single, vx As Single, vy As Single) As Single
        Dim dot As Single = ux * vx + uy * vy
        If (dot > 1) Then dot = 1
        If (dot < -1) Then dot = -1
        dot = Math.Acos(dot)
        If (ux * vy - uy * vx) < 0 Then dot = -dot
        Return dot
    End Function
    Private Function get_arc_center(x1, y1, x2, y2, fa, fs, rx, ry, sin_phi, cos_phi) As Single()
        Dim x1p As Single = cos_phi * (x1 - x2) / 2 + sin_phi * (y1 - y2) / 2
        Dim y1p As Single = -sin_phi * (x1 - x2) / 2 + cos_phi * (y1 - y2) / 2
        Dim rx_sq As Single = rx * rx
        Dim ry_sq As Single = ry * ry
        Dim x1p_sq As Single = x1p * x1p
        Dim y1p_sq As Single = y1p * y1p

        Dim radicant As Single = (rx_sq * ry_sq) - (rx_sq * y1p_sq) - (ry_sq * x1p_sq)
        If radicant < 0 Then radicant = 0
        radicant /= (rx_sq * y1p_sq) + (ry_sq * x1p_sq)
        radicant = Math.Sqrt(radicant) * If(fa = fs, -1, 1)
        Dim cxp As Single = radicant * rx / ry * y1p
        Dim cyp As Single = radicant * -ry / rx * x1p

        Dim cx As Single = cos_phi * cxp - sin_phi * cyp + (x1 + x2) / 2
        Dim cy As Single = sin_phi * cxp + cos_phi * cyp + (y1 + y2) / 2

        Dim v1x As Single = (x1p - cxp) / rx
        Dim v1y As Single = (y1p - cyp) / ry
        Dim v2x As Single = (-x1p - cxp) / rx
        Dim v2y As Single = (-y1p - cyp) / ry

        Dim theta1 As Single = unit_vector_angle(1, 0, v1x, v1y)
        Dim delta_theta As Single = unit_vector_angle(v1x, v1y, v2x, v2y)

        If (fs = 0 And delta_theta > 0) Then delta_theta -= TAU
        If (fs = 1 And delta_theta < 0) Then delta_theta += TAU
        Return {cx, cy, theta1, delta_theta}
    End Function
    Private Function approximate_unit_arc(theta1, delta_theta) As Single()
        Dim alpha As Single = 4 / 3 * Math.Tan(delta_theta / 4)
        Dim x1 As Single = Math.Cos(theta1)
        Dim y1 As Single = Math.Sin(theta1)
        Dim x2 As Single = Math.Cos(theta1 + delta_theta)
        Dim y2 As Single = Math.Sin(theta1 + delta_theta)
        Return {x1, y1, x1 - y1 * alpha, y1 + x1 * alpha, x2 + y2 * alpha, y2 - x2 * alpha, x2, y2}
    End Function
    Public Function a2c(x1, y1, x2, y2, fa, fs, rx, ry, phi) As List(Of Single())
        Dim sin_phi As Single = Math.Sin(phi * TAU / 360)
        Dim cos_phi As Single = Math.Cos(phi * TAU / 360)
        Dim x1p As Single = cos_phi * (x1 - x2) / 2 + sin_phi * (y1 - y2) / 2
        Dim y1p As Single = -sin_phi * (x1 - x2) / 2 + cos_phi * (y1 - y2) / 2

        If (x1p = 0 And y1p = 0) Then Return Nothing
        If (rx = 0 Or ry = 0) Then Return Nothing

        rx = Math.Abs(rx)
        ry = Math.Abs(ry)

        Dim lambda As Single = (x1p * x1p) / (rx * rx) + (y1p * y1p) / (ry * ry)
        If (lambda > 1) Then
            rx *= Math.Sqrt(lambda)
            ry *= Math.Sqrt(lambda)
        End If

        Dim cc() = get_arc_center(x1, y1, x2, y2, fa, fs, rx, ry, sin_phi, cos_phi)

        Dim result As New List(Of Single())
        Dim theta1 = cc(2)
        Dim delta_theta = cc(3)

        Dim segments As Single = Math.Max(Math.Ceiling(Math.Abs(delta_theta) / (TAU / 4)), 1)
        delta_theta /= segments

        For i = 0 To segments - 1
            result.Add(approximate_unit_arc(theta1, delta_theta))
            theta1 += delta_theta
        Next i

        For Each Curve As Single() In result
            For i = 0 To Curve.Count - 1 Step 2
                Dim x = Curve(i + 0)
                Dim y = Curve(i + 1)
                ' scale
                x *= rx
                y *= ry
                ' rotate
                Dim xp = cos_phi * x - sin_phi * y
                Dim yp = sin_phi * x + cos_phi * y
                ' translate
                Curve(i + 0) = xp + cc(0)
                Curve(i + 1) = yp + cc(1)
            Next i
        Next
        Return result
    End Function
End Module
