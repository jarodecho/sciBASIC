﻿Imports System.Runtime.CompilerServices

Namespace ComponentModel.Algorithm.BinaryTree

    ''' <summary>
    ''' Binary tree balance helper
    ''' </summary>
    Public Module AVLSupports

        <Extension>
        Public Function RotateLL(Of K, V)(node As BinaryTree(Of K, V)) As BinaryTree(Of K, V)
            Dim top = node.Left

            node.Left = top.Right
            top.Right = node

            node.SetValue("height", Math.Max(node.Left.height, node.Right.height) + 1)
            top.SetValue("height", Math.Max(top.Left.height, top.Right.height) + 1)

            Return top
        End Function

        <Extension>
        Public Function RotateRR(Of K, V)(node As BinaryTree(Of K, V)) As BinaryTree(Of K, V)
            Dim top = node.Right

            node.Right = top.Left
            top.Left = node

            node.SetValue("height", Math.Max(node.Left.height, node.Right.height) + 1)
            top.SetValue("height", Math.Max(top.Left.height, top.Right.height) + 1)

            Return top
        End Function

        <Extension>
        Public Function RotateLR(Of K, V)(node As BinaryTree(Of K, V)) As BinaryTree(Of K, V)
            node.Left = node.Left.RotateRR
            Return node.RotateLL
        End Function

        <Extension>
        Public Function RotateRL(Of K, V)(node As BinaryTree(Of K, V)) As BinaryTree(Of K, V)
            node.Right = node.Right.RotateLL
            Return node.RotateRR
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function height(Of K, V)(node As BinaryTree(Of K, V)) As Double
            Return If(node Is Nothing, -1, CDbl(node!height))
        End Function
    End Module
End Namespace