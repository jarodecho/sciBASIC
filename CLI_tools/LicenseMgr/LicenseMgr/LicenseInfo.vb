﻿#Region "Microsoft.VisualBasic::6ab9efbd49fab902f2cf3a007c484dfc, ..\sciBASIC#\CLI_tools\LicenseMgr\LicenseMgr\LicenseInfo.vb"

' Author:
' 
'       asuka (amethyst.asuka@gcmodeller.org)
'       xieguigang (xie.guigang@live.com)
'       xie (genetics@smrucc.org)
' 
' Copyright (c) 2018 GPL3 Licensed
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

Imports Microsoft.VisualBasic.ApplicationServices.Development

Module LicenseInfoExtensions

    Dim __content As LicenseInfo

    Public Property info As LicenseInfo
        Get
            If __content Is Nothing Then
                __content = New LicenseInfo
            End If

            Return __content
        End Get
        Set(value As LicenseInfo)
            __content = value
        End Set
    End Property

End Module
