﻿#Region "Microsoft.VisualBasic::884502716def045fbf3a249f990e6a9f, ..\VisualBasic_AppFramework\Scripting\Math\Math\Logical\FuzzyLogic\LinguisticVariableCollection.vb"

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

#Region "GNU Lesser General Public License"
'
'This file is part of DotFuzzy.
'
'DotFuzzy is free software: you can redistribute it and/or modify
'it under the terms of the GNU Lesser General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'DotFuzzy is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU Lesser General Public License for more details.
'
'You should have received a copy of the GNU Lesser General Public License
'along with DotFuzzy.  If not, see <http://www.gnu.org/licenses/>.
'

#End Region

Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Text

Namespace Logical.FuzzyLogic

    ''' <summary>
    ''' Represents a collection of rules.
    ''' </summary>
    Public Class LinguisticVariableCollection
        Inherits Dictionary(Of LinguisticVariable)

        Sub New()
            Call MyBase.New
        End Sub

#Region "Public Methods"

        ''' <summary>
        ''' Finds a linguistic variable in a collection.
        ''' </summary>
        ''' <param name="linguisticVariableName">Linguistic variable name.</param>
        ''' <returns>The linguistic variable, if founded.</returns>
        Public Overloads Function Find(linguisticVariableName As String) As LinguisticVariable
            If Me.ContainsKey(linguisticVariableName) Then
                Return Me(linguisticVariableName)
            Else
                Throw New Exception("LinguisticVariable not found: " & linguisticVariableName)
            End If
        End Function

#End Region
    End Class
End Namespace
