﻿#Region "Microsoft.VisualBasic::ea9de97ad00dd264779b85e5d3a05a18, vs_solutions\tutorials\core.test\ParallelLoadingTest.vb"

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

    ' Class ParallelLoadingTest
    ' 
    '     Properties: ddddd, fffd
    ' 
    '     Function: Load
    ' 
    ' /********************************************************************************/

#End Region

#Region "Microsoft.VisualBasic::1dcfe98595b59fadc05eff88da28068f, core.test"

    ' Author:
    ' 
    ' 
    ' Copyright (c) 2018 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 


    ' Source file summaries:

    ' Class ParallelLoadingTest
    ' 
    '     Properties: ddddd, fffd
    ' 
    '     Function: Load
    ' 
    ' 

#End Region

#Region "Microsoft.VisualBasic::b493f08649bb3120eb1d418929d10925, core.test"

    ' Author:
    ' 
    ' 
    ' Copyright (c) 2018 GPL3 Licensed
    ' 
    ' 
    ' GNU GENERAL PUBLIC LICENSE (GPL3)
    ' 


    ' Source file summaries:

    ' Class ParallelLoadingTest
    ' 
    '     Properties: ddddd, fffd
    ' 
    ' 
    '     Function: Load
    ' 
    ' 
    ' 

#End Region

<Serializable>
Public Class ParallelLoadingTest

    Public Property ddddd As String
    Public Property fffd As Date

    <Microsoft.VisualBasic.Parallel.ParallelLoading.LoadEntry>
    Public Shared Function Load(path As String) As ParallelLoadingTest()
        Call Threading.Thread.Sleep(10 * 1000)
        Return {New ParallelLoadingTest With {.ddddd = Rnd(), .fffd = Now}}
    End Function
End Class
