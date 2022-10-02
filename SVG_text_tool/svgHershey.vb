Public Class svgHershey

#Region "Constantes y Variables"
    Private Const FONTS_PATH As String = "svg_fonts" ' Localización de Carpeta de fuentes

    Private text As String = "Lorem Ipsum"
    Private rotation As Integer = 0
    Private rectangle As Boolean = False
    Private paths As New List(Of svgpath)
    Private font As str_font = New str_font("HersheySans1", 24)
    Private fonts As New Dictionary(Of String, str_hershey)
    Private pos As New Drawing.PointF(0, 0)
    Private maxRect As New Drawing.RectangleF(0, 0, 0, 0)
#End Region

#Region "Enumeraciones y Estructuras"
    Enum textAlignment
        left = 0
        center = 1
        right = 2
    End Enum
    Structure HersheySymbol
        Dim d As svgpath
        Dim unicode As Integer
        Dim name As String
        Dim width As Single
        Dim height As Integer
    End Structure
    Structure str_hershey
        Public data() As HersheySymbol
        Public str As String
        Public size As Integer
    End Structure
#End Region

#Region "Clases Privadas"
    Private Class str_font
        Public alignment As textAlignment
        Public selected As String
        'Public sizeInPixels As Integer
        Public size As Integer
        Public lineHeight As Single
        Public strokeWidth As Integer
        'Public widthUnit As String
        Public characterSpacing As Integer
        Sub New(fuente As String, tamano As Integer)
            alignment = textAlignment.left
            selected = fuente
            'sizeInPixels = tamano
            size = tamano
            lineHeight = 1
            strokeWidth = 1
            'widthUnit = "px"
            characterSpacing = 0
        End Sub
    End Class
    Private Class xmlText
        Dim xmlLine As String
        Sub New(codigo As String)
            xmlLine = "<" & codigo & " "
        End Sub
        Public Sub add(par As String, value As Single)
            xmlLine += par & "=" & ControlChars.Quote & value & ControlChars.Quote & " "
        End Sub
        Public Sub add(par As String, value As String)
            xmlLine += par & "=" & ControlChars.Quote & value & ControlChars.Quote & " "
        End Sub
        Public Sub addPos(x As Single, y As Single)
            xmlLine += "x=" & ControlChars.Quote & x & ControlChars.Quote & " y=" & ControlChars.Quote & y & ControlChars.Quote & " "
        End Sub
        Public Sub addRect(w As Single, h As Single)
            xmlLine += "width=" & ControlChars.Quote & w & ControlChars.Quote & " height=" & ControlChars.Quote & h & ControlChars.Quote & " "
        End Sub
        Public Sub addRect(x As Single, y As Single, w As Single, h As Single)
            addPos(x, y)
            addRect(w, h)
        End Sub
        Public Function str(Optional selClose As Boolean = True) As String
            If selClose Then Return xmlLine & "/>" & vbCrLf Else Return xmlLine
        End Function
    End Class
#End Region

#Region "Propiedades Públicas"
    Public Property textInput As String
        Get
            Return text
        End Get
        Set(value As String)
            text = value
        End Set
    End Property
    Public Property textPosX As Single
        Get
            Return pos.X
        End Get
        Set(value As Single)
            pos.X = value
        End Set
    End Property
    Public Property textPosY As Single
        Get
            Return pos.Y
        End Get
        Set(value As Single)
            pos.Y = value
        End Set
    End Property
    Public Property textRotate As Integer
        Get
            Return rotation
        End Get
        Set(value As Integer)
            rotation = value
        End Set
    End Property
    Public Property textRectangle As Boolean
        Get
            Return rectangle
        End Get
        Set(value As Boolean)
            rectangle = value
        End Set
    End Property
    Public Property fontSelected As String
        Get
            Return font.selected
        End Get
        Set(value As String)
            If fonts.ContainsKey(value) Then font.selected = value
        End Set
    End Property
    Public Property fontSize As Integer
        Get
            Return font.size
        End Get
        Set(value As Integer)
            If value < 10 Then
                font.size = 10
            ElseIf value > 150 Then
                font.size = 150
            Else
                font.size = value
            End If
        End Set
    End Property
    Public Property fontLineHeight As Single
        Get
            Return font.lineHeight
        End Get
        Set(value As Single)
            If value < 0.5 Then
                font.lineHeight = 0.5
            ElseIf value > 3 Then
                font.lineHeight = 3
            Else
                font.lineHeight = CSng(value)
            End If
        End Set
    End Property
    Public Property fontCharSpacing As Integer
        Get
            Return font.characterSpacing
        End Get
        Set(value As Integer)
            font.characterSpacing = value
        End Set
    End Property
    Public Property fontAlignment As textAlignment
        Get
            Return font.alignment
        End Get
        Set(value As textAlignment)
            font.alignment = value
        End Set
    End Property
#End Region

