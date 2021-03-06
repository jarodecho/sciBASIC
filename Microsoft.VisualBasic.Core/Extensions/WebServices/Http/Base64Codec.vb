﻿#Region "Microsoft.VisualBasic::a3e29c4c9a353ba410134637bb269f37, Microsoft.VisualBasic.Core\Extensions\WebServices\Http\Base64Codec.vb"

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

    '     Module Base64Codec
    ' 
    '         Function: __getImageFromBase64, __toBase64String, GetImage, (+3 Overloads) ToBase64String, (+2 Overloads) ToStream
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Imaging
Imports Microsoft.VisualBasic.Language

Namespace Net.Http

    ''' <summary>
    ''' Tools API for encoded the image into a base64 string.
    ''' </summary>
    Public Module Base64Codec

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function ToBase64String(byts As IEnumerable(Of Byte)) As String
            Return Convert.ToBase64String(byts.ToArray)
        End Function

        ''' <summary>
        ''' Function to Get Image from Base64 Encoded String
        ''' </summary>
        ''' <param name="Base64String"></param>
        ''' <param name="format"></param>
        ''' <returns></returns>
        <Extension> Public Function GetImage(Base64String As String, Optional format As ImageFormats = ImageFormats.Png) As Bitmap
            Try
                If String.IsNullOrEmpty(Base64String) Then
                    ' Checking The Base64 string validity
                    Return Nothing
                Else
                    Return Base64String.__getImageFromBase64(GetFormat(format))
                End If
            Catch ex As Exception
                Call ex.PrintException
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Function to Get Image from Base64 Encoded String
        ''' </summary>
        ''' <param name="base64String"></param>
        ''' <param name="format"></param>
        ''' <returns></returns>
        ''' 
        <Extension>
        Private Function __getImageFromBase64(base64String$, format As ImageFormat) As Bitmap
            Dim bytData As Byte(), streamImage As Bitmap

            ' Convert Base64 to Byte Array
            bytData = Convert.FromBase64String(base64String)

            ' Using Memory stream to save image
            Using ms As New MemoryStream(bytData)
                ' Converting image from Memory stream
                streamImage = Image.FromStream(ms)
            End Using

            Return streamImage
        End Function

        ''' <summary>
        ''' Convert the Image from Input to Base64 Encoded String
        ''' </summary>
        ''' <param name="ImageInput"></param>
        ''' <returns></returns>
        <Extension> Public Function ToBase64String(ImageInput As Image, Optional format As ImageFormats = ImageFormats.Png) As String
            Try
                Return __toBase64String(ImageInput, GetFormat(format))
            Catch ex As Exception
                Call ex.PrintException
                Return ""
            End Try
        End Function

        ''' <summary>
        ''' Convert the Image from Input to Base64 Encoded String
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension> Public Function ToBase64String(bmp As Bitmap, Optional format As ImageFormats = ImageFormats.Png) As String
            Return ToBase64String(ImageInput:=bmp, format:=format)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function ToStream(image As Image, Optional format As ImageFormats = ImageFormats.Png) As MemoryStream
            Return image.ToStream(format.GetFormat)
        End Function

        <Extension>
        Public Function ToStream(image As Image, format As ImageFormat) As MemoryStream
            With New MemoryStream
                Call image.Save(.ByRef, format)
                Call .Flush()
                Call .Seek(Scan0, SeekOrigin.Begin)
                Return .ByRef
            End With
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Private Function __toBase64String(image As Image, format As ImageFormat) As String
            Return Convert.ToBase64String(image.ToStream(format).ToArray)
        End Function
    End Module
End Namespace
