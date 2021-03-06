﻿#Region "Microsoft.VisualBasic::0d898980449d57303d7415a954794fb6, Microsoft.VisualBasic.Core\Extensions\StringHelpers\NumericFormatHelper.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xie (genetics@smrucc.org)
    '       xieguigang (xie.guigang@live.com)
    ' 
    ' Copyright (c) 2018 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 
    ' 
    ' This program is free software: you can redistribute it and/or modify
    ' it under the terms of the GNU General Public License as published by
    ' the Free Software Foundation, either version 3 of the License, or
    ' (at your option) any later version.
    ' 
    ' This program is distributed in the hope that it will be useful,
    ' but WITHOUT ANY WARRANTY; without even the implied warranty of
    ' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    ' GNU General Public License for more details.
    ' 
    ' You should have received a copy of the GNU General Public License
    ' along with this program. If not, see <http://www.gnu.org/licenses/>.



    ' /********************************************************************************/

    ' Summaries:

    ' Module NumericFormatHelper
    ' 
    '     Function: [Decimal], Float, SafeToString, SafeToStrings
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices

''' <summary>
''' ###### ``C``货币
'''
''' ```vbnet
''' 2.5.ToString("C")
''' ' ￥2.50
''' ```
'''
''' ###### ``D``十进制数
'''
''' ```vbnet
''' 25.ToString("D5")
''' ' 00025
''' ```
''' 
''' ###### ``E``科学型
'''
''' ```vbnet
''' 25000.ToString("E")
''' ' 2.500000E+005
''' ```
''' 
''' ###### ``F``固定点
'''
''' ```vbnet
''' 25.ToString("F2")
''' ' 25.00
''' ```
''' 
''' ###### ``G``常规
'''
''' ```vbnet
''' 2.5.ToString("G")
''' ' 2.5
''' ```
'''
''' ###### ``N``数字
'''
''' ```vbnet
''' 2500000.ToString("N")
''' ' 2,500,000.00
''' ```
''' 
''' ###### ``X``十六进制
'''
''' ```vbnet
''' 255.ToString("X")
''' ' FF
''' ```
''' </summary>
Public Module NumericFormatHelper

    ''' <summary>
    ''' ``D&lt;n>``
    ''' </summary>
    ''' <param name="n%"></param>
    ''' <returns></returns>
    Public Function [Decimal](n%) As String
        Return "D" & n
    End Function

    ''' <summary>
    ''' ``F&lt;n>``
    ''' </summary>
    ''' <param name="n%"></param>
    ''' <returns></returns>
    Public Function Float(n%) As String
        Return "F" & n
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="x#"></param>
    ''' <param name="NaN_imaginary$">Default using R language style default numeric value</param>
    ''' <returns></returns>
    <Extension>
    Public Function SafeToString(x#, Optional NaN_imaginary$ = "NA") As String
        If x.IsNaNImaginary Then
            Return NaN_imaginary
        Else
            Return CStr(x)
        End If
    End Function

    <Extension>
    Public Function SafeToStrings(v As IEnumerable(Of Double), Optional NaN_imaginary$ = "NA") As String()
        Return v _
            .Select(Function(x) x.SafeToString(NaN_imaginary)) _
            .ToArray
    End Function
End Module