#Region "Constructor"
    Sub New()
        Dim hershey As New str_hershey With {.data = Nothing, .str = "", .size = 24}
        Dim installed_fonts As String() = getArrayOfFonts()
        For Each fuente As String In installed_fonts
            fonts.Add(fuente, hershey)
        Next
    End Sub
#End Region

#Region "Funciones Privadas"
    Private Function parseFont(ByVal elemento As XDocument, ByVal Optional size As Integer = 24) As HersheySymbol()
        Dim xmlns As String = "{http://www.w3.org/2000/svg}" 'www.w3.org/1999/xlink2

        Dim svgFont = elemento.Descendants(xmlns & "font").First
        Dim svgFontface = elemento.Descendants(xmlns & "font-face").First
        Dim svgGlyps = elemento.Descendants(xmlns & "glyph")

        Dim fontHorizAdvX As Integer = CInt(svgFont.Attribute("horiz-adv-x").Value)
        Dim fontAscent As Integer = CInt(svgFontface.Attribute("ascent").Value) 'descent, cap-height, x-height
        Dim xfontUnitsPerEm As XAttribute = svgFontface.Attribute("units-per-em")
        Dim fontUnitsPerEm As Integer = If(xfontUnitsPerEm IsNot Nothing, xfontUnitsPerEm.Value, 1000)

        Dim EM As Integer = size
        Dim scale As Single = EM / fontUnitsPerEm

        Dim result(255) As HersheySymbol 'solo procesaremos los carácteres ascci

        For Each svgGlyph As XElement In svgGlyps
            Dim d = svgGlyph.Attribute("d")
            Dim unichar As Char = svgGlyph.Attribute("unicode").Value
            Dim unicode As Integer = Convert.ToInt32(unichar)
            If unicode > 255 Then Continue For

            Dim xname As XAttribute = svgGlyph.Attribute("glyph-name")
            Dim name As String = If(xname IsNot Nothing, xname.Value, ("glyph" + unicode))
            Dim xwidth As XAttribute = svgGlyph.Attribute("horiz-adv-x")
            Dim width As Integer = If(xwidth IsNot Nothing, CInt(xwidth.Value), fontHorizAdvX)

            With result(unicode)
                If d IsNot Nothing Then
                    .d = New svgpath(d.Value)
                    .d.translate(0, -fontAscent)
                    .d.scale(scale, -scale)
                    '.d.iterate("abs")
                    .d.iterate("rel")
                    .d.to_String(True)
                Else
                    .d = Nothing
                End If
                .unicode = unicode
                .name = name
                .width = width * scale
                .height = EM
            End With
        Next
        Return result
    End Function
    Private Function rotateRectangle(rect As Drawing.RectangleF, angle As Integer) As Drawing.RectangleF
        Dim nRect As Drawing.RectangleF
        Dim rads As Double = Math.PI * angle / 180
        Dim cose As Single = Math.Abs(CSng(Math.Cos(rads)))
        Dim sine As Single = Math.Abs(CSng(Math.Sin(rads)))
        nRect.Width = rect.Width * cose + rect.Height * sine
        nRect.Height = rect.Width * sine + rect.Height * cose
        nRect.X = (nRect.Width - rect.Width) / 2
        nRect.Y = (nRect.Height - rect.Height) / 2
        Return nRect
    End Function
#End Region

