﻿#Region "Microsoft.VisualBasic::95c76db2cefe341ba66b482aae48607c, ..\sciBASIC#\Data\Trinity\NLP.vb"

' Author:
' 
'       asuka (amethyst.asuka@gcmodeller.org)
'       xieguigang (xie.guigang@live.com)
'       xie (genetics@smrucc.org)
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

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Data.Graph.Analysis.PageRank
Imports Microsoft.VisualBasic.Language

''' <summary>
''' 从现有的理论和技术现状看，通用的、高质量的自然语言处理系统，仍然是较长期的努力目标，
''' 但是针对一定应用，具有相当自然语言处理能力的实用系统已经出现，有些已商品化，甚至
''' 开始产业化。典型的例子有：
''' 
''' + 多语种数据库和专家系统的自然语言接口
''' + 各种机器翻译系统
''' + 全文信息检索系统
''' + 自动文摘系统
''' 
''' </summary>
Public Module NLPExtensions

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="keywordsSet"></param>
    ''' <returns></returns>
    <Extension>
    Public Function KeyPhrases(originalText$, keywordsSet As IEnumerable(Of String), Optional minOccurNum% = 2) As IEnumerable(Of String)
        Dim result As New List(Of String)
        Dim keywords As Index(Of String) = keywordsSet _
            .Select(AddressOf LCase) _
            .Indexing

        originalText = originalText.StripMessy

        For Each sentence In originalText.Sentences.Select(AddressOf LCase)
            Dim one As New List(Of String)

            For Each word As String In sentence.Words

                If word.IsOneOfA(keywords) Then
                    one += word
                Else
                    If one.Count > 1 Then
                        result += one.JoinBy(" ")
                    ElseIf one = 0 Then
                        Continue For
                    Else
                        one *= 0
                    End If
                End If
            Next

            If one.Count > 1 Then
                result += one.JoinBy(" ")
            End If
        Next

        Return From phrase As String
               In result
               Let count = originalText.Count(phrase, CompareMethod.Text)
               Where count >= minOccurNum
               Order By count Descending
               Select phrase
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension> Public Function KeyWords(text As GraphMatrix) As Dictionary(Of String, Double)
        Dim result = text.TranslateVector(New PageRank(text).ComputePageRank, True)
        Return result
    End Function

    ''' <summary>
    ''' 获取最重要的num个长度大于等于sentence_min_len的句子用来生成摘要。
    ''' </summary>
    ''' <param name="text"></param>
    ''' <returns></returns>
    <Extension> Public Function Abstract(text As GraphMatrix, Optional minWords% = 6, Optional minWeight# = 0.05) As Dictionary(Of String, Double)
        Dim result = text.TranslateVector(New PageRank(text).ComputePageRank, True)

        Return result _
            .Subset(Function(sentence, w)
                        Return sentence.Words.Length >= minWords AndAlso w >= minWords
                    End Function) _
            .ToDictionary
    End Function
End Module
