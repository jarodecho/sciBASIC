Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic

''' <summary>
''' The heart of the Q-learning algorithm, the QTable contains the table
''' which maps states, actions and their Q values. This class has elaborate
''' documentation, and should be the focus of the students' body of work
''' for the purposes of this tutorial.
'''     
''' @author A.Liapis (Original author), A. Hartzen (2013 modifications) 
''' </summary>
Public Class QTable

    ''' <summary>
    ''' for creating random numbers
    ''' </summary>
    ReadOnly __randomGenerator As Random

    ''' <summary>
    ''' the table variable stores the Q-table, where the state is saved
    ''' directly as the actual map. Each map state has an array of Q values
    ''' for all the actions available for that state.
    ''' </summary>
    Public ReadOnly Property Table As Dictionary(Of String, Single())

    ''' <summary>
    ''' the actionRange variable determines the number of actions available
    ''' at any map state, and therefore the number of Q values in each entry
    ''' of the Q-table.
    ''' </summary>
    Public ReadOnly Property ActionRange As Integer

#Region "E-GREEDY Q-LEARNING SPECIFIC VARIABLES"

    ''' <summary>
    ''' for e-greedy Q-learning, when taking an action a random number is
    ''' checked against the explorationChance variable: if the number is
    ''' below the explorationChance, then exploration takes place picking
    ''' an action at random. Note that the explorationChance is not a final
    ''' because it is customary that the exploration chance changes as the
    ''' training goes on.
    ''' </summary>
    Public Property ExplorationChance As Single = 0.05F
    ''' <summary>
    ''' the discount factor is saved as the gammaValue variable. The
    ''' discount factor determines the importance of future rewards.
    ''' If the gammaValue is 0 then the AI will only consider immediate
    ''' rewards, while with a gammaValue near 1 (but below 1) the AI will
    ''' try to maximize the long-term reward even if it is many moves away.
    ''' </summary>
    Public Property GammaValue As Single = 0.9F
    ''' <summary>
    ''' the learningRate determines how new information affects accumulated
    ''' information from previous instances. If the learningRate is 1, then
    ''' the new information completely overrides any previous information.
    ''' Note that the learningRate is not a final because it is
    ''' customary that the learningRate changes as the
    ''' training goes on.
    ''' </summary>
    Public Property LearningRate As Single = 0.15F
#End Region

    'PREVIOUS STATE AND ACTION VARIABLES
    ''' <summary>
    ''' Since in Q-learning the updates to the Q values are made ONE STEP
    ''' LATE, the state of the world when the action resulting in the reward
    ''' was made must be stored.
    ''' </summary>
    Dim _prevState() As Char
    ''' <summary>
    ''' Since in Q-learning the updates to the Q values are made ONE STEP
    ''' LATE, the index of the action which resulted in the reward must be
    ''' stored.
    ''' </summary>
    Dim _prevAction As Integer

    ''' <summary>
    ''' Q table constructor, initiates variables. </summary>
    ''' <param name="actionRange"> number of actions available at any map state </param>
    Public Sub New(actionRange As Integer)
        __randomGenerator = New Random()
        Me.ActionRange = actionRange
        Me.Table = New Dictionary(Of String, Single())()
    End Sub

    ''' <summary>
    ''' For this example, the getNextAction function uses an e-greedy
    ''' approach, having exploration happen if the exploration chance
    ''' is rolled.
    ''' </summary>
    ''' <param name="map"> current map (state) </param>
    ''' <returns> the action to be taken by the calling program </returns>
    Public Overridable Function NextAction(map() As Char) As Integer
        _prevState = CType(map.Clone(), Char())

        If __randomGenerator.NextDouble() < ExplorationChance Then
            _prevAction = __explore()
        Else
            _prevAction = __getBestAction(map)
        End If
        Return _prevAction
    End Function

    ''' <summary>
    ''' The getBestAction function uses a greedy approach for finding
    ''' the best action to take. Note that if all Q values for the current
    ''' state are equal (such as all 0 if the state has never been visited
    ''' before), then getBestAction will always choose the same action.
    ''' If such an action is invalid, this may lead to a deadlock as the
    ''' map state never changes: for situations like these, exploration
    ''' can get the algorithm out of this deadlock.
    ''' </summary>
    ''' <param name="map"> current map (state) </param>
    ''' <returns> the action with the highest Q value </returns>
    Private Function __getBestAction(map() As Char) As Integer
        Dim rewards() As Single = Me.__getActionsQValues(map)
        Dim maxRewards As Single = Single.NegativeInfinity
        Dim indexMaxRewards As Integer = 0

        For i As Integer = 0 To rewards.Length - 1
            If maxRewards < rewards(i) Then
                maxRewards = rewards(i)
                indexMaxRewards = i
            End If
        Next i

        Return indexMaxRewards
    End Function

    ''' <summary>
    ''' The explore function is called for e-greedy algorithms.
    ''' It can choose an action at random from all available,
    ''' or can put more weight towards actions that have not been taken
    ''' as often as the others (most unknown).
    ''' </summary>
    ''' <returns> index of action to take </returns>
    Private Function __explore() As Integer
        Return (New Random()).Next(4)
    End Function

    ''' <summary>
    ''' The updateQvalue is the heart of the Q-learning algorithm. Based on
    ''' the reward gained by taking the action prevAction while being in the
    ''' state prevState, the updateQvalue must update the Q value of that
    ''' {prevState, prevAction} entry in the Q table. In order to do that,
    ''' the Q value of the best action of the current map state must also
    ''' be calculated.
    ''' </summary>
    ''' <param name="reward"> at the current map state </param>
    ''' <param name="map"> current map state (for finding the best action of the
    ''' current map state) </param>
    Public Overridable Sub UpdateQvalue(reward As Integer, map() As Char)
        Dim preVal() As Single = Me.__getActionsQValues(Me._prevState)
        preVal(Me._prevAction) += Me.LearningRate * (reward + Me.GammaValue * Me.__getActionsQValues(map)(Me.__getBestAction(map)) - preVal(Me._prevAction))
    End Sub

    ''' <summary>
    ''' This helper function is used for entering the map state into the
    ''' HashMap </summary>
    ''' <param name="map"> </param>
    ''' <returns> String used as a key for the HashMap </returns>
    Private Function __getMapString(map() As Char) As String
        Dim result As String = ""
        For x As Integer = 0 To map.Length - 1
            result &= "" & AscW(map(x))
            If x > 0 AndAlso x Mod 3 = 0 Then
                result += vbLf
            End If
        Next x
        Return result
    End Function
    ''' <summary>
    ''' The getActionsQValues function returns an array of Q values for
    ''' all the actions available at any state. Note that if the current
    ''' map state does not already exist in the Q table (never visited
    ''' before), then it is initiated with Q values of 0 for all of the
    ''' available actions.
    ''' </summary>
    ''' <param name="map"> current map (state) </param>
    ''' <returns> an array of Q values for all the actions available at any state </returns>
    Private Function __getActionsQValues(map() As Char) As Single()
        Dim actions() As Single = GetValues(map)
        If actions Is Nothing Then
            Dim initialActions(ActionRange - 1) As Single
            For i As Integer = 0 To ActionRange - 1
                initialActions(i) = 0.0F
            Next i
            Table(__getMapString(map)) = initialActions
            Return initialActions
        End If
        Return actions
    End Function
    ''' <summary>
    ''' printQtable is included for debugging purposes and uses the
    ''' action labels used in the maze class (even though the Qtable
    ''' is written so that it can more generic).
    ''' </summary>
    Public Sub PrintQTable()
        Dim [iterator] As IEnumerator = Table.Keys.GetEnumerator()
        Do While [iterator].MoveNext
            Dim key() As Char = CType([iterator].Current, Char())
            Dim qvalues() As Single = GetValues(key)

            Console.Write(AscW(key(0)) & "" & AscW(key(1)) & "" & AscW(key(2)))
            Console.WriteLine("  UP   RIGHT  DOWN  LEFT")
            Console.Write(AscW(key(3)) & "" & AscW(key(4)) & "" & AscW(key(5)))
            Console.WriteLine(": " & qvalues(0) & "   " & qvalues(1) & "   " & qvalues(2) & "   " & qvalues(3))
            Console.WriteLine(AscW(key(6)) & "" & AscW(key(7)) & "" & AscW(key(8)))
        Loop
    End Sub

    ''' <summary>
    ''' Helper function to find the Q-values of a given map state.
    ''' </summary>
    ''' <param name="map"> current map (state) </param>
    ''' <returns> the Q-values stored of the Qtable entry of the map state, otherwise null if it is not found </returns>
    Public Overridable Function GetValues(map() As Char) As Single()
        Dim mapKey As String = __getMapString(map)
        If Table.ContainsKey(mapKey) Then
            Return Table(mapKey)
        End If
        Return Nothing
    End Function
End Class