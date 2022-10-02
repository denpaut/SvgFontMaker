Imports System.Windows.Forms


Module loadfont
    'exportar esta estructura
    Structure pfont
        Dim d 'especificar tipo
        Dim unicode As String
        Dim name As String
        Dim width As Single
        Dim height As Integer
    End Structure

    Public Function parseFont(ByVal element As HtmlDocument, ByVal Optional size As Integer = 24) As pfont()
        Dim fontList As HtmlElementCollection = element.GetElementsByTagName("font")
        Dim svgFont As HtmlElement = fontList(0)
        Dim face_list As HtmlElementCollection = element.GetElementsByTagName("font-face")
        Dim svgFontface As HtmlElement = face_list(0)
        Dim svgGlyps As HtmlElementCollection = element.GetElementsByTagName("glyph")

        Dim fontHorizAdvX As String = svgFont.GetAttribute("horiz-adv-x")
        Dim fontAscent As String = svgFontface.GetAttribute("ascent")
        Dim fontUnitsPerEm As Integer = svgFontface.GetAttribute("units-per-em") Or 1000

        Dim EM As Integer = size ' Unit For the height
        Dim scale As Single = EM / fontUnitsPerEm

        Dim result(svgGlyps.Count - 1) As pfont
        For Each svgGlyph As HtmlElement In svgGlyps
            Dim d As String = svgGlyph.GetAttribute("d")
            Dim unicode As String = svgGlyph.GetAttribute("unicode")
            Dim name = svgGlyph.GetAttribute("glyph-name") Or ("glyph" + unicode)
            Dim width As Single = svgGlyph.GetAttribute("horiz-adv-x") Or fontHorizAdvX

            With result(unicode)
                If d Then
                    .d = New svgpath(d)
                    .d.translate(0, -fontAscent)
                    .d.scale(scale, -scale)
                    .d.abs()
                    .d.rel()
                    .d.To_String()
                Else
                    .d = vbNull
                End If
                .unicode = unicode
                .name = name
                .width = width * scale  'parse
                .height = EM
            End With
        Next
        Return result
    End Function
End Module