#Region "Procedimientos Públicos"
    Public Sub loadFont(Optional selFont As String = "", Optional selSize As Integer = 0)
        If selFont <> "" Then
            If fonts.ContainsKey(selFont) Then font.selected = selFont Else Return
            If selSize >= 10 And selSize <= 150 Then font.size = selSize
        End If

        Dim fontMeta As str_hershey = fonts.Item(font.selected)
        ' Salimos si ya tenemos data en la fuente seleccionada y el tamaño es el mismo
        If fontMeta.data IsNot Nothing And font.size = fontMeta.size Then Return
        ' De lo contrario verificamos si realmente no hay data en la fuente seleccionada
        If fontMeta.data Is Nothing Then
            ' Cargamos el archivo svg en una cadena de texto
            Dim url_data As String = IO.File.ReadAllText(FONTS_PATH & "/" & font.selected & ".svg")
            ' Quitar cualquier cosa antes de <svg
            Dim firstOccurenceOfSVG As Integer = url_data.IndexOf("<svg ")
            If firstOccurenceOfSVG = -1 Then firstOccurenceOfSVG = url_data.IndexOf("<SVG ")
            fontMeta.str = url_data.Substring(firstOccurenceOfSVG)
        End If
        ' Creamos un XDocument y le cargamos el texto del archivo SVG
        Dim domParser As XDocument = XDocument.Parse(fontMeta.str)
        fontMeta.data = parseFont(domParser, font.size)
        fontMeta.size = font.size
        ' Actualizando el diccionario de fuentes
        fonts(font.selected) = fontMeta
    End Sub
    Public Sub createTextPaths(Optional fastRotate As Integer = 0)
        maxRect.Width = 0
        maxRect.Height = 0
        paths.Clear()

        Dim fontData() As HersheySymbol = fonts.Item(font.selected).data
        Dim fontHeight As Single = fonts(font.selected).size
        If fontData Is Nothing Then Return

        Dim originX As Single = pos.X
        Dim originY As Single = pos.Y
        Dim lineWidth As Single = 0
        Dim lineWidths As New List(Of Single)
        Dim lineIndex = 0
        Dim characters() As Char = text.ToCharArray
        Dim encodedCharacter As Integer

        Dim index As Integer = 0
        For Each character In characters
            If character = vbCr Then
                If lineWidth > maxRect.Width Then maxRect.Width = lineWidth
                lineWidths.Add(lineWidth)
                lineWidth = 0
                maxRect.Height += fontHeight * font.lineHeight
            End If
            encodedCharacter = Convert.ToUInt32(character)
            If encodedCharacter < fontData.Count Then
                lineWidth += fontData(encodedCharacter).width + font.characterSpacing
                If index + 1 = characters.Count Then
                    If lineWidth > maxRect.Width Then maxRect.Width = lineWidth
                    lineWidths.Add(lineWidth)
                    maxRect.Height += fonts(font.selected).size * font.lineHeight
                End If
            End If
            index += 1
        Next

        Dim rx As Single = maxRect.Width
        Dim ry As Single = maxRect.Height
        If fastRotate <> 0 Or rotation <> 0 Then maxRect = rotateRectangle(maxRect, rotation)

        For Each character In characters
            If character = vbCr Then
                lineIndex += 1
                originX = pos.X
                If font.alignment = textAlignment.center Then originX = pos.X + (rx - lineWidths(lineIndex)) / 2
                If font.alignment = textAlignment.right Then originX = pos.X + (rx - lineWidths(lineIndex))
                originY += fontHeight * font.lineHeight
            End If

            encodedCharacter = Convert.ToUInt32(character)
            If encodedCharacter < fontData.Count Then
                If fontData(encodedCharacter).d IsNot Nothing Then
                    Dim characterXPosition = originX

                    Dim dpath As New svgpath
                    dpath.clone(fontData(encodedCharacter).d)
                    dpath.translate(characterXPosition, originY)
                    dpath.iterate("rel")
                    If fastRotate <> 0 Then
                        dpath.rotate(fastRotate, pos.X + rx / 2, pos.Y + ry / 2)   'fast rotate without center
                    ElseIf rotation <> 0 Then
                        dpath.rotate(rotation, pos.X + rx / 2, pos.Y + ry / 2) 'rotation and 
                        dpath.translate(maxRect.X, maxRect.Y) ' center
                    End If
                    dpath.to_String()
                    paths.Add(dpath)
                End If
                originX += fontData(encodedCharacter).width + font.characterSpacing
            End If
        Next
    End Sub
#End Region

#Region "Funciones Públicas"
    Public Function getArrayOfFonts() As String()
        Dim svg_fileFonts As String() = IO.Directory.GetFiles(FONTS_PATH)
        For index = 0 To svg_fileFonts.Count - 1
            svg_fileFonts(index) = IO.Path.GetFileNameWithoutExtension(svg_fileFonts(index))

            loadFont(svg_fileFonts(index), 24) 'borrar

        Next
        Return svg_fileFonts
    End Function
    Public Function getStringFile(Optional roundNumber As Integer = -1) As String
        Dim flattenedPaths = ""
        For Each path In paths
            flattenedPaths += "<path d=" & ControlChars.Quote & path.to_String(False, roundNumber) & ControlChars.Quote & " fill=""none"" stroke-linecap=""join"" stroke-width=""1"" stroke=""#000000""></path>" & vbCrLf
        Next
        Dim svgDoctype As String = "<?xml version=""1.0"" standalone=""no""?>" & vbCrLf & "<!DOCTYPE svg PUBLIC "" -// W3C // DTD SVG 1.1//EN"" ""http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd"">" & vbCrLf & vbCrLf
        Dim cabecera As New xmlText("svg")
        cabecera.add("xmlns", "http://www.w3.org/2000/svg")
        cabecera.addRect(pos.X + maxRect.Width, pos.Y + maxRect.Height)
        cabecera.add("viewbox", pos.X & " " & pos.Y & " " & maxRect.Width & " " & maxRect.Height)
        Dim svgHeader As String = cabecera.str(False) & "><title>CNC Fill Text</title>" & vbCrLf & vbCrLf
        Dim svgBody As String = "<g>" & vbCrLf & flattenedPaths & "</g>" & vbCrLf & "</svg>"
        Dim svgRect As String = "" '"fill:yellow;stroke:red;stroke-width:1;fill-opacity:0.1"
        If rectangle Then
            Dim borde As New xmlText("rect")
            borde.addRect(pos.X, pos.Y, maxRect.Width, maxRect.Height)
            borde.add("stroke", "black")
            borde.add("stroke-width", 1)
            borde.add("fill", "none")
            svgRect = borde.str
        End If
        Return svgDoctype & svgHeader & svgRect & svgBody
    End Function
#End Region

End Class