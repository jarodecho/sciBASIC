﻿Imports Microsoft.VisualBasic.Imaging.Driver
Imports Microsoft.VisualBasic.Imaging.Driver.CSS
Imports Microsoft.VisualBasic.MIME.Markup.HTML.CSS
Imports Microsoft.VisualBasic.MIME.Markup.HTML.CSS.Render
Imports VisualBasic = Microsoft.VisualBasic.Language.Runtime

Module DriverTest
    Sub Main()

        Dim css As New CSSFile With {
            .Selectors = {
                New Selector With {.Selector = "#CSS", .Properties = New Dictionary(Of String, String) From {{"XXX", "123"}}},
                New Selector With {.Selector = "#testfont", .Properties = New Dictionary(Of String, String) From {{"font-size", "15px"}}}
            }
        }

        With New VisualBasic

            ' Call (AddressOf testPlot).RunPlot(css, !A = 99, !B = 123, !CSS = "dertfff")

            Call GetType(DriverTest).LoadDriver("test.plot").RunPlot(Nothing, css, !A = 99, !B = 123, !CSS = "from reflection: 1234567890")

            Dim driver As Func(Of Single, Single, String, GraphicsData) = AddressOf testPlot

            Call driver.RunPlot(
                css, !A = 99,
                     !B = 123,
                     !CSS = "from delegate: 1234567890",
                     !testfont = "12345")

            Call testPlot(A:=666, b:=4444, css:="direct calls")

        End With


        Pause()
    End Sub

    ''' <summary>
    ''' Test target
    ''' </summary>
    ''' <param name="A!"></param>
    ''' <param name="b!"></param>
    ''' <param name="css$"></param>
    ''' <returns></returns>
    <Driver("test.plot")>
    Public Function testPlot(A!, b!, Optional css$ = "1234", Optional testFont$ = CSSFont.Win7LargerBold) As GraphicsData
        Call Console.WriteLine(A)
        Call Console.WriteLine(b)
        Call Console.WriteLine(css)
        Call Console.WriteLine(testFont)

        Call Console.WriteLine(New String("+"c, 20))
    End Function
End Module