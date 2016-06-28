﻿#Region "19ed1258713d3eb569665fe92737ed7b, ..\Microsoft.VisualBasic.Architecture.Framework\Text\StringSimilarity\Tokeniser.vb"

    ' Author:
    ' 
    '       asuka (amethyst.asuka@gcmodeller.org)
    '       xieguigang (xie.guigang@live.com)
    ' 
    ' Copyright (c) 2016 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
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

#End Region

Imports System.Text.RegularExpressions

Namespace Text.Similarity

    '
    'Tokenization string 
    'Author: Thanh Ngoc Dao - Thanh.dao@gmx.net
    'Copyright (c) 2005 by Thanh Ngoc Dao.
    '

    ''' <summary>
    ''' Summary description for Tokeniser.
    ''' Partition string off into subwords
    ''' </summary>
    Module Tokeniser

        Const BlanksControl As String = vbCr & vbLf & vbTab & "  "

        Private Function Tokenize(input As System.String) As ArrayList
            Dim returnVect As New ArrayList(10)
            Dim nextGapPos As Integer
            Dim curPos As Integer = 0
            While curPos < input.Length
                Dim ch As Char = input(curPos)
                If System.[Char].IsWhiteSpace(ch) Then
                    curPos += 1
                End If
                nextGapPos = input.Length
                For i As Integer = 0 To BlanksControl.Length - 1
                    Dim testPos As Integer = input.IndexOf(CType(BlanksControl(i), [Char]), curPos)
                    If testPos < nextGapPos AndAlso testPos <> -1 Then
                        nextGapPos = testPos
                    End If
                Next

                Dim term As System.String = input.Substring(curPos, (nextGapPos) - (curPos))
                'if (!stopWordHandler.isWord(term))
                returnVect.Add(term)
                curPos = nextGapPos
            End While

            Return returnVect
        End Function

        Private Sub __normalizeCasing(ByRef input As String)
            'if it is formed by Pascal/Carmel casing
            For i As Integer = 0 To input.Length - 1
                If [Char].IsSeparator(input(i)) Then
                    input = input.Replace(input(i).ToString(), " ")
                End If
            Next
            Dim idx As Integer = 1
            While idx < input.Length - 2
                idx += 1
                If ([Char].IsUpper(input(idx)) AndAlso [Char].IsLower(input(idx + 1))) AndAlso (Not [Char].IsWhiteSpace(input(idx - 1)) AndAlso Not [Char].IsSeparator(input(idx - 1))) Then
                    input = input.Insert(idx, " ")
                    idx += 1
                End If
            End While
        End Sub

        Public Function Partition(input As String) As String()
            Call __normalizeCasing(input)

            input = input.ToLower()

            Dim r As New Regex("([ \t{}():;])")
            Dim Tokens As String() = r.Split(input)
            Dim Filter As New List(Of String)

            For i As Integer = 0 To Tokens.Length - 1
                Dim mc As MatchCollection = r.Matches(Tokens(i))
                If mc.Count <= 0 AndAlso Tokens(i).Trim().Length > 0 Then
                    Filter.Add(Tokens(i))
                End If
            Next

            Tokens = Filter.ToArray

            Return Tokens
        End Function
    End Module
End Namespace
