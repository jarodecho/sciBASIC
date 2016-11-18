﻿#Region "Microsoft.VisualBasic::8e6c3d13a900910d0f36ecfac82b75d7, ..\visualbasic_App\Microsoft.VisualBasic.Architecture.Framework\Tools\SoftwareToolkits\XmlDoc\ProjectNamespace.vb"

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

' Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
'    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. 

Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Microsoft.VisualBasic.Text

Namespace SoftwareToolkits.XmlDoc.Assembly

    ''' <summary>
    ''' A namespace within a project -- typically a collection of related types.  Equates to a .net Namespace.
    ''' </summary>
    Public Class ProjectNamespace

        Dim project As Project
        Dim m_types As Dictionary(Of String, ProjectType)

        Public Property Path() As String

        Public ReadOnly Property Types() As IEnumerable(Of ProjectType)
            Get
                Return Me.m_types.Values
            End Get
        End Property
        Public Sub New(project As Project)
            Me.project = project
            Me.m_types = New Dictionary(Of String, ProjectType)()
        End Sub

        Public Overloads Function [GetType](typeName As String) As ProjectType
            If Me.m_types.ContainsKey(typeName.ToLower()) Then
                Return Me.m_types(typeName.ToLower())
            End If

            Return Nothing
        End Function

        Public Function EnsureType(typeName As String) As ProjectType
            Dim pt As ProjectType = Me.[GetType](typeName)

            If pt Is Nothing Then
                pt = New ProjectType(Me) With {
                    .Name = typeName
                }

                Me.m_types.Add(typeName.ToLower(), pt)
            End If

            Return pt
        End Function

        ''' <summary>
        ''' Exports for namespace markdown documents
        ''' </summary>
        ''' <param name="folderPath"></param>
        ''' <param name="pageTemplate"></param>
        ''' <param name="hexoPublish"></param>
        Public Sub ExportMarkdownFile(folderPath As String, pageTemplate As String, Optional hexoPublish As Boolean = False)
            Dim typeList As New StringBuilder()
            Dim projectTypes As New SortedList(Of String, ProjectType)()
            Dim ext As String = If(hexoPublish, ".html", ".md")

            For Each pt As ProjectType In Me.Types
                projectTypes.Add(pt.Name, pt)
            Next

            Call typeList.AppendLine("|Type|Summary|")
            Call typeList.AppendLine("|----|-------|")

            For Each pt As ProjectType In projectTypes.Values
                Dim file$

                If hexoPublish Then
                    file = "T-" & Me.Path & "." & pt.Name & $"{ext}"
                Else
                    file = $"./{pt.Name}.md"
                End If

                Dim lines$() = If(pt.Summary Is Nothing, "", pt.Summary) _
                    .Trim(ASCII.CR, ASCII.LF) _
                    .Trim _
                    .lTokens
                Dim summary$ = If(lines.IsNullOrEmpty OrElse lines.Length = 1,
                    lines.FirstOrDefault,
                    lines.First & " ...")

                Call typeList.AppendLine($"|[{pt.Name}]({file})|{summary}|")
            Next

            Dim text As String
            Dim path$ ' *.md output path

            If hexoPublish Then
                text = $"---
title: {Me.Path}
---"
                text = text & vbCrLf & vbCrLf & typeList.ToString
                path = folderPath & "/N-" & Me.Path & ".md"
            Else
                text = String.Format(vbCr & vbLf & "# {0}" & vbCr & vbLf & vbCr & vbLf & "{1}" & vbCr & vbLf, Me.Path, typeList.ToString())
                path = folderPath & "/" & Me.Path & "/index.md"
            End If

            If pageTemplate IsNot Nothing Then
                text = pageTemplate.Replace("[content]", text)
            End If

            Call text.SaveTo(path, Encoding.UTF8)
        End Sub
    End Class
End Namespace